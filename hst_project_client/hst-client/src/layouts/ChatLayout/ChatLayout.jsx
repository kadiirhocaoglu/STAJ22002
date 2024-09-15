import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { Layout, Menu, List, Divider, message } from 'antd';
import { DashboardOutlined, UserOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import './ChatLayout.css';

const { Sider, Header, Content, Footer } = Layout;

const ChatLayout = ({ children }) => {
  const navigate = useNavigate();
  const [selectedUser, setSelectedUser] = useState(null);
  const [users, setUsers] = useState([]);
  const [currentUserId, setCurrentUserId] = useState(null);
  const [currentUserPhone, setCurrentUserPhone] = useState(null);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        setCurrentUserId(decodedToken.sub);
        setCurrentUserPhone(decodedToken.given_name);
      } catch (error) {
        
      }
    } else {
    }
  }, []);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get('http://localhost:5121/api/User/live-support-users');
        console.log('API yanıtı:', response.data); 
        const filteredUsers = response.data.filter(user => user.phone !== currentUserPhone);
        setUsers(filteredUsers);

      } catch{

      }
    };

    const connectToHub = async () => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl('http://localhost:5121/chat-hub')
          .configureLogging(LogLevel.Information)
          .build();

          connection.on('NewUserConnected', (newUser) => {
            console.log('Yeni kullanıcı bağlantısı:', newUser);
            if (newUser.phone !== currentUserPhone) {
              setUsers((prevUsers) => {
                if (prevUsers.some((user) => user.phone === newUser.phone)) {
                  return prevUsers;
                }
                console.log(`${newUser.name} sisteme giriş yaptı.`);
                return [...prevUsers, newUser];
              });
            }
          });

        await connection.start();
        console.log('SignalR bağlantısı kuruldu');
        setHubConnection(connection);

        if (currentUserId) {
          await connection.invoke('Connect', parseInt(currentUserId));
        }
      } catch (error) {
        
      }
    };

    if (currentUserId && !hubConnection) {
      fetchUsers();
      connectToHub();
    }

    return () => {
      if (hubConnection) {
        hubConnection.stop();
      }
    };
  }, [currentUserId, hubConnection]);

  const menuItems = [
    {
      key: '1',
      icon: <DashboardOutlined />,
      label: 'Dashboard',
      path: '/chat',
      onClick: () => navigate('/chat'),
    },
  ];

  const getActiveKey = () => menuItems.find(item => item.path === window.location.pathname)?.key;

  const getPageTitle = () => menuItems.find(item => item.path === window.location.pathname)?.label;

  const handleUserSelect = (user) => {
    if (currentUserId) {
      setSelectedUser(user);
      navigate(`/chat/${currentUserId}/${user.id}`);
    } else {
      message.error('Geçerli kullanıcı ID\'si bulunamadı.');
    }
  };

  return (
    <Layout className="layout-container">
      <Sider>
        <div className="sider-logo">HST POS</div>
        <Menu
          theme="dark"
          mode="inline"
          items={menuItems}
          defaultSelectedKeys={[getActiveKey()]}
        />
        <div className="user-section">
          <div className="user-section-title">Kullanıcılar</div>
          <Divider style={{ margin: 0 }} />
          <div className="user-list">
            <List
              dataSource={users}
              renderItem={(user) => (
                <List.Item
                  key={user.id}
                  onClick={() => handleUserSelect(user)}
                  style={{ cursor: 'pointer', padding: '10px', color: '#fff' }}
                >
                  <UserOutlined style={{ marginRight: 8 }} />
                  {user.name}
                </List.Item>
              )}
            />
          </div>
        </div>
      </Sider>
      <Layout>
        <Header>
          <div style={{ display: 'flex', justifyContent: 'space-between', color: 'white' }}>
            <h2>{getPageTitle()}</h2>
            <h2>Operasyon Destek Paneli</h2>
          </div>
        </Header>
        <Content style={{ margin: '16px', padding: '24px', background: '#fff' }}>
          {React.cloneElement(children, { selectedUser })}
        </Content>
        <Footer className="footer">
          HST Intern ©{new Date().getFullYear()} Abdülkadir Hocaoğlu
        </Footer>
      </Layout>
    </Layout>
  );
};

ChatLayout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default ChatLayout;
