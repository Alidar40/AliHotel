import React from 'react'
import { connect } from 'react-redux'
import { withRouter } from 'react-router-dom'

import { Navbar } from '../components/navbar'
import { handleLogout } from '../store/actions/authentication-actions'

class NavbarContainer extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { user } = this.props
        return (
            <Navbar
                name={user.name}
                isLoggedIn={user.isLoggedIn}
                error={user.error}
                isFetching={user.isFetching}
                dispatch={this.props.dispatch}
                handleLogout={handleLogout}
                history={this.props.history}
                match={this.props.match}
            />
        )
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default withRouter(connect(mapStateToProps)(NavbarContainer));