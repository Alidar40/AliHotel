import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';

import Login from '../components/login';
import CurrentOrder from '../components/current-order';
import CreateOrder from '../components/create-order';
import { handleLogin } from '../store/actions/authentication-actions';

class HomeContainer extends React.Component {
    render() {
        const { user } = this.props

        let greeting;

        if (this.props.user.isLoggedIn) {
            if (this.props.user.haveCurrentOrder) {
                greeting = <CurrentOrder />
            } else {
                greeting = <CreateOrder />
            }

        } else {
            greeting = <Login isLoginRequestFailed={this.props.user.isLoginRequestFailed}
                        error={this.props.user.error}
                        user={this.props.user}
                        dispatch={this.props.dispatch}
                        handleLogin={handleLogin} />
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