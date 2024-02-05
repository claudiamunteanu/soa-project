import { useCallback, useEffect, useState } from "react";
import { isPersistedLocalStorageState } from "../helpers";

const initialState = {
    products: []
}

const notificationState = {
    isError: false,
    message: ''
}

export const useCartProductsFetch = () => {

    const [state, setState] = useState(initialState);
    const [loading, setLoading] = useState(false);
    const [errorNotification, setErrorNotification] = useState(notificationState);
    const [loadAgain, setLoadAgain] = useState(false);

    const fetchCartProducts = useCallback(async () => {
        try {
            setLoading(true)
            const result = isPersistedLocalStorageState("cart")
            if (result) {
                setErrorNotification({
                    isError: false,
                    message: ''
                })
                setState({
                    products: result
                })
            } else {
                setState({
                    products: []
                })
            }

        } catch (error) {
            setErrorNotification({
                isError: true,
                message: "Could not load cart!"
            })
        }
        setLoading(false)
    }, []);

    // Initial
    useEffect(() => {
        console.log("Fetching cart products")
        fetchCartProducts();
    }, []) //runs only once with empty dependencies

    //Load Again
    useEffect(() => {
        if (!loadAgain)
            return;

        console.log("Fetching cart products")
        fetchCartProducts();
        console.log(state)
        setLoadAgain(false);
    }, [fetchCartProducts, loadAgain])

    return { state, loading, errorNotification, setLoadAgain }
}