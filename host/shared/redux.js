import { createStore } from 'redux';
import { getCartSize } from '../src/helpers';

const initialState = {
    cartSize: getCartSize(),
};

const rootReducer = (state = initialState, action) => {
    switch (action.type) {
        case 'INCREMENT_CART_SIZE':
            return { ...state, cartSize: state.cartSize + 1 };
        case 'DECREMENT_CART_SIZE':
            return { ...state, cartSize: state.cartSize - 1 };
        case 'RESET_CART_SIZE':
            return { ...state, cartSize: 0 };
        default:
            return state;
    }
};

const store = createStore(rootReducer);

export default store;