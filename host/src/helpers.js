import { jwtDecode } from "jwt-decode";

const reviver = (key, value) => {
    if (typeof value === 'object' && value !== null) {
        if (value.dataType === 'Map') {
            return new Map(value.value);
        }
    }
    return value;
}
export const isPersistedSessionStorageState = stateName => {
    const sessionState = sessionStorage.getItem(stateName);
    return sessionState && JSON.parse(sessionState, reviver);
}

export const isPersistedLocalStorageState = stateName => {
    const sessionState = localStorage.getItem(stateName);
    return sessionState && JSON.parse(sessionState, reviver);
}

export const getUserRole = token => {
    const decodedToken = jwtDecode(token);
    return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
}

export const getCartSize = () => {
    const cart = isPersistedLocalStorageState("cart");
    if (cart)
        return cart.reduce((accumulator, currentValue) => accumulator + currentValue.quantity, 0)
    else
        return 0;
}