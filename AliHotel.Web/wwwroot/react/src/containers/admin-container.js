import React from 'react'
import { connect } from 'react-redux'
import { Route, Switch } from 'react-router-dom'
import { TacoTable, DataType, SortDirection, Formatters, Summarizers, TdClassNames } from 'react-taco-table';

import Login from '../components/login'
import ActiveUsers from '../components/admin/active-users'
import ActiveOrders from '../components/admin/active-orders'
import { handleLogin } from '../store/actions/authentication-actions';
import { handleFetchAdminData } from '../store/actions/datafetch-actions';

class AdminContainer extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { user } = this.props;
        const { match } = this.props;

        let greeting = <h2 className="container text-danger">With great power comes great responsibility</h2>;
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

            if (!user.isLoggedIn) {
                this.props.dispatch(handleFetchAdminData());
                return (loading)
            }
        } 
        
        return (
            <div>
                <br/>
                {greeting}
                <br />
                <Switch>
                    <Route path={`${match.path}/Orders`} render={() => <ActiveOrders match={match} user={this.props.user} dispatch={this.props.dispatch} />} />
                    <Route path={`${match.path}/Users`} render={() => <ActiveUsers match={match} user={this.props.user} dispatch={this.props.dispatch} />} />
                    <Route
                        exact
                        path={match.path}
                    />
                </Switch>
            </div>
        )
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default connect(mapStateToProps)(AdminContainer);