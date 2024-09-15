import "../Auth.css";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const Register = () => {
  const [formData, setFormData] = useState({
    name: "",
    surname: "",
    email: "",
    phoneNumber: "",
    webSiteAddress: "",
    password: "",
    role: 2, 
    roles: [] 
  });

  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.post('http://localhost:5121/api/User', formData);
      navigate("/login");
    } catch (error) {
      console.error("Registration error:", error);
      // Display appropriate error message here
    }
  };

  return (
    <section className="account-page">
      <div className="container">
        <div className="account-wrapper">
          <div className="account-column">
            <h2>Register</h2>
            <form onSubmit={handleSubmit}>
              <div>
                <label>
                  <span>Username <span className="required">*</span></span>
                  <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleInputChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <span>Surname <span className="required">*</span></span>
                  <input
                    type="text"
                    name="surname"
                    value={formData.surname}
                    onChange={handleInputChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <span>Email <span className="required">*</span></span>
                  <input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleInputChange}
                    required
                  />
                </label>
              </div>
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
                  <span>Website Address</span>
                  <input
                    type="text"
                    name="webSiteAddress"
                    value={formData.webSiteAddress}
                    onChange={handleInputChange}
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
              <div className="privacy-policy-text remember">
                <p>
                  Your personal data will be used to support your experience
                  throughout this website, to manage access to your account, and
                  for other purposes described in our{" "}
                  <a href="#">privacy policy.</a>
                </p>
                <button className="btn btn-sm" type="submit">Register</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Register;
