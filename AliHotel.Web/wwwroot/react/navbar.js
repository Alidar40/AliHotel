import React from 'react';

import Modal from './modal';
import Cookies from 'js-cookie';
import { NavLink } from "react-router-dom";

function UserGreeting(props) {
    return <h5 className="nav-item mr-sm-2">Hello, {props.name}!</h5>;
}

function GuestGreeting(props) {
    return <h5 className="nav-item mr-sm-2">Please sign up</h5>;
}

function Greeting(props) {
    const isLoggedIn = props.isLoggedIn ;
    if (isLoggedIn) {
        return <UserGreeting name={props.name} />;
    }
    return <GuestGreeting />;
}

function LoginFailSpan(props) {
    const isLoginRequestFailed = props.isLoginRequestFailed;
    if (isLoginRequestFailed) {
        return <label className="text-danger">Incorrect e-mail or password</label>;
    }
    return <div></div>;
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
    return (<button className="btn btn-secondary my-2 my-sm-0"><NavLink to="/MyOrders" exact style={{ color: "white", "textDecoration": "none" }}>My Orders</NavLink></button>);
}

class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.handleLoginClick = this.handleLoginClick.bind(this);
        this.handleLogoutClick = this.handleLogoutClick.bind(this);
        this.toggleLogInModal = this.toggleLogInModal.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.state = {
            isLogInModalOpen: false,
            isLoggedIn: false,
            isLoginRequestFailed: false,
            name: "",
            email: "",
            password: ""
        };
    }

    toggleLogInModal() {
        this.setState({ isLogInModalOpen: !this.state.isLogInModalOpen });
    }

    handleEmailChange(event) {
        this.setState({ email: event.target.value });
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handleLoginClick(event) {
        fetch('/Account/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email: this.state.email,
                password: this.state.password,
            })
        })
        .then(response => {
            if (response.status == 200) {
                response.json().then(json => {
                    this.setState({ name: json, isLoggedIn: true, isLogInModalOpen: false, isLoginRequestFailed: false });
                });
                return;
            }
            return error;
        })
        .catch(function (error) {
            this.setState({ isLoginRequestFailed: true });
        }.bind(this));
        
        event.preventDefault();
    }

    handleLogoutClick() {
        fetch('/Account', {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
        this.setState({ isLoggedIn: false });
    }

    handleSignUpClick() {
        //TODO
    }

    render() {
        const isLoggedIn = this.state.isLoggedIn;
        const isLoginRequestFailed = this.state.isLoginRequestFailed;
        const name = this.state.name;
        let button1; //register or my orders
        let button2; //login or logout

        if (isLoggedIn) {
            button1 = <MyOrdersButton/>;
            button2 = <LogoutButton onClick={this.handleLogoutClick} />;
        } else {
            button1 = <LoginButton onClick={this.toggleLogInModal} />;
            button2 = <SignUpButton onClick={this.handleSignUpClick} />;
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

                            <Greeting isLoggedIn={isLoggedIn} name={name} />

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
                        
                        <form onSubmit={this.handleLoginClick} style={{ padding: "20px" }}>
                            <LoginFailSpan isLoginRequestFailed={isLoginRequestFailed} />
                            
                            <div className="form-group">
                                <label htmlFor="email">Email address</label>
                                <input type="email" value={this.state.email} onChange={this.handleEmailChange} className="form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email"></input>
                                    <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                            </div>

                            <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <input type="password" value={this.state.password} onChange={this.handlePasswordChange} className="form-control" id="password" placeholder="Password"></input>
                            </div>

                            <div style={{ display: "flex", flexDirection: "row" }}>
                                <div  style={{ padding: "5px", left: "50%" }}>
                                    <input className="btn btn-primary" type="submit" value="Log in"/>
                                </div>
                                <div style={{ padding: "5px" }}>
                                    <input type="button" className="btn btn-primary" onClick={this.toggleLogInModal} readOnly value="Cancel" />
                                </div>
                            </div>
                        </form>
                        

                    </Modal>
                </main>
                
            </div>
        );
    }
}

export default Navbar;