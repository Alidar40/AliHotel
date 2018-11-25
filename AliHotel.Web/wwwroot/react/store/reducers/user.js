import { ACTION_LOGIN_REQUEST, ACTION_LOGIN_SUCCESS, ACTION_LOGIN_FAIL } from '../actions/authentication-actions'
import { history } from '../../containers/app'

export const initialState = {
    isLoggedIn: false,
    name: "",
    haveCurrentOrder: false,
    isLoginRequestFailed: false,
    error: "",
    isFetching: false
}

export function userReducer(state = initialState, action) {
    if (typeof state === 'undefined') {
        return initialState
    }

    switch (action.type) {
        case ACTION_LOGIN_REQUEST:
            return { ...state, isFetching: true, error: '' }
        case ACTION_LOGIN_SUCCESS:
            history.push('/')
            return { ...state, isFetching: false, name: action.name, isLoggedIn: true, isLoginRequestFailed:false,  }
        case ACTION_LOGIN_FAIL:
            return { ...state, isFetching: false, error: action.payload.error, isLoginRequestFailed:true }
        default:
            return state
    }
}