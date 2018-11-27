import React from 'react';
import ReactDOM from 'react-dom';
import { connect } from 'react-redux'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import createHistory from 'history/createBrowserHistory'

import NavbarContainer from './navbar-container';
import HomeContainer from './home-container';
import MyOrdersContainer from './myorders-container';
import AdminContainer from './admin-container';
import Register from '../components/register'
import Footer from '../components/footer'
import store from '../store/hotel-store';
import { handleLogin } from '../store/actions/authentication-actions'

export const history = createHistory()

class App extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <BrowserRouter>
                    <div>
                        <NavbarContainer history={history}/>
                        <Switch>
                            <Route exact path="/" component={HomeContainer} />
                            <Route exact path="/Login" component={HomeContainer} />
                            <Route exact path="/MyOrders" component={MyOrdersContainer} />
                            <Route exact path="/Register" component={Register} />
                            <Route path="/Admin" component={AdminContainer} />
                        </Switch>
                        <Footer />
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