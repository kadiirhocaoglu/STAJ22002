import "./Header.css";
import "../../index.css";
import { Link, useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode"; 

const Header = () => {
  const navigate = useNavigate();

  const token = localStorage.getItem("token");
  let userRole = null;
  let userFullName = null;
  if (token) {
    try {
      const decodedToken = jwtDecode(token);
      userRole = decodedToken.role;
      userFullName = decodedToken.name;
      console.log(userFullName);
    } catch (error) {
      console.error("Error decoding token", error);
    }
  }

  // Handle navigation based on user role
  const handleAdminClick = () => {
    if (userRole === "Superadmin") {
      navigate("/admin");
    } else if (userRole === "OperationSupport") {
      navigate("/chat");
    } else if (userRole === "User") {
      navigate("/");
    }
  };

  return (
    <header>
      <div className="global-notification">
        <div className="container">
          <p>HER HARCAMANDA %3 CASHBACK HST KARTDA <a href="shop.html">HEMEN HST KARTA BAŞVUR</a></p>
          
          <p>Hoşgeldin <a>{userFullName}</a></p>
          
        </div>
      </div>
      <div className="header-row">
        <div className="container">
          <div className="header-wrapper">
            <div className="header-mobile">
              <i className="bi bi-list" id="btn-menu"></i>
            </div>
            <div className="header-left">
              <a href="/" className="logo">
                HST POS
              </a>
            </div>
            <div className="header-center" id="sidebar">
              <nav className="navigation">
                <ul className="menu-list">
                  <li className="menu-list-item">
                    <a href="index.html" className="menu-link active">
                      ANASAYFA
                      <i className="bi bi-chevron-down"></i>
                    </a>
                    <div className="menu-dropdown-wrapper">
                      <ul className="menu-dropdown-content">
                        <li>
                          <a href="#">Bireysel POS Başvurusu</a>
                        </li>
                        <li>
                          <a href="#">Kurumsal POS Başvurusu</a>
                        </li>
                        <li>
                          <a href="#">HST POS</a>
                        </li>
                        <li>
                          <a href="#">HST Wallet</a>
                        </li>
                        <li>
                          <a href="#">Komisyonlar</a>
                        </li>
                        <li>
                          <a href="#">Blog</a>
                        </li>
                      </ul>
                    </div>
                  </li>
                  <li className="menu-list-item megamenu-wrapper">
                    <a href="shop.html" className="menu-link">
                      BİREYSEL
                      <i className="bi bi-chevron-down"></i>
                    </a>
                    <div className="menu-dropdown-wrapper">
                      <div className="menu-dropdown-megamenu">
                        <div className="megamenu-links">
                          <div className="megamenu-products">
                            <h3 className="megamenu-products-title">
                              BİREYSEL
                            </h3>
                            <ul className="megamenu-menu-list">
                              <li>
                                <a href="#">Bireysel POS Başvurusu</a>
                              </li>
                              <li>
                                <a href="#">HST POS Mobil</a>
                              </li>
                              <li>
                                <a href="#">Komisyonlar</a>
                              </li>
                              <li>
                                <a href="#">Sana Özel Teklif</a>
                              </li>
                            </ul>
                          </div>
                        </div>
                        <div className="megamenu-single">
                          <a href="#">
                            <img
                              src="img/mega-menu.png"
                              alt=""
                              className="megamenu-image"
                            />
                          </a>
                          <h3 className="megamenu-single-title">HST POS</h3>
                          <h4 className="megamenu-single-subtitle">
                            YENİ NESİL POS CİHAZI
                          </h4>
                          <a
                            href="#"
                            className="megamenu-single-button btn btn-sm"
                          >
                            HEMEN BAŞVUR
                          </a>
                        </div>
                      </div>
                    </div>
                  </li>
                  <li className="menu-list-item megamenu-wrapper">
                    <a href="shop.html" className="menu-link">
                      KURUMSAL
                      <i className="bi bi-chevron-down"></i>
                    </a>
                    <div className="menu-dropdown-wrapper">
                      <div className="menu-dropdown-megamenu">
                        <div className="megamenu-links">
                          <div className="megamenu-products">
                            <h3 className="megamenu-products-title">
                              KURUMSAL
                            </h3>
                            <ul className="megamenu-menu-list">
                              <li>
                                <a href="#">Kurumsal POS Başvurusu</a>
                              </li>
                              <li>
                                <a href="#">HST POS Mobil</a>
                              </li>
                              <li>
                                <a href="#">Komisyonlar</a>
                              </li>
                              <li>
                                <a href="#">Size Özel Teklif</a>
                              </li>
                            </ul>
                          </div>
                        </div>
                        <div className="megamenu-single">
                          <a href="#">
                            <img
                              src="img/mega-menu.png"
                              alt=""
                              className="megamenu-image"
                            />
                          </a>
                          <h3 className="megamenu-single-title">HST POS</h3>
                          <h4 className="megamenu-single-subtitle">
                            YENİ NESİL POS CİHAZI
                          </h4>
                          <a
                            href="#"
                            className="megamenu-single-button btn btn-sm"
                          >
                            HEMEN BAŞVUR
                          </a>
                        </div>
                      </div>
                    </div>
                  </li>
                  <li className="menu-list-item">
                    <a href="blog.html" className="menu-link">
                      Blog
                    </a>
                  </li>
                  <li className="menu-list-item">
                    <a href="contact.html" className="menu-link">
                      İLETİŞİM
                    </a>
                  </li>
                </ul>
              </nav>
              <i className="bi-x-circle" id="close-sidebar"></i>
            </div>
            <div className="header-right">
  {token ? (
    userRole !== "User" && (
      <button onClick={handleAdminClick} className="button-colored">
        {userRole === "Superadmin" || "OperationSupport" ? "Admin Paneli" : ""}
      </button>
    )
  ) : (
    <div className="button-container">
      <Link to="/register" className="button-transparent">
        Kayıt Ol
      </Link>
      <Link to="/login" className="button-colored">
        Giriş Yap
      </Link>
    </div>
  )}
</div>
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
