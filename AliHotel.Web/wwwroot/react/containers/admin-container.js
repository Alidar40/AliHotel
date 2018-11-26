import React from 'react'
import { connect } from 'react-redux'
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';

import Login from '../components/login'
import { handleLogin } from '../store/actions/authentication-actions';
import { handleFetchAdminData } from '../store/actions/datafetch-actions';

class AdminContainer extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { user } = this.props;

        let greeting = <h1 className="container">With great power comes great responsibility</h1>;
        const loading = <div className="container body-content"><br /><h3>Loading</h3></div>
        
        if (this.props.location.pathname === "/Admin") {
            if (user.isLoggingOut) {
                this.props.user.isLoggingOut = false;
                this.props.history.push("/Login");
                return (<div></div>)
            }

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
        } 

        return (
            <div>{greeting}</div>
        )
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default connect(mapStateToProps)(AdminContainer);