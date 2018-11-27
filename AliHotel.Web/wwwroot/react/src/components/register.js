import React from 'react';
import DatePicker from "react-datepicker";
import Cookies from 'js-cookie';
import { NavLink } from "react-router-dom";

Date.prototype.toISOString = function () {
    var tzo = -this.getTimezoneOffset(),
        dif = tzo >= 0 ? '+' : '-',
        pad = function (num) {
            var norm = Math.floor(Math.abs(num));
            return (norm < 10 ? '0' : '') + norm;
        };
    return this.getFullYear() +
        '-' + pad(this.getMonth() + 1) +
        '-' + pad(this.getDate()) +
        'T' + pad(this.getHours()) +
        ':' + pad(this.getMinutes()) +
        ':' + pad(this.getSeconds()) +
        dif + pad(tzo / 60) +
        ':' + pad(tzo % 60);
}

class Register extends React.Component {
    constructor(props) {
        super(props);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
        this.handlePhoneNumberChange = this.handlePhoneNumberChange.bind(this);
        this.handleBirthDateChange = this.handleBirthDateChange.bind(this);
        this.handleCreditCardChange = this.handleCreditCardChange.bind(this);
        
        this.handleRegisterClick = this.handleRegisterClick.bind(this);

        this.RegistrationForm = this.RegistrationForm.bind(this);
        this.ConfirmationSentForm = this.ConfirmationSentForm.bind(this);
        this.state = {
            name: "",
            email: "",
            password: "",
            phoneNumber: "",
            birthDate: new Date("01/01/1990"),
            creditCard: "",

            isConfirmationSent:false
        };
    }

    handleNameChange(event) {
        this.setState({ name: event.target.value });
    }

    handleEmailChange(event) {
        this.setState({ email: event.target.value });
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value });
    }

    handlePhoneNumberChange(event) {
        this.setState({ phoneNumber: event.target.value });
    }

    handleBirthDateChange(date) {
        this.setState({ birthDate: new Date(date) });
    }

    handleCreditCardChange(event) {
        this.setState({ creditCard: event.target.value });
    }
    
    handleRegisterClick(event) {
        event.preventDefault();
        fetch('/api/Account/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "name": this.state.name,
                "email": this.state.email,
                "password": this.state.password,
                "phoneNumber": this.state.phoneNumber,
                "birthDate": this.state.birthDate.toISOString().substring(0, 10),
                "creditCard": this.state.creditCard
            })
        })
            .then(response => {
                if (response.status === 200) {
                    this.setState({ isConfirmationSent: true})
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    RegistrationForm() {
        const maxDate = new Date((new Date).setUTCHours(0, 0, 0, 0))
        maxDate.setFullYear(maxDate.getFullYear() - 18)
        return <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
            <div className="container form-group" style={{ position: "relative", "marginTop": "10%" }}>
                <h1 >Create free account</h1>
            </div>
            <div className="container jumbotron form-group" style={{ "marginLeft": "10%", "maxWidth": "50%" }}>
                <form onSubmit={this.handleRegisterClick} style={{ padding: "20px" }}>

                    <div className="form-group">
                        <input required value={this.state.name} onChange={this.handleNameChange} type="text" className="form-control" placeholder="Your name" />
                    </div>

                    <div className="form-group">
                        <input required type="email" value={this.state.email} onChange={this.handleEmailChange} className="form-control" aria-describedby="emailHelp" placeholder="Enter email"></input>
                        <small className="form-text text-muted">We'll never share your email with anyone else.</small>
                    </div>

                    <div className="form-group">
                        <input required type="password" value={this.state.password} onChange={this.handlePasswordChange} className="form-control" placeholder="Password"></input>
                    </div>

                    <div className="form-group">
                        <input required value={this.state.phoneNumber} onChange={this.handlePhoneNumberChange} type="number" className="form-control" placeholder="Phone number" />
                    </div>

                    <div className="form-group" style={{ display: "flex", "flexDirection": "row", width: "100%" }}>
                        <label style={{ "marginRight": "35%" }}>Birthdate</label>
                        <div style={{ display: "flex", "flexDirection": "row", width: "100%" }}>
                            <DatePicker
                                selected={this.state.birthDate}
                                onChange={this.handleBirthDateChange}
                                showMonthDropdown
                                showYearDropdown
                                dropdownMode="select"
                                maxDate={maxDate}
                                required={true}
                                placeholderText="Select your birth day"
                                className="form-control"
                            />
                        </div>
                    </div>

                    <div className="form-group">
                        <input required value={this.state.creditCard} onChange={this.handleCreditCardChange} type="number" className="form-control" placeholder="Credir card" />
                    </div>

                    <input style={{ width: "100%" }} className="btn btn-primary " type="submit" value="Create account" />
                </form>
            </div>
        </div>
    }

    ConfirmationSentForm() {
        return <div className="container jumbotron form-group" style={{background: "white"}}>
            <h3>Check your email for confirmation</h3>
            <br />
            <NavLink to="/Login" exact><button className="btn btn-info">Login</button></NavLink>
        </div>
    }

    render() {
        if (this.state.isConfirmationSent) {
            return <div>{this.ConfirmationSentForm()}</div>
        } else {
            return <div>{this.RegistrationForm()}</div>
        }
    }
}

export default Register