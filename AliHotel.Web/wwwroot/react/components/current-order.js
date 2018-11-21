import React from 'react'
import Cookies from 'js-cookie';
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';
import DatePicker from "react-datepicker";

import 'react-datepicker/dist/react-datepicker.css';
import 'react-datepicker/dist/react-datepicker-cssmodules.css';
import 'react-taco-table/dist/react-taco-table.css';

import Modal from './modal';

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

class CurrentOrder extends React.Component {
    constructor(props) {
        super(props);
        this.closeModal = this.closeModal.bind(this);
        this.handleOpenCloseOrderModal = this.handleOpenCloseOrderModal.bind(this);
        this.handleCloseOrderSubmitClick = this.handleCloseOrderSubmitClick.bind(this);
        this.handleCloseOrderFinishedClick = this.handleCloseOrderFinishedClick.bind(this);
        this.handleOpenChangeDepDateModal = this.handleOpenChangeDepDateModal.bind(this);
        this.handleChangeDepDateClick = this.handleChangeDepDateClick.bind(this);
        this.handleNewDepartureDayPick = this.handleNewDepartureDayPick.bind(this);
        this.state = {
            current: null,
            currentFetched: false,
            currentDepartureDate: "",
            isModalOpen: false,
            isChangeDateModalRequested: false,
            isCloseOrderModalRequested: false,
            isClosingConfirmed: false,
            orderClosedResponse: "",
            newDepartureDate: (new Date()).setHours(0, 0, 0, 0),
            isChangingDepDateRequested: false,
            isDepDateChangedWithoutErrors: false,
            changeDepDateResponse: ""
        }

        fetch('/Orders/Current', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                if (response.status == 404) {
                    this.setState({ current: "NO_ACTIVE_ORDERS", currentFetched: true });
                } else {
                    response.json().then(data => {
                        delete data['id'];
                        delete data.userId;
                        delete data.room;
                        delete data.roomId;
                        this.setState({ current: data, currentDepartureDate: data["0"].departureDate, currentFetched: true });
                    })
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    componentWillUnmount() {
        this.setState({ currentFetched: false });
    }

    closeModal() {
        this.setState({
            isModalOpen: false,
            isChangeDateModalRequested: false,
            isCloseOrderModalRequested: false,
            isClosingConfirmed: false,
            orderClosedResponse: "",
            newDepartureDate: (new Date()).setHours(0, 0, 0, 0),
            isChangingDepDateRequested: false,
            isDepDateChangedWithoutErrors: false,
            changeDepDateResponse: ""
        })
    }

    handleOpenCloseOrderModal() {
        this.setState({ isCloseOrderModalRequested: true, isModalOpen: !this.state.isModalOpen, isChangeDateModalRequested: false })
    }

    handleCloseOrderSubmitClick() {
        fetch('/Orders/PayOrder', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ isClosingConfirmed: true, orderClosedResponse: data })
            })
            .catch(error => {
                console.log(error);
            })
    }

    handleCloseOrderFinishedClick() {
        this.setState({ isModalOpen: false, orderClosedResponse: "", isClosingConfirmed: false, isCloseOrderModalRequested: false, current: "NO_ACTIVE_ORDERS" });
    }

    handleOpenChangeDepDateModal() {
        this.setState({ isChangeDateModalRequested: true, isModalOpen: !this.state.isModalOpen, isCloseOrderModalRequested: false })
    }

    handleChangeDepDateClick() {
        fetch('/Orders/DepartureDay', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            },
            body: '\"' + this.state.newDepartureDate.toISOString().substring(0, 19) + '\"'//'.236\"'
        })
            .then(response => {
                if (response.status == 200) {
                    console.log(this.state.newDepartureDate.toISOString());
                    console.log(this.state.newDepartureDate);
                    this.setState({ isChangingDepDateRequested: true, isDepDateChangedWithoutErrors: true });
                } else {
                    console.log(this.state.newDepartureDate.toISOString());
                    this.setState({ isChangingDepDateRequested: true, isDepDateChangedWithoutErrors: false })
                }
                return response.json();
            })
            .then(data => {
                this.setState({ changeDepDateResponse: data });
            })
            .catch(error => {
                console.log(error);
            })
    }

    handleNewDepartureDayPick(date) {
        this.setState({ newDepartureDate: date })
    }

    handleChangeDepDateFinal() {

        this.setState({})
    }

    CurrentOrderForm(current) {
        if (current === "NO_ACTIVE_ORDERS") {
            //TODO
            return <div>
                <h2>You have not active orders</h2>
                <br />
            </div>
        }
        return <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
            <div className="container form-group" style={{ position: "relative", "marginTop": "10%" }}>
                <h1 className="display-4">Current order</h1>
            </div>

            <div className="container jumbotron form-group" style={{ "marginLeft": "10%", "maxWidth": "50%" }}>
                <ul className="list-group">
                    <li className="list-group-item d-flex justify-content-between align-items-center">
                        Arrival Date
                                    <span className="badge badge-primary badge-pill">{current["0"].arrivalDate}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between align-items-center">
                        Departure Date
                                    <span className="badge badge-primary badge-pill">{current["0"].departureDate}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between align-items-center">
                        Room Number
                                    <span className="badge badge-primary badge-pill">{current[0].number}</span>
                    </li>
                    <li className="list-group-item d-flex justify-content-between align-items-center">
                        Room Type
                                    <span className="badge badge-primary badge-pill">{current["0"].room.roomType.name}</span>
                    </li>
                </ul>

                <br />

                <div className="form-group">
                    <button type="button" onClick={this.handleOpenChangeDepDateModal} className="btn btn-primary btn-lg" style={{ "marginTop": "3%", width: "100%" }}>Change deparure date</button>
                </div>

                <div className="form-group">
                    <button type="button" onClick={this.handleOpenCloseOrderModal} className="btn btn-primary btn-lg" style={{ width: "100%" }}>Close order</button>
                </div>
            </div>
        </div>
    }

    CloseOrderModal(isClosingConfirmed) {
        if (isClosingConfirmed) {
            return <div style={{ "textAlign": "center", position: "relative" }}>
                <div className="card-header"><h5>Your bill</h5></div>

                <div className="card-body"><h5>{this.state.orderClosedResponse}</h5></div>

                <div style={{ padding: "5px", left: "50%" }}>
                    <input className="btn btn-primary" type="submit" onClick={this.handleCloseOrderFinishedClick} value="Ok" />
                </div>
            </div>
        } else {
            return <div style={{ "textAlign": "center", position: "relative" }}>
                <div className="card-header"><h5>Are you sure?</h5></div>

                <div className="card-body">{this.state.orderClosedResponse}</div>

                <div style={{ "textAlign": "center", display: "flex", flexDirection: "row" }}>
                    <div style={{ padding: "5px", width: "100%" }}>
                        <input type="button" className="btn btn-primary" type="submit" onClick={this.handleCloseOrderSubmitClick} value="Yes" />
                    </div>
                    <div style={{ padding: "5px", width: "100%" }}>
                        <input type="button" className="btn btn-primary" onClick={this.closeModal} readOnly value="No" />
                    </div>
                </div>
            </div>
        }
    }

    ChangeDepartureDayModal(isChangingDepDateRequested, isDepDateChangedWithoutErrors) {
        if (isChangingDepDateRequested) {
            if (isDepDateChangedWithoutErrors) {
                this.state.current["0"].departureDate = this.state.changeDepDateResponse;
                let dateToPrint = new Date(Date.parse(this.state.changeDepDateResponse));
                dateToPrint = dateToPrint.toString().substring(0, 16);
                return <div style={{ "textAlign": "center", position: "relative" }}>
                    <div className="card-header"><h5>Departure day changed succesfully!</h5></div>

                    <div className="card-body">
                        <h5>New departure day:</h5>
                        <h6>{dateToPrint}</h6>
                    </div>

                    <div style={{ padding: "5px", left: "50%" }}>
                        <input className="btn btn-primary" type="submit" onClick={this.closeModal} value="Ok" />
                    </div>
                </div>
            } else {
                return <div style={{ "textAlign": "center", position: "relative" }}>
                    <div className="card-header"><h5>Error occured :(</h5></div>

                    <div className="card-body">
                        <h5>Error:</h5>
                        <h6>{this.state.changeDepDateResponse}</h6>
                    </div>

                    <div style={{ padding: "5px", left: "50%" }}>
                        <input className="btn btn-primary" type="submit" onClick={this.closeModal} value="Ok" />
                    </div>
                </div>
            }

        } else {
            return <div style={{ "textAlign": "center", position: "relative" }}>
                <div className="card-header"><h5>Change departure day</h5></div>

                <div className="card-body">
                    <DatePicker
                        selected={this.state.newDepartureDate}
                        onChange={this.handleNewDepartureDayPick}
                        minDate={new Date()}
                        todayButton={"Today"}
                        showDisabledMonthNavigation
                        highlightDates={[new Date(this.state.currentDepartureDate)]}
                        placeholderText="Select new departure day"
                        className="form-control"
                    />
                </div>

                <div style={{ "textAlign": "center", display: "flex", flexDirection: "row" }}>
                    <div style={{ padding: "5px", width: "100%" }}>
                        <input className="btn btn-primary" type="submit" onClick={this.handleChangeDepDateClick} value="Change" />
                    </div>
                    <div style={{ padding: "5px", width: "100%" }}>
                        <input className="btn btn-primary" type="submit" onClick={this.closeModal} value="Cancel" />
                    </div>
                </div>
            </div>
        }
    }


    render() {
        if (this.state.currentFetched) {
            const dateFormat1 = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/;
            const dateFormat2 = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}$/;
            const dateFormat3 = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{2}Z$/;
            const dateFormat4 = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z$/;
            const dateFormat5 = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$/;

            function reviver(key, value) {
                if (typeof value === "string" && (dateFormat1.test(value) || dateFormat2.test(value) || dateFormat3.test(value) || dateFormat4.test(value) || dateFormat5.test(value))) {
                    var date = new Date(value);

                    return date.toString().substring(0, 15);
                }

                return value;
            }

            let modal;

            if (this.state.isCloseOrderModalRequested) {
                modal = this.CloseOrderModal(this.state.isClosingConfirmed);
            }

            if (this.state.isChangeDateModalRequested) {
                modal = this.ChangeDepartureDayModal(this.state.isChangingDepDateRequested, this.state.isDepDateChangedWithoutErrors);
            }

            return <div className="container body-content">
                <br />
                <main styles={{ width: "20%" }}>
                    <Modal show={this.state.isModalOpen}>
                        {modal}
                    </Modal>
                </main>

                {this.CurrentOrderForm(JSON.parse(JSON.stringify(this.state.current), reviver))}
                
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default CurrentOrder;