import React from 'react'
import { connect } from 'react-redux'

import { handleLogin } from '../store/actions/authentication-actions'
import { Navbar } from '../components/navbar'

class NavbarContainer extends React.Component {
    render() {
        const { user } = this.props
        return (
            <Navbar
                name={user.name}
                isLoggedIn={user.isLoggedIn}
                error={user.error}
                isFetching={user.isFetching}
                handleLogin={this.handleLogin}
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