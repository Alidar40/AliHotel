import React from 'react'
import { connect } from 'react-redux'

import { Navbar } from '../components/navbar'
import { handleLogout } from '../store/actions/authentication-actions'

class NavbarContainer extends React.Component {
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
            />
        )
    }
}

const mapStateToProps = store => {
    return {
        user: store.user,
    }
}

export default connect(mapStateToProps)(NavbarContainer);