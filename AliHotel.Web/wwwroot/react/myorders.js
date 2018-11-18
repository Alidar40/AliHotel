import React from 'react';

import Cookies from 'js-cookie';
import {
    TacoTable, DataType, SortDirection, Formatters,
    Summarizers, TdClassNames
} from 'react-taco-table';
import 'react-taco-table/dist/react-taco-table.css';


class MyOrders extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            current: null,
            ordersHistory: null,
            currentFetched: false,
            historyFetched: false,
        }

        fetch('/Orders/Current', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                delete data['id'];
                delete data.userId;
                delete data.room;
                delete data.roomId;
                this.setState({ current: data, currentFetched: true });
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
                delete data.id;
                delete data.userId;
                delete data.room;
                delete data.roomId;
                this.setState({ ordersHistory: data, historyFetched: true });
            })
            .catch(error => {
                console.log(error);
            })
    }

    componentWillUnmount() {
        this.setState({ historyFetched: false, currentFetched: false });
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
                <br/>
                <h2>Current order</h2>
                <TacoTable
                    className="table table-hover simple-example table-full-width table-striped"
                    columns={columns}
                    data={JSON.parse(JSON.stringify(this.state.current), reviver)}
                    striped
                />

                <h2>My old orders</h2>
                <TacoTable
                    className="table table-hover simple-example table-full-width table-striped table-sortable"
                    columns={columns}
                    columnHighlighting
                    data={JSON.parse(JSON.stringify(this.state.ordersHistory), reviver)}
                    striped
                    sortable
                />
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default MyOrders;