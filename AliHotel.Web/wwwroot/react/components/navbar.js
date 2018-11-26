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

function LogoutButton(props) {
    return (<button className="btn btn-secondary my-2 my-sm-0" onClick={props.onClick}>Logout</button>);
}

function MyOrdersButton(props) {
    return (<NavLink to="/MyOrders" exact ><button className="btn btn-secondary my-2 my-sm-0">My Orders</button></NavLink>);
}

export class Navbar extends React.Component {
    constructor(props) {
        super(props);
        this.handleLogoutClick = this.handleLogoutClick.bind(this);
    }
    
    handleLogoutClick() {
        this.props.dispatch(this.props.handleLogout())
        this.props.history.push("/Login")
    }

    render() {
        const isLoggedIn = this.props.isLoggedIn;
        const isLoginRequestFailed = this.props.error;
        const name = this.props.name;
        let button1;
        let button2;

        if (this.props.isLoggedIn) {
            button1 = <MyOrdersButton/>;
            button2 = <LogoutButton onClick={this.handleLogoutClick} />;
        }

        return (
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                    <div className="container">
                        <NavLink to="/" exact className="navbar-brand">AliHotel</NavLink>

                        <div className="collapse navbar-collapse" id="navbarColor01">
                            <ul className="navbar-nav mr-auto">
                            </ul>

                            <Greeting isLoggedIn={this.props.isLoggedIn} name={this.props.name} />

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
            </div>
        );
    }
}

export default Navbar;