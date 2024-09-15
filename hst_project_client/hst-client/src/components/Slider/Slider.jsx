import "./Slider.css";

const Slider = () => {
  return (
    <section className="slider">
      <div className="slider-elements">
        <div className="slider-item fade">
          <div className="slider-image">
            <img src="img/slider/slider1.png" className="img-fluid" alt="" />
          </div>
           
          <div className="container-slider">
            <div className="download-app">
              <a href="#">
                <img src="img/footer/app-store.png" alt="" />
              </a>
              <a href="#">
                <img src="img/footer/google-play.png" alt="" />
              </a>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};
export default Slider;
