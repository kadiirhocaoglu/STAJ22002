import PropTypes from "prop-types";
import { DashboardOutlined, UserOutlined, MessageOutlined } from "@ant-design/icons";
import { Layout, Menu } from "antd";
import { useNavigate } from "react-router-dom";
import "./AdminLayout.css";
const { Sider, Header, Content, Footer } = Layout;

const AdminLayout = ({ children }) => {
  const navigate = useNavigate();
  const menuItems = [
    {
      key: "1",
      icon: <DashboardOutlined />,
      label: "Dashboard",
      path: "/admin",
      onClick: () => navigate(`/admin`),
    },
    {
      key: "2",
      icon: <UserOutlined />,
      label: "Kullanıcı Yönetimi",
      path: "/admin/users",
      onClick: () => navigate(`/admin/users`),
    },
    {
      key: "3",
      icon: <MessageOutlined />,
      label: "Chat",
      path: "/admin/chat",
      onClick: () => navigate(`/chat`),
    }
  ];

  const getActiveKey = () => menuItems.find(item => item.path === window.location.pathname)?.key;

  const getPageTitle = () => menuItems.find(item => item.path === window.location.pathname)?.label;

  return (
    <Layout className="layout-container">
      <Sider>
        <div className="sider-logo">HSTPOS</div>
        <Menu
          theme="dark"
          mode="inline"
          items={menuItems}
          defaultSelectedKeys={[getActiveKey()]}
        />
      </Sider>
      <Layout>
        <Header>
          <div
            style={{
              display: "flex",
              justifyContent: "space-between",
              color: "white",
            }}
          >
            <h2>{getPageTitle()}</h2>
            <h2>Admin Paneli</h2>
          </div>
        </Header>
        <Content style={{ margin: "16px", padding: "24px" }}>
          {children}
        </Content>
        <Footer className="footer">
          HST Intern ©{new Date().getFullYear()} Abdülkadir Hocaoğlu
        </Footer>
      </Layout>
    </Layout>
  );
};

AdminLayout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default AdminLayout;
