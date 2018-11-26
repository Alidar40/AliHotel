import { ACTION_LOGIN_REQUEST, ACTION_LOGIN_SUCCESS, ACTION_LOGIN_FAIL, ACTION_LOGOUT } from '../actions/authentication-actions'

import {
    ACTION_CURRENT_ORDER_REQUEST, ACTION_CURRENT_ORDER_SUCCESS, ACTION_CURRENT_ORDER_ABSENCE, ACTION_CURRENT_ORDER_FAIL,
    ACTION_ADMIN_DATA_REQUEST, ACTION_ADMIN_DATA_SUCCESS, ACTION_ADMIN_DATA_ABSENCE, ACTION_ADMIN_DATA_FAIL
} from '../actions/datafetch-actions'
import { history } from '../../containers/app'

export const initialState = {
    isLoggedIn: false,
    name: "",
    isFetchingLogin: false,
    isLoginRequestFailed: false,

    isLoggingOut:false,

    currentOrder: null,
    haveCurrentOrder: false,
    isFetchingCurrentOrder: false,

    adminData: null,
    isFetchingAdminData: false,
    adminHaveData: false,

    error: "",
}

export function userReducer(state = initialState, action) {
    if (typeof state === 'undefined') {
        return initialState
    }

    switch (action.type) {
        case ACTION_LOGIN_REQUEST:
            return { ...state, isFetchingLogin: true}

        case ACTION_LOGIN_SUCCESS:
            return { ...state, isFetchingLogin: false, name: action.name, isLoggedIn: true, isLoginRequestFailed: false, }

        case ACTION_LOGIN_FAIL:
            return { ...state, isFetchingLogin: false, error: action.payload.error, isLoginRequestFailed: true }

        case ACTION_LOGOUT:
            return {...initialState, isLoggingOut: true }



        case ACTION_CURRENT_ORDER_REQUEST:
            return { ...state, isFetchingCurrentOrder: true}

        case ACTION_CURRENT_ORDER_SUCCESS:
            return { ...state, isFetchingCurrentOrder: false, currentOrder: action.current, name: action.current["0"].userName, haveCurrentOrder:true, isLoggedIn: true }

        case ACTION_CURRENT_ORDER_ABSENCE:
            return { ...state, isFetchingCurrentOrder: false, currentOrder: "NO_ACTIVE_ORDER", name: action.payload.userName, isLoggedIn: true, haveCurrentOrder: false }

        case ACTION_CURRENT_ORDER_FAIL:
            return { ...state, isFetchingCurrentOrder: false, error: action.payload.error }


        case ACTION_ADMIN_DATA_REQUEST:
            return { ...state, isFetchingAdminData: true }

        case ACTION_ADMIN_DATA_SUCCESS:
            return { ...state, isFetchingAdminData: false, adminData: action.payload, name: action.payload.name, adminHaveData: true, isLoggedIn: true }

        case ACTION_ADMIN_DATA_ABSENCE:
            return { ...state, isFetchingAdminData: false, adminData: "NO_ACTIVE_ORDER", name: action.payload.name, isLoggedIn: true, adminHaveData: false }

        case ACTION_ADMIN_DATA_FAIL:
            return { ...state, isFetchingAdminData: false, error: action.payload.error }

        default:
            return state
    }
}