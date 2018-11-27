import Cookies from 'js-cookie';

export const ACTION_CURRENT_ORDER_REQUEST = 'ACTION_CURRENT_ORDER_REQUEST';
export const ACTION_CURRENT_ORDER_SUCCESS = 'ACTION_CURRENT_ORDER_SUCCESS';
export const ACTION_CURRENT_ORDER_ABSENCE = 'ACTION_CURRENT_ORDER_ABSENCE';
export const ACTION_CURRENT_ORDER_FAIL = 'ACTION_CURRENT_ORDER_FAIL';

export const ACTION_ADMIN_DATA_REQUEST = 'ACTION_ADMIN_DATA_REQUEST';
export const ACTION_ADMIN_DATA_SUCCESS = 'ACTION_ADMIN_DATA_SUCCESS';
export const ACTION_ADMIN_DATA_ABSENCE = 'ACTION_ADMIN_DATA_ABSENCE';
export const ACTION_ADMIN_DATA_FAIL = 'ACTION_ADMIN_DATA_FAIL';

export function handleCurrentOrder() {
    return function (dispatch) {
        dispatch({
            type: ACTION_CURRENT_ORDER_REQUEST,
        })

        fetch('/api/Orders/Current', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                if (response.status == 404) {
                    response.json().then(data => {
                        dispatch({
                            type: ACTION_CURRENT_ORDER_ABSENCE,
                            payload: data,
                        })
                    })
                } else {
                    response.json().then(data => {
                        delete data['id'];
                        delete data.userId;
                        delete data.room;
                        delete data.roomId;
                        dispatch({
                            type: ACTION_CURRENT_ORDER_SUCCESS,
                            current: data,
                        })
                    })
                }
            })
            .catch(error => {
                dispatch({
                    type: ACTION_CURRENT_ORDER_FAIL,
                    payload: error,
                })
            })
    }
}

export function handleFetchAdminData() {
    return function (dispatch) {
        dispatch({
            type: ACTION_ADMIN_DATA_REQUEST,
        })

        fetch('/api/Admin/Orders/GetCurrentData', {
            method: 'GET',
            headers: {
                'Set-Cookie': Cookies.get('.AspNetCore.Identity.Application'),
            }
        })
            .then(response => {
                if (response.status == 404) {
                    response.json().then(data => {
                        dispatch({
                            type: ACTION_ADMIN_DATA_ABSENCE,
                            name: "admin",
                        })
                    })
                } else {
                    response.json().then(data => {
                        dispatch({
                            type: ACTION_ADMIN_DATA_SUCCESS,
                            payload: data,
                        })
                    })
                }
            })
            .catch(error => {
                dispatch({
                    type: ACTION_ADMIN_DATA_FAIL,
                    payload: error,
                })
            })
    }
}