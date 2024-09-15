import "./Policy.css"
const Policy = () => {
  return (
    <section className="policy">
      <div className="container">
        <ul className="policy-list">
        <li className="policy-item">
            <i className="bi bi-headset"></i>
            <div className="policy-texts">
              <strong>7/24 DESTEK</strong>
              <span>Günün her saati online</span>
            </div>
          </li>
          <li className="policy-item">
            <i className="bi bi-truck"></i>
            <div className="policy-texts">
              <strong>Link ile Ödeme</strong>
              <span>E-Ticaretiniz için bir ödemeden daha fazlası</span>
            </div>
          </li>
          <li className="policy-item">
            <i className="bi bi-arrow-clockwise"></i>
            <div className="policy-texts">
              <strong>Kart ile Ödeme</strong>
              <span>Kart ile kolayca ödeme alın</span>
            </div>
          </li>
          <li className="policy-item">
            <i className="bi bi-credit-card"></i>
            <div className="policy-texts">
              <strong>Temassız Ödeme</strong>
              <span>Temassız özelliğini kullanarak hızlıca ödeme alın.</span>
            </div>
          </li>
        </ul>
      </div>
    </section>
  )
}

export default Policy
