import React from 'react';
import { connect } from 'react-redux'

import CurrentOrder from '../components/current-order';
import OrdersHistory from '../components/orders-history';
import { handleCurrentOrder } from '../store/actions/datafetch-actions';

class MyOrdersContainer extends React.Component {
    constructor(props) {
        super(props);
        this.updateParent = this.updateParent.bind(this);
    }

    updateParent() {
        this.forceUpdate();
    }

    render() {
        const { user } = this.props;
        const loading = <div className="container body-content"><br /><h3>Loading</h3></div>

        if (this.props.location.pathname === "/Login" && user.isLoggingOut) {
            this.props.history.push("/");
        }

        if (user.isFetchingCurrentOrder) {
            return (loading)
        }

        if (!user.isLoggedIn) {
            this.props.dispatch(handleCurrentOrder());
            return (loading)
        }

        if (user.currentOrder === null) {
            this.props.dispatch(handleCurrentOrder());
            return (loading)
        }

        return <div className="container body-content">
                    <CurrentOrder user={user} updateParent={this.updateParent}/>
                    <OrdersHistory user={user} updateParent={this.updateParent}/>
                </div>
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default connect(mapStateToProps)(MyOrdersContainer);