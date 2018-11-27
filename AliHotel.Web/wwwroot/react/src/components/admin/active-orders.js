import React from 'react';
import Cookies from 'js-cookie';
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';

import { formatJsonDateToUTC } from '../../utils/date';
import { handleFetchAdminData } from '../../store/actions/datafetch-actions';
import Modal from '../modal';

class ActiveOrders extends React.Component {
    constructor(props) {
        super(props);
    }
    
    ActiveOrders(orders, columns) {
        return <div>
            <h2>Current orders</h2>
            <TacoTable
                className="table table-hover simple-example table-full-width table-striped table-sortable"
                columns={columns}
                columnHighlighting
                data={orders}
                striped
                sortable
                smt="smt"
            />
        </div>
    }
    
    render() {
        const { user } = this.props;
        const loading = <div className="container body-content"><br /><h3>Loading</h3></div>

        if (user.isFetchingAdminData) {
            return (loading)
        }

        if (user.adminData === "NO_ACTIVE_ORDER") {
            return <div className="container body-content"><br /><h3>Hotel is empty. There is no active orders</h3></div>
        }

        if (user.adminData === null) {
            this.props.dispatch(handleFetchAdminData());
            return (loading)
        }

        if (!user.isLoggedIn) {
            this.props.dispatch(handleFetchAdminData());
            return (loading)
        }

        if (this.props.user.adminHaveData) {
            const columns = [
                {
                    id: 'userEmail',
                    type: DataType.String,
                    header: 'Email',
                },
                {
                    id: 'userName',
                    type: DataType.String,
                    header: 'Name',
                },
                {
                    id: 'arrivalDate',
                    type: DataType.NumberOrdinal,
                    header: 'Arrival date',
                },
                {
                    id: 'departureDate',
                    type: DataType.String,
                    header: 'Departure date',
                },
                {
                    id: 'number',
                    type: DataType.String,
                    header: 'Room number',
                },
                {
                    id: 'name',
                    type: DataType.String,
                    header: 'Room type',
                },
                {
                    id: 'id',
                    type: DataType.String,
                    header: 'Close order',
                    renderer(cellData, { column, rowData }) {
                        return <button onClick={() => {
                            fetch(`/api/Admin/Orders/CloseOrder?orderId=${rowData.id}`, {
                                method: 'GET',
                                headers: {
                                    'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
                                }
                            })
                                .then(response => {
                                    if (response.status == 200) {
                                        response.json().then(data => {
                                            console.log(data);
                                        })
                                    } else {
                                        console.log("closing order status code:" + response.status);
                                    }
                                })
                                .catch(error => {
                                    console.log(error);
                                })
                        }} className="btn btn-danger btn-sm">Close order</button>;
                    },
                }];

            return <div className="container body-content">
                {this.ActiveOrders(formatJsonDateToUTC(user.adminData.activeOrders), columns)}
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default ActiveOrders;