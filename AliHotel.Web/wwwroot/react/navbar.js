var React = require('react');

var Modal = require('./modal');

function UserGreeting(props) {
    return <h5 className="nav-item mr-sm-2">Welcome back!</h5>;
}

function GuestGreeting(props) {
    return <h5 className="nav-item mr-sm-2">Please sign up</h5>;
}

function Greeting(props) {
    const isLoggedIn = props.isLoggedIn;
    if (isLoggedIn) {
        return <UserGreeting />;
    }
    return <GuestGreeting />;
}

function SignUpButton(props) {
    return (<button className="btn btn-secondary" onClick={props.onClick}>Sign Up</button>);
}

function LoginButton(props) {
    return (<button type="button" className="btn btn-secondary" onClick={props.onClick}>Login</button>);
}

function LogoutButton(props) {
    return (<button className="btn btn-secondary my-2 my-sm-0" onClick={props.onClick}>Logout</button>);
}

function MyOrdersButton(props) {
    return (<button className="btn btn-secondary my-2 my-sm-0" onClick={props.onClick}> My Orders</button>);
}

class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.handleLoginClick = this.handleLoginClick.bind(this);
        this.handleLogoutClick = this.handleLogoutClick.bind(this);
        this.toggleLogInModal = this.toggleLogInModal.bind(this);
        this.state = {
            isLogInModalOpen: false,
            isLoggedIn: false
        };
    }

    toggleLogInModal() {
        this.setState({ isLogInModalOpen: !this.state.isLogInModalOpen });
    }

    handleLoginClick() {
        this.setState({ isLoggedIn: true, isLogInModalOpen:false });
    }

    handleLogoutClick() {
        this.setState({ isLoggedIn: false });
    }

    handleMyOrdersClick() {
        //TODO
    }

    handleSignUpClick() {
        //TODO
    }

    render() {
        const isLoggedIn = this.state.isLoggedIn;
        let button1; //register or my orders
        let button2; //login or logout

        if (isLoggedIn) {
            button1 = <MyOrdersButton onClick={this.handleMyOrdersClick} />;
            button2 = <LogoutButton onClick={this.handleLogoutClick} />;
        } else {
            button1 = <LoginButton onClick={this.toggleLogInModal} />;
            button2 = <SignUpButton onClick={this.handleSignUpClick}/>;
        }

        return (
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                    <div className="container">
                        <a className="navbar-brand" href="#">AliHotel</a>

                        <div className="collapse navbar-collapse" id="navbarColor01">
                            <ul className="navbar-nav mr-auto">
                                <li className="nav-item">
                                    <a className="nav-link" href="#">Galery <span className="sr-only">(current)</span></a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">Rooms</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">Contacts</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#">About</a>
                                </li>
                            </ul>

                            <Greeting isLoggedIn={isLoggedIn} />

                            <div className="navbar-right">
                                <ul className="nav navbar-nav navbar-right mr-auto">
                                    <li className="nav-item" style={{ padding: "5px"}}>
                                        {button1}
                                    </li>
                                    <li className="nav-item" style={{ padding: "5px"}}>
                                        {button2}
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </nav>

                <main>
                    <Modal show={this.state.isLogInModalOpen}>
                        <div className="card-header">Logging in</div>
                        
                        <form style={{padding: "20px"}}>
                            <div className="form-group">
                                <label htmlFor="email">Email address</label>
                                <input type="email" className="form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email"></input>
                                    <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>

                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <input type="password" className="form-control" id="password" placeholder="Password"></input>
                            </div>

                            <div style={{ display: "flex", flexDirection: "row" }}>
                                <div style={{ padding: "5px", left: "50%" }}>
                                    <button className="btn btn-primary" onClick={this.handleLoginClick}>Log in</button>
                                </div>
                                <div style={{ padding: "5px" }}>
                                    <button className="btn btn-primary" onClick={this.toggleLogInModal}>Cancel</button>
                                </div>
                            </div>
                        </form>
                        

                    </Modal>
                </main>
                
            </div>
        );
    }
}

module.exports = Navbar;