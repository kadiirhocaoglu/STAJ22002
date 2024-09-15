import { useEffect, useState } from 'react';
import {
  Button,
  Form,
  Input,
  Select,
} from 'antd';
import { useParams } from 'react-router-dom';
import axios from 'axios'; 

const UserUpdate = () => {
  const [componentSize, setComponentSize] = useState('default');
  const [form] = Form.useForm();
  const { userId } = useParams(); 
  const [loading, setLoading] = useState(false);
  const [roles, setRoles] = useState([]);

  useEffect(() => {
    
    if (userId) {
      setLoading(true);
      axios.get(`http://localhost:5121/api/User/${userId}`)
        .then(response => {
          form.setFieldsValue(response.data);
          setLoading(false);
        })
        .catch(error => {
          console.error('There was an error fetching the user data:', error);
          setLoading(false);
        });
    }

   
    axios.get('http://localhost:5121/api/User/GetAllRoles')
      .then(response => {
        setRoles(response.data); 
      })
      .catch(error => {
        console.error('Error fetching roles:', error);
      });
  }, [userId, form]);

  const onFormLayoutChange = ({ size }) => {
    setComponentSize(size);
  };

  const handleSubmit = (values) => {
    console.log('Form values:', values);
    axios.put(`http://localhost:5121/api/User/${values.id}`, values)
      .then(response => {
        console.log('User updated successfully:', response.data);
       
      })
      .catch(error => {
        console.error('There was an error updating the user:', error);
      });
  };

  return (
    <Form
      form={form}
      labelCol={{ span: 6 }}
      wrapperCol={{ span: 14 }}
      layout="horizontal"
      initialValues={{
        size: componentSize,
        id: 0,
        name: '',
        surname: '',
        email: '',
        phoneNumber: '',
        webSiteAddress: '',
        role: '',
      }}
      onValuesChange={onFormLayoutChange}
      size={componentSize}
      style={{ maxWidth: 800 }}
      onFinish={handleSubmit} 
    >
      <Form.Item label="Name" name="name">
        <Input />
      </Form.Item>
      <Form.Item label="Surname" name="surname">
        <Input />
      </Form.Item>
      <Form.Item label="Email" name="email">
        <Input />
      </Form.Item>
      <Form.Item label="Phone Number" name="phoneNumber">
        <Input />
      </Form.Item>
      <Form.Item label="Website Address" name="webSiteAddress">
        <Input />
      </Form.Item>
      <Form.Item label="Role" name="role">
        <Select options={roles.map(role => ({ value: role.name, label: role.name }))} />
      </Form.Item>

      <Form.Item wrapperCol={{ offset: 6, span: 14 }}>
        <Button type="primary" htmlType="submit" loading={loading}>
          Submit
        </Button>
      </Form.Item>
    </Form>
  );
};

export default UserUpdate;
