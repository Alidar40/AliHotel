import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import { createStore } from 'redux'
import { BrowserRouter, Route, Switch } from 'react-router-dom'

import Navbar from './navbar';
import Home from './home';
import MyOrders from './myorders';

const initialState = {
    isLoggedIn: false,
    name: ""
}

function hotelStore(state, action) {
    if (typeof state === 'undefined') {
        return initialState
    }

    return state
}

const store = createStore(hotelStore)

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