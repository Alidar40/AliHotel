import React from 'react';
import { handleLogin } from '../store/actions/authentication-actions'

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
        this.state = {
            email: "",
            password: ""
        };
    }

    componentWillUnmount() {
        //this.setState({ password: "" });
    }

    handleEmailChange(event) {
        console.log(this.state.email)
        this.setState({ email: event.target.value });
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handleLoginClick(event) {
        /*fetch('/Account/Login', {
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
            }.bind(this));*/
        //console.log(this.state);
        //handleLogin(this.state.email, this.state.password);
        const { dispatch } = this.props;
        event.preventDefault();
        dispatch(this.props.handleLogin(this.state.email, this.state.password));

    }


    render() {
        return <form onSubmit={this.handleLoginClick} style={{ padding: "20px" }}>
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
            
                    <div style={{ padding: "5px", width: "100%" }}>
                        <input className="btn btn-primary" type="submit" value="Log in" />
                    </div>
                </form>
    }
}

export default Login