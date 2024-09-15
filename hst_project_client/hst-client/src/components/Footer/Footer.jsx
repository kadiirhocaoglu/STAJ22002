import "./Footer.css";
import "../../index.css";
const Footer = () => {
  return (
    <footer className="footer">
      <div className="subscribe-row">
        <div className="container">
          <div className="footer-row-wrapper">
            <div className="footer-subscribe-wrapper">
              <div className="footer-subscribe">
                <div className="footer-subscribe-top">
                  <h3 className="subscribe-title">
                    Bizden haberler ve daha fazlası için e-postalarımızı alın.
                  </h3>
                  <p className="subscribe-desc">
                    Sana özel teklifler için hemen Abone Ol
                  </p>
                </div>
                <div className="footer-subscribe-bottom">
                  <form>
                    <input
                      type="text"
                      placeholder="E-posta adresinizi girin."
                    />
                    <button className="btn">Abone Ol</button>
                  </form>
                  <p className="privacy-text">
                    Abone olarak{" "}
                    <a href="#">
                      Şartlar ve Koşullar ile Gizlilik ve Çerez Politikası
                    </a>{" "}
                    nı kabul etmiş olursunuz.
                  </p>
                </div>
              </div>
            </div>
            <div className="footer-contact-wrapper">
              <div className="footer-contact-top">
                <h3 className="contact-title">
                  Yardıma mı ihtiyacınız var? <br />
                  (+90) 123 456 78 90
                </h3>
                <p className="contact-desc">
                  8:00 - 19:00 saatleri arasında hizmetinizdeyiz
                </p>
              </div>
              <div className="footer-contact-bottom">
                <div className="download-app">
                  <a href="#">
                    <img src="img/footer/app-store.png" alt="" />
                  </a>
                  <a href="#">
                    <img src="img/footer/google-play.png" alt="" />
                  </a>
                </div>
                <p className="privacy-text">
                  <strong>HST POS:</strong> Yeni nesil pos cihazı deneyin, kayıtları yönetin ve ödeme bilgilerini
                  kaydedin.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div className="widgets-row">
        <div className="container">
          <div className="footer-widgets">
            <div className="brand-info">
              <div className="footer-logo">
                <a href="index.html" className="logo">
                  HST POS
                </a>
              </div>
              <div className="footer-desc">
                <p>
                  {" "}
                  Yeni Nesi Pos Cihazı
                </p>
              </div>
              <div className="footer-contact">
                <p>
                  <a href="tel:555 555 55 55">(+90) 1234 5678 90</a> –{" "}
                  <a href="mailto:info@example.com">info@example.com</a>
                </p>
              </div>
            </div>
            <div className="widget-nav-menu">
              <h4>Bilgilendirme</h4>
              <ul className="menu-list">
                <li>
                  <a href="#">Hakkımızda</a>
                </li>
                <li>
                  <a href="#">Gizlilik Politikası</a>
                </li>
                <li>
                  <a href="#">İade Politikası</a>
                </li>
                <li>
                  <a href="#">Teslimat Politikası</a>
                </li>
                <li>
                  <a href="#">Dropshipping</a>
                </li>
              </ul>
            </div>
            <div className="widget-nav-menu">
              <h4>Hesap</h4>
              <ul className="menu-list">
                <li>
                  <a href="#">Kontrol Paneli</a>
                </li>
                <li>
                  <a href="#">Siparişlerim</a>
                </li>
                <li>
                  <a href="#">Hesaplar</a>
                </li>
                <li>
                  <a href="#">Hesap Detayları</a>
                </li>
                <li>
                  <a href="#">Sözleşmeler</a>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
      <div className="copyright-row">
        <div className="container">
          <div className="footer-copyright">
            <div className="site-copyright">
              <p>
                Copyright 2024. Tüm hakları saklıdır. Powered by Abdulkadir
                Hocaoglu.
              </p>
            </div>
            <a href="#">
              <img src="img/footer/cards.png" alt="" />
            </a>
            <div className="footer-menu">
              <ul className="footer-menu-list">
                <li className="list-item">
                  <a href="#">Gizlilik Politikası</a>
                </li>
                <li className="list-item">
                  <a href="#">Şartlar ve Koşullar</a>
                </li>
                
              </ul>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
};
export default Footer;
