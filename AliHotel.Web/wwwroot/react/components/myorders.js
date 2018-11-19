import React from 'react';

import Cookies from 'js-cookie';
import {
    TacoTable, DataType, SortDirection, Formatters,
    Summarizers, TdClassNames
} from 'react-taco-table';
import 'react-taco-table/dist/react-taco-table.css';

import Modal from './modal';

class MyOrders extends React.Component {
    constructor(props) {
        super(props);
        this.CurrentOrder = this.CurrentOrder.bind(this);
        this.OrdersHistory = this.OrdersHistory.bind(this);
        this.toggleCloseOrderModal = this.toggleCloseOrderModal.bind(this);
        this.handleCloseOrderSubmitClick = this.handleCloseOrderSubmitClick.bind(this);
        this.handleCloseOrderFinishedClick = this.handleCloseOrderFinishedClick.bind(this);
        this.state = {
            current: null,
            ordersHistory: null,
            currentFetched: false,
            historyFetched: false,
            isCloseOrderModalOpen: false,
            isClosingConfirmed: false,
            orderClosedResponse: ""
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
                        this.setState({ current: data, currentFetched: true });
                    })                    
                }
            })
            .catch(error => {
                console.log(error);
            })


        fetch('/Orders/History', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (JSON.stringify(data) === JSON.stringify([{}])) {
                    this.setState({ ordersHistory: "ORDERS_HISTORY_IS_EMPTY", historyFetched: true });
                } else {
                    delete data.id;
                    delete data.userId;
                    delete data.room;
                    delete data.roomId;
                    this.setState({ ordersHistory: data, historyFetched: true });
                }
            })
            .catch(error => {
                console.log(error);
            })
    }

    componentWillUnmount() {
        this.setState({ historyFetched: false, currentFetched: false });
    }

    toggleCloseOrderModal() {
        this.setState({ isCloseOrderModalOpen: !this.state.isCloseOrderModalOpen })
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
                this.setState({isClosingConfirmed: true, orderClosedResponse: data })
            })
            .catch(error => {
                console.log(error);
            })
    }

    handleCloseOrderFinishedClick() {
        this.setState({ isCloseOrderModalOpen: false, orderClosedResponse: "", isClosingConfirmed: false, current: "NO_ACTIVE_ORDERS" });
    }

    CurrentOrder(current) {
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
                    <button type="button" className="btn btn-primary btn-lg" style={{ "marginTop": "3%", width: "100%" }}>Change deparure date</button>
                </div>

                <div className="form-group">
                    <button type="button" onClick={this.toggleCloseOrderModal} className="btn btn-primary btn-lg" style={{ width: "100%" }}>Close order</button>
                </div>
            </div>
        </div>
    }

    OrdersHistory(ordersHistory, columns) {
        if (ordersHistory === "ORDERS_HISTORY_IS_EMPTY") {
            return <div></div>
        }
        return <div>
            <h2>My old orders</h2>
            <TacoTable
                className="table table-hover simple-example table-full-width table-striped table-sortable"
                columns={columns}
                columnHighlighting
                data={ordersHistory}
                striped
                sortable
            />
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

                        <div style={{ "textAlign": "center" , display: "flex", flexDirection: "row"}}>
                            <div style={{ padding: "5px", size: "100%"}}>
                                <input type="button" className="btn btn-primary" type="submit" onClick={this.handleCloseOrderSubmitClick} value="Yes" />
                            </div>
                            <div style={{ padding: "5px" }}>
                                <input type="button" className="btn btn-primary" onClick={this.toggleCloseOrderModal} readOnly value="No" />
                            </div>
                        </div>
                    </div>
        }
        
    }

    render() {
        if (this.state.currentFetched & this.state.historyFetched) {
            const columns = [
                {
                    id: 'number',
                    type: DataType.NumberOrdinal,
                    header: 'Room number',
                },
                {
                    id: 'name',
                    type: DataType.String,
                    header: 'Room Type',
                },
                {
                    id: 'arrivalDate',
                    type: DataType.String,
                    header: 'Arrival Date',
                },
                {
                    id: 'departureDate',
                    type: DataType.String,
                    header: 'Departure Date',
                },
                {
                    id: 'bill',
                    type: DataType.NumberOrdinal,
                    header: 'Bill',
                }];

            const dateFormat = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}$/;

            function reviver(key, value) {
                if (typeof value === "string" && dateFormat.test(value)) {
                    var date = new Date(value);

                    return date.toString().substring(0, 15);
                }

                return value;
            }
            
            return <div className="container body-content">
                <br />
                <Modal show={this.state.isCloseOrderModalOpen}>
                    {this.CloseOrderModal(this.state.isClosingConfirmed)}
                </Modal>

                {this.CurrentOrder(JSON.parse(JSON.stringify(this.state.current), reviver))}                

                {this.OrdersHistory(JSON.parse(JSON.stringify(this.state.ordersHistory), reviver), columns)}
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default MyOrders;