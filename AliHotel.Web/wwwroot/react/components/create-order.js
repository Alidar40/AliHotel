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

class CreateOrder extends React.Component {
    constructor(props) {
        super(props);
        this.handleArrivalDatePick = this.handleArrivalDatePick.bind(this);
        this.handleDepartureDatePick = this.handleDepartureDatePick.bind(this);
        var firstDay = new Date((new Date()).setHours(0, 0, 0, 0));
        console.log(typeof firstDay)
        var nextWeek = new Date(firstDay.getTime() + 7 * 24 * 60 * 60 * 1000);
        this.state = {
            arrivalDate: new Date((new Date()).setHours(0, 0, 0, 0)),
            departureDate: nextWeek,
            roomType: "",
            peopleCount: ""
        }
    }
    
    handleArrivalDatePick(date) {
        this.setState({ arrivalDate: date })
    }

    handleDepartureDatePick(event) {
        this.setState({ departureDate: date })
    }

    handleCreateOrderClick() {
        //TODO
    }

    render() {
        return <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
            <div className="container form-group" style={{ position: "relative", "marginTop": "10%" }}>
                <h1 >Your Trip starts here!</h1>
            </div>
            <div className="container jumbotron form-group" style={{ "marginLeft": "10%", "maxWidth": "50%" }}>
                <form onSubmit={this.handleCreateOrderClick} style={{ padding: "20px" }}>
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
                                className="form-control"
                            />
                        </div>
                        <div style={{ "marginLeft": "2%"}}>
                            <DatePicker
                                selected={this.state.departureDate}
                                selectsEnd
                                startDate={this.state.arrivalDate}
                                endDate={this.state.departureDate}
                                onChange={this.handleDepartureDatePick}
                                className="form-control"
                            />
                        </div>
                    </div>
                    <div className="form-group">
                        <select value={this.roomType} className="form-control" id="roomTypeSelect" >
                            <option selected="">Room type</option>
                            <option value="Standart">Standart</option>
                            <option value="Semi suite">Semi suite</option>
                            <option value="Suite">Suite</option>
                        </select>
                    </div>
                    <div className="form-group">
                        <input type="number" step="1" min="1" max="10" className="form-control" placeholder="Number of settlers"/>
                    </div>
                    <input style={{ width: "100%" }} className="btn btn-primary " type="submit" value="Book room!" />
                </form>
            </div>
        </div>
    }
}

export default CreateOrder;