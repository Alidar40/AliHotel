import { createStore } from 'redux'

const initialState = {
    isLoggedIn: false,
    name: ""
}

function hotelStore(state, action) {
    if (typeof state === 'undefined') {
        return initialState
    }

    return state
}

const store = createStore(hotelStore);

export default store