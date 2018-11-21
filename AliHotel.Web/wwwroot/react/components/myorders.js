import React from 'react';

import CurrentOrder from './current-order';
import OrdersHistory from './orders-history';

class MyOrders extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <div className="container body-content">
                    <CurrentOrder />
                    <OrdersHistory />
                </div>
    }
}

export default MyOrders;