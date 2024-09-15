import { Space, Table, Tag, Button } from 'antd';
import { useEffect, useState } from 'react';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';

const { Column } = Table;

const UsersPage = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 7;

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await fetch(`http://localhost:5121/api/User?page=${currentPage}&pageSize=${pageSize}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const result = await response.json();
        setData(result);
      } catch (error) {
        console.error('There was an error fetching the users:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [currentPage]);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const navigate = useNavigate();

  const handleUpdate = (userId) => {
    navigate(`/admin/userupdate/${userId}`);
  };

  const handleDelete = (userId) => {
    console.log('Delete user with ID:', userId);
  };

  return (
    <Table
      dataSource={data}
      rowKey="id"
      loading={loading}
      pagination={{
        current: currentPage,
        pageSize: pageSize,
        onChange: handlePageChange,
        showSizeChanger: false, 
      }}
    >
      <Column title="Name" dataIndex="name" key="name" />
      <Column title="Surname" dataIndex="surname" key="surname" />
      <Column title="Email" dataIndex="email" key="email" />
      <Column title="Phone Number" dataIndex="phoneNumber" key="phoneNumber" />
      <Column title="Website" dataIndex="webSiteAddress" key="webSiteAddress" />
      <Column
        title="User Confirmed"
        dataIndex="userConfirmed"
        key="userConfirmed"
        render={(userConfirmed) => (
          <Tag color={userConfirmed ? 'green' : 'volcano'}>
            {userConfirmed ? 'Confirmed' : 'Not Confirmed'}
          </Tag>
        )}
      />
      <Column title="Role" dataIndex="role" key="role" />
      <Column
        title="Action"
        key="action"
        render={(text, record) => (
          <Space size="middle">
            <Button 
              icon={<EditOutlined />} 
              onClick={() => handleUpdate(record.id)}
              type="primary"
            />
            <Button 
              icon={<DeleteOutlined />} 
              onClick={() => handleDelete(record.id)}
              type="danger"
            />
          </Space>
        )}
      />
    </Table>
  );
};

export default UsersPage;
