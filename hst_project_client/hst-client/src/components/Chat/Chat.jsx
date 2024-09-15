import { useState, useEffect } from 'react';
import axios from 'axios';
import PropTypes from 'prop-types';


const Chat = ({ selectedUser }) => {
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');

  useEffect(() => {
    if (selectedUser) {
      const fetchMessages = async () => {
        try {
          const response = await axios.get(`/api/Chat/${selectedUser.id}`);
          setMessages(response.data);
        } catch (error) {
          console.error('Mesajları alırken hata:', error);
        }
      };

      fetchMessages();
    }
  }, [selectedUser]);

  const sendMessage = async () => {
    if (newMessage.trim()) {
      const payload = {
        userId: selectedUser.id,
        message: newMessage
      };

      try {
        await axios.post('/api/Chat', payload);
        setMessages([...messages, payload]);
        setNewMessage('');
      } catch (error) {
        console.error('Mesaj gönderirken hata:', error);
      }
    }
  };

  return (
    <div className="chat">
      <h3>{selectedUser ? `${selectedUser.name} ile Sohbet` : 'Bir kullanıcı seçin'}</h3>
      <div className="chat-messages">
        {messages.map((msg, index) => (
          <div key={index} className="chat-message">
            <span>{msg.message}</span>
          </div>
        ))}
      </div>
      {selectedUser && (
        <div className="chat-input">
          <input
            type="text"
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            placeholder="Mesajınızı yazın..."
          />
          <button onClick={sendMessage}>Gönder</button>
        </div>
      )}
    </div>
  );
};
Chat.propTypes = {
  selectedUser: PropTypes.shape({
    id: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
  }),
};

export default Chat;
