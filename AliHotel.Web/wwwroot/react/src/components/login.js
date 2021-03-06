﻿import React from 'react';

function LoginFailSpan(props) {
    const isLoginRequestFailed = props.isLoginRequestFailed;
    if (isLoginRequestFailed) {
        return <label className="text-danger">Incorrect e-mail or password</label>;
    }
    return <div></div>;
}

class Login extends React.Component {
    constructor(props) {
        super(props);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handleLoginClick = this.handleLoginClick.bind(this);
        this.handleRegisterClick = this.handleRegisterClick.bind(this);
        this.state = {
            email: "",
            password: ""
        };
    }

    handleEmailChange(event) {
        this.setState({ email: event.target.value });
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handleLoginClick(event) {
        const { dispatch } = this.props;
        event.preventDefault();
        dispatch(this.props.handleLogin(this.state.email, this.state.password));
    }

    handleRegisterClick(event) {
        this.props.history.push("/Register")
    }

    render() {
        return <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
            <div className="container form-group" style={{ position: "relative", "marginTop": "10%" }}>
                <h1 >Welcome to AliHotel!</h1>
            </div>
            <div className="container jumbotron form-group" style={{ "marginLeft": "10%", "maxWidth": "50%" }}>
                <form onSubmit={this.handleLoginClick} style={{ padding: "20px" }}>
                    <LoginFailSpan isLoginRequestFailed={this.props.isLoginRequestFailed} />

                    <div className="form-group">
                        <label htmlFor="email">Email address</label>
                        <input type="email" value={this.state.email} onChange={this.handleEmailChange} className="form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email"></input>
                        <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                    </div>

                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input type="password" value={this.state.password} onChange={this.handlePasswordChange} className="form-control" id="password" placeholder="Password"></input>
                    </div>

                    <input style={{ width: "100%" }} className="btn btn-primary " type="submit" value="Log in" />
                    <hr />
                    <small className="form-text text-muted">Don't have an account? So create!</small>
                    <input onClick={this.handleRegisterClick} type="button" style={{ width: "100%" }} className="btn btn-primary btn-sm" value="Register" />
                </form>
            </div>
        </div>
    }
}

export default Login