import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';
import { withRouter } from 'react-router-dom'

import Login from '../components/login'
import CurrentOrder from '../components/current-order';
import CreateOrder from '../components/create-order';
import { handleLogin } from '../store/actions/authentication-actions';
import { handleCurrentOrder } from '../store/actions/datafetch-actions';

class HomeContainer extends React.Component {
    constructor(props) {
        super(props);
        this.updateParent = this.updateParent.bind(this);
    }

    updateParent() {
        this.forceUpdate();
    }

    render() {
        const { user } = this.props;

        let greeting;
        const loading = <div className="container body-content"><br /><h3>Loading</h3></div>
        
        if (this.props.location.pathname === "/Login") {
            if (user.isLoggedIn) {
                this.props.history.push("/");
            }

            greeting = <div>
                <Login isLoginRequestFailed={this.props.user.isLoginRequestFailed}
                    error={this.props.user.error}
                    user={this.props.user}
                    dispatch={this.props.dispatch}
                    handleLogin={handleLogin} />
            </div>
        } 

        if (this.props.location.pathname === "/") {
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

            if (user.haveCurrentOrder) {
                greeting = <CurrentOrder user={user} updateParent={this.updateParent}/>
            } else {
                greeting = <CreateOrder user={user} updateParent={this.updateParent} />
            }
        } 
        
        return (
            <div>
                {greeting}
            </div>
        )
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default connect(mapStateToProps)(HomeContainer);