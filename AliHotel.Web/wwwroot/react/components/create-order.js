import React from 'react'
import Cookies from 'js-cookie';
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';
import DatePicker from "react-datepicker";

import 'react-datepicker/dist/react-datepicker.css';
import 'react-datepicker/dist/react-datepicker-cssmodules.css';
import 'react-taco-table/dist/react-taco-table.css';

import Modal from './modal';
import { formatJsonDateToUTC } from '../utils/date';

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

function CreateOrderFailSpan(props) {
    const isCreateRequestFailed = props.isCreateRequestFailed;
    if (isCreateRequestFailed) {
        return <label className="text-danger" type="text" style={{ "whiteSpace": "pre-line", "maxWidth": "100%", overflow: "hidden"}}>{``+props.msg}</label>;
    }
    return <div></div>;
}

class CreateOrder extends React.Component {
    constructor(props) {
        super(props);
        this.handleArrivalDatePick = this.handleArrivalDatePick.bind(this);
        this.handleDepartureDatePick = this.handleDepartureDatePick.bind(this);
        this.handleCreateOrderClick = this.handleCreateOrderClick.bind(this);
        this.handleRoomTypeSelect = this.handleRoomTypeSelect.bind(this);
        this.handlePeopleCountSelect = this.handlePeopleCountSelect.bind(this);
        var firstDay = new Date((new Date()).setHours(0, 0, 0, 0));
        var nextWeek = new Date(firstDay.getTime() + 7 * 24 * 60 * 60 * 1000);
        this.state = {
            arrivalDate: new Date((new Date()).setHours(23, 59, 59, 99)),
            departureDate: nextWeek,
            roomType: "",
            peopleCount: "",
            isCreateRequestFailed: false,
            errorMsg: ""
        }
    }
    
    handleArrivalDatePick(date) {
        if (date.getDate() === (new Date()).getDate()) {
            this.setState({ arrivalDate: new Date((new Date()).setHours(23, 59, 59, 99)) })
        } else {
            this.setState({ arrivalDate: new Date(date) })
        }
    }

    handleDepartureDatePick(date) {
        this.setState({ departureDate: new Date(date) })
    }

    handleRoomTypeSelect(event) {
        this.setState({roomType: event.target.value})
    }

    handlePeopleCountSelect(event) {
        this.setState({ peopleCount: event.target.value })
    }

    handleCreateOrderClick(event) {

        event.preventDefault();
        fetch('/Orders/Add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            },
            body: JSON.stringify({
                "arrivalDate": this.state.arrivalDate.toISOString().substring(0, 19),
                "departureDate": this.state.departureDate.toISOString().substring(0, 19),
                "roomTypeName": this.state.roomType,
                "peopleCount": parseInt(this.state.peopleCount)
            })
        })
            .then(response => {
                if (response.status === 201) {
                    this.setState({ isCreateRequestFailed: false, errorMsg: "" })
                    this.props.user.currentOrder = null;
                    this.props.updateParent();
                }
                if (response.status === 400 || response.status === 403 || response.status === 404) {
                    response.json().then(date => {
                        this.setState({ isCreateRequestFailed: true, errorMsg: date })
                    })
                }
            }) 
            .catch (error => {
                console.log(error);
            })
    }

    render() {
        return <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
            <div className="container form-group" style={{ position: "relative", "marginTop": "10%" }}>
                <h1 >Your Trip starts here!</h1>
            </div>
            <div className="container jumbotron form-group" style={{ "marginLeft": "10%", "maxWidth": "50%" }}>
                <form onSubmit={this.handleCreateOrderClick} style={{ padding: "20px" }}>
                    <CreateOrderFailSpan isCreateRequestFailed={this.state.isCreateRequestFailed} msg={this.state.errorMsg} />
                    <div className="form-group" style={{ display: "flex", "flexDirection": "row", width: "100%" }}>
                        <div style={{ "marginRight": "2%"}}>
                            <DatePicker
                                todayButton={"Today"}
                                minDate={new Date((new Date()).setHours(0, 0, 0, 0))}
                                selected={this.state.arrivalDate}
                                selectsStart
                                startDate={this.state.arrivalDate}
                                endDate={this.state.departureDate}
                                onChange={this.handleArrivalDatePick}
                                isClearable={false}
                                required={true}
                                className="form-control"
                            />
                            <small className="form-text text-muted">Arrival Date</small>
                        </div>
                        <div style={{ "marginLeft": "2%"}}>
                            <DatePicker
                                selected={this.state.departureDate}
                                selectsEnd
                                startDate={this.state.arrivalDate}
                                endDate={this.state.departureDate}
                                onChange={this.handleDepartureDatePick}
                                isClearable={false}
                                required={true}
                                className="form-control"
                            />
                            <small className="form-text text-muted">Departure Date</small>
                        </div>
                    </div>
                    <div className="form-group">
                        <select required onChange={this.handleRoomTypeSelect} value={this.state.roomType} className="form-control" id="roomTypeSelect" >
                            <option value="">Room type</option>
                            <option value="Standart">Standart</option>
                            <option value="Semi suite">Semi suite</option>
                            <option value="Suite">Suite</option>
                        </select>
                    </div>
                    <div className="form-group">
                        <input required onChange={this.handlePeopleCountSelect} value={this.state.peopleCount} type="number" step="1" min="1" max="10" className="form-control" placeholder="Number of settlers" />
                    </div>
                    <input style={{ width: "100%" }} className="btn btn-primary " type="submit" value="Book room!" />
                </form>
            </div>
        </div>
    }
}

export default CreateOrder;