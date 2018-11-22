import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux';

import Login from '../components/login';
import CurrentOrder from '../components/current-order';
import { handleLogin } from '../store/actions/authentication-actions';

class HomeContainer extends React.Component {
    render() {
        const { user } = this.props

        let greeting;

        if (this.props.user.isLoggedIn) {
            greeting = <CurrentOrder />;
        } else {
            greeting = <Login isLoginRequestFailed={this.props.user.isLoginRequestFailed}
                error={this.props.user.error}
                user={this.props.user}
                dispatch={this.props.dispatch}
                handleLogin={handleLogin} />;
        }
        
        return (
            <div className="container jumbotron form-group" style={{ display: "flex", "flexDirection": "row", background: "white" }}>
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