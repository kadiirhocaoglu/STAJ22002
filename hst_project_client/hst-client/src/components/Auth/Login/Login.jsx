import "../Auth.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import {jwtDecode} from "jwt-decode"; 


const api_url = "http://localhost:5121";

const Login = () => {
  const [formData, setFormData] = useState({
    phoneNumber: "",
    password: "",
    rememberMe: false
  });
  
  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(`${api_url}/api/Auth/login`, formData);
      const token = response.data.token.result;
      console.log("TOKEN:", token);

      if (typeof token === 'string') {
        localStorage.setItem("token", token); 
        const decodedToken = jwtDecode(token);
        console.log("Decoded token:", decodedToken);

        const userRole = decodedToken.role; 
        console.log("User role:", userRole);

        if (userRole === "Superadmin") {
          navigate("/admin");
        } else {
          navigate("/chat");
        }
      } else {
        console.error("Invalid token format. Expected a string.");
      }

    } catch (error) {
      console.error("Login error:", error);
    }
  };

  return (
    <section className="account-page">
      <div className="container">
        <div className="account-wrapper">
          <div className="account-column">
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
              <div>
                <label>
                  <span>Phone Number <span className="required">*</span></span>
                  <input
                    type="text"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleInputChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <span>Password <span className="required">*</span></span>
                  <input
                    type="password"
                    name="password"
                    value={formData.password}
                    onChange={handleInputChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <input
                    type="checkbox"
                    name="rememberMe"
                    checked={formData.rememberMe}
                    onChange={handleInputChange}
                  />
                  <span>Remember Me</span>
                </label>
              </div>
              <div className="privacy-policy-text remember">
                <button className="btn btn-sm" type="submit">Login</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Login;
