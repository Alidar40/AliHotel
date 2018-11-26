import React from 'react';

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = {}
    }

    render() {
        return <div>
                    <div id="Galery" className="container">
                        <h1>Galery</h1>
                        <hr />
                        <div id="Galery" className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
                            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                                <ol className="carousel-indicators">
                                    <li data-target="#carouselExampleIndicators" data-slide-to="0" className="active"></li>
                                    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                                    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                                    <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
                                </ol>
                                <div className="carousel-inner">
                                    <div className="carousel-item active">
                                        <img className="d-block w-100 thumbnail" src="https://pp.userapi.com/c851136/v851136835/50477/Fbf6cYQr-Rg.jpg" alt="hotel1" />
                                    </div>
                                    <div className="carousel-item">
                                        <img class="d-block w-100 thumbnail" src="https://pp.userapi.com/c851136/v851136835/50481/qfCExhzUNTA.jpg" alt="hotel2" />
                                    </div>
                                    <div className="carousel-item">
                                        <img className="d-block w-100 thumbnail" src="https://pp.userapi.com/c851136/v851136835/5048b/bNtgL_FcwZc.jpg" alt="room2" />
                                    </div>
                                    <div className="carousel-item">
                                        <img className="d-block w-100 thumbnail" src="https://pp.userapi.com/c851136/v851136835/50495/xaCSz_Ew_t0.jpg" alt="room2" />
                                    </div>
                                </div>
                                <a className="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span className="sr-only">Previous</span>
                                </a>
                                <a className="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span className="sr-only">Next</span>
                                </a>
                            </div>
                        </div>
                    </div>
                    
                    

                    <div id="About" class="container jumbotron form-group" style={{ background: "white" }}>
                        <h1>AliHotel by Alidar Asvarov</h1>
                        <hr />
                        <h3 className="text-danger">This site was made only for educational purposes</h3>
                        <h4>Everything is imagined</h4>
                        <br/>
                        <ul>Made with:
                            <li>ASP.NET Core 2.0</li>
                            <li>React, Bootstrap 4</li>
                            <li>PostgreSQL</li>
                        </ul>

                        <ul>Links:
                            <li><a href="https://github.com/Alidar40/AliHotel">GitHub</a></li>
                            <li><a href="https://bootswatch.com/minty/">Site theme</a></li>
                        </ul>
                    </div>
                </div> 
    }
}

export default Home;