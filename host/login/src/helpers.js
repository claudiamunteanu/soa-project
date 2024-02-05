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