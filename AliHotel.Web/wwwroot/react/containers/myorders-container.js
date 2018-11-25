import React from 'react';
import { connect } from 'react-redux'

import CurrentOrder from '../components/current-order';
import OrdersHistory from '../components/orders-history';

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