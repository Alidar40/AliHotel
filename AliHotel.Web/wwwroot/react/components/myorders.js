import React from 'react';
import Cookies from 'js-cookie';
import {TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames} from 'react-taco-table';

import CurrentOrder from './current-order';

class MyOrders extends React.Component {
    constructor(props) {
        super(props);
        this.OrdersHistory = this.OrdersHistory.bind(this);
        this.state = {
            ordersHistory: null,
            historyFetched: false,
        }

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
        this.setState({ historyFetched: false });
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
    
    render() {
        if (this.state.historyFetched) {
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

            return <div className="container body-content">
                        <CurrentOrder />
                        {this.OrdersHistory(JSON.parse(JSON.stringify(this.state.ordersHistory), reviver), columns)}
                   </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default MyOrders;