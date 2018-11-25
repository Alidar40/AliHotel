import Cookies from 'js-cookie';

export const ACTION_LOGIN_REQUEST = 'ACTION_LOGIN_REQUEST';
export const ACTION_LOGIN_SUCCESS = 'ACTION_LOGIN_SUCCESS';
export const ACTION_LOGIN_FAIL = 'ACTION_LOGIN_FAIL';

export const ACTION_LOGOUT = 'ACTION_LOGOUT';

export function handleLogin(email, password) {
    return function (dispatch) {
        dispatch({
            type: ACTION_LOGIN_REQUEST,
        })

        fetch('/Account/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email: email,
                password: password,
            })
        })
            .then(response => {
                if (response.status == 200) {
                    response.json().then(json => {
                        dispatch({
                            type: ACTION_LOGIN_SUCCESS,
                            name: json,
                            isLoggedIn: true,
                        })
                    });
                    return;
                }
                return error;
            })
            .catch(function (error) {
                dispatch({
                    type: ACTION_LOGIN_FAIL,
                    isLoginRequestFailed: true,
                    payload: error,
                })
            });
    }
}

export function handleLogout() {
    return function (dispatch) {
        fetch('/Account', {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })

        dispatch({
            type: ACTION_LOGOUT
        })
    }
}