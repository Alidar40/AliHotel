import React from 'react';
import Cookies from 'js-cookie';
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';

import { formatJsonDateToUTC } from '../utils/date';

class OrdersHistory extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            ordersHistory: null,
            historyFetched: false,
        }

        fetch('/api/Orders/History', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                if (JSON.stringify(data) === JSON.stringify([])) {
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

            return <div className="container body-content">
                {this.OrdersHistory(formatJsonDateToUTC(this.state.ordersHistory), columns)}
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default OrdersHistory;