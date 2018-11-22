import React from 'react';
import ReactDOM from 'react-dom';
import { connect } from 'react-redux'
import { BrowserRouter, Route, Switch } from 'react-router-dom'

import NavbarContainer from './navbar-container';
import HomeContainer from './home-container';
import MyOrders from '../components/myorders';
import store from '../store/hotel-store';
import { handleLogin } from '../store/actions/authentication-actions'

class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        console.log(this.props.dispatch)
        return <BrowserRouter /*history={hashHistory}*/>
                    <div>
                        <NavbarContainer />
                        <Switch>
                            <Route exact path="/" component={HomeContainer} />
                            <Route exact path="/MyOrders" component={MyOrders} />
                        </Switch>
                    </div>
               </BrowserRouter>
    }
}

const mapStateToProps = (store) => {
    return {
        user: store.user
    }
}

export default connect(mapStateToProps)(App);