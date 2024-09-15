import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Input, Button, Divider, message, Spin } from 'antd';
import { SendOutlined } from '@ant-design/icons';
import axios from 'axios';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

const ChatPage = () => {
  const { currentUserId, recipientUserId } = useParams();
  const [messages, setMessages] = useState([]);
  const [inputValue, setInputValue] = useState('');
  const [loading, setLoading] = useState(false);
  const [sending, setSending] = useState(false);
  const [hubConnection, setHubConnection] = useState(null);

  useEffect(() => {
    const fetchMessages = async () => {
      setLoading(true);
      try {
        const response = await axios.get(
          `http://localhost:5121/api/Chat/chats?userId=${currentUserId}&toUserId=${recipientUserId}`
        );
        const transformedMessages = response.data.map((msg) => ({
          text: msg.message,
          type: msg.userId === parseInt(currentUserId) ? 'sent' : 'received',
        }));
        setMessages(transformedMessages);
      } catch (error) {
       
      } finally {
        setLoading(false);
      }
    };





    fetchMessages();
  }, [currentUserId, recipientUserId]);

  useEffect(() => {
    const connectToHub = async (reconnectAttempts = 0) => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl('http://localhost:5121/chat-hub')
          .configureLogging(LogLevel.Information)
          .build();
  
        connection.on('ReceiveMessage', (userId, receivedMessage) => {
          setMessages((prevMessages) => [
            ...prevMessages,
            {
              text: receivedMessage,
              type: userId === parseInt(currentUserId) ? 'sent' : 'received',
            },
          ]);
        });
  
        await connection.start();
        console.log('SignalR bağlantısı kuruldu');
        setHubConnection(connection);
  
        await connection.invoke('Connect', parseInt(currentUserId));
  
        connection.onclose(async () => {
          if (reconnectAttempts < 5) {  
            console.warning('Bağlantı kesildi, tekrar bağlanılıyor...');
            setTimeout(() => connectToHub(reconnectAttempts + 1), 10000); 
          } else {
          }
        });
      } catch (error) {

      }
    };
  
    connectToHub();
  
    return () => {
      if (hubConnection) {
        hubConnection.stop();
      }
    };
  }, [currentUserId, recipientUserId, hubConnection]);
  
    
  const handleSend = async () => {
    if (!inputValue.trim()) {
      message.error('Boş mesaj gönderilemez');
      return;
    }

    if (!currentUserId || !recipientUserId || isNaN(currentUserId) || isNaN(recipientUserId)) {
      message.error('Geçersiz kullanıcı ID\'leri');
      return;
    }

    setSending(true);

    try {
      const newMessage = { text: inputValue, type: 'sent' };
      setMessages((prevMessages) => [...prevMessages, newMessage]);

      await hubConnection.invoke('SendMessage', inputValue, parseInt(recipientUserId));

      setInputValue('');  
    } catch (error) {
 
    } finally {
      setSending(false);
    }
  };

  return (
    <div style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
      <Divider orientation="left">HST Canlı Destek</Divider>
      <div style={{ flex: 1, overflowY: 'auto', padding: '10px', background: '#ffffff' }}>
        {loading ? (
          <Spin size="small" style={{ display: 'block', margin: '10px 0' }} />
        ) : (
          messages.map((message, index) => (
            <div
              key={index}
              style={{
                display: 'flex',
                justifyContent: message.type === 'sent' ? 'flex-end' : 'flex-start',
                marginBottom: '10px',
              }}
            >
              <div
                style={{
                  maxWidth: '60%',
                  padding: '10px',
                  borderRadius: '10px',
                  background: message.type === 'sent' ? '#d4edda' : '#f1f1f1',
                  color: message.type === 'sent' ? '#155724' : '#000',
                  wordBreak: 'break-word',
                }}
              >
                {message.text}
              </div>
            </div>
          ))
        )}
      </div>
      <div style={{ padding: '10px', background: '#ffffff', borderTop: '1px solid #d9d9d9' }}>
        <Input
          value={inputValue}
          onChange={(e) => setInputValue(e.target.value)}
          placeholder="Mesajınızı yazın..."
          style={{ marginBottom: '10px', width: 'calc(100% - 88px)', display: 'inline-block' }}
          disabled={sending || loading}
        />
        <Button
          type="primary"
          icon={<SendOutlined />}
          onClick={handleSend}
          style={{ width: '80px', marginLeft: '8px' }}
          loading={sending}
        >
          Gönder
        </Button>
      </div>
    </div>
  );
};

export default ChatPage;
