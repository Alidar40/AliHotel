﻿import React from 'react';
import { render } from 'react-dom';
import { createStore } from 'redux';
import { Provider } from 'react-redux';

import App from './containers/app';
import { store } from './store/hotel-store'

render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root')
);