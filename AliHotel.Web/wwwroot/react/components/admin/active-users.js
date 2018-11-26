import React from 'react';
import Cookies from 'js-cookie';
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';

import { formatJsonDateToUTC } from '../../utils/date';
import { handleFetchAdminData } from '../../store/actions/datafetch-actions';

class ActiveUsers extends React.Component {
    constructor(props) {
        super(props);
    }
    
    ActiveUsers(users, columns) {
        if (this.props.user.adminHaveData === false) {
            return <div><br/><h3>Hotel is empty. There is no current renters</h3></div>
        }
        return <div>
            <h2>Current renters</h2>
            <TacoTable
                className="table table-hover simple-example table-full-width table-striped table-sortable"
                columns={columns}
                columnHighlighting
                data={users}
                striped
                sortable
            />
        </div>
    }

    render() {
        const { user } = this.props;
        const loading = <div className="container body-content"><br /><h3>Loading</h3></div>

        if (user.isFetchingAdminData) {
            return (loading)
        }

        if (user.adminHaveData == false) {
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
                    id: 'email',
                    type: DataType.String,
                    header: 'Email',
                },
                {
                    id: 'name',
                    type: DataType.String,
                    header: 'Name',
                },
                {
                    id: 'phoneNumber',
                    type: DataType.NumberOrdinal,
                    header: 'Phone number',
                },
                {
                    id: 'birthDate',
                    type: DataType.String,
                    header: 'Birth date',
                },
                {
                    id: 'creditCard',
                    type: DataType.String,
                    header: 'Credit card',
                }];

            return <div className="container body-content">
                {this.ActiveUsers(formatJsonDateToUTC(user.adminData.renters), columns)}
            </div>
        } else {
            return <div className="container body-content"><br /><h3>Loading content</h3></div>
        }
    }
}

export default ActiveUsers;