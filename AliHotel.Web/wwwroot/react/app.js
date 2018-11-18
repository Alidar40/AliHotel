import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import { BrowserRouter, Route, Switch } from 'react-router-dom'

import Navbar from './components/navbar';
import Home from './components/home';
import MyOrders from './components/myorders';
import store from './reducers/hotel-store';

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter /*history={hashHistory}*/>
            <div>
                <Navbar />
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route exact path="/MyOrders" component={MyOrders} />
                </Switch>
            </div>
        </BrowserRouter>
    </Provider>,
    document.getElementById('root')
)