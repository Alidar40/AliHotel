export const ACTION_LOGIN_REQUEST = 'ACTION_LOGIN_REQUEST';
export const ACTION_LOGIN_SUCCESS = 'ACTION_LOGIN_SUCCESS';
export const ACTION_LOGIN_FAIL = 'ACTION_LOGIN_FAIL';

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