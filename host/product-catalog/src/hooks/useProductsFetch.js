import {useCallback, useEffect, useState} from "react";
import ProductsAPI from "../apis/ProductsAPI";
import { getUserRole, isPersistedLocalStorageState } from "../helpers";

const initialState = {
    userRole: '',
    products: []
}

const notificationState = {
    isError: false,
    message: ''
}

export const useProductsFetch = () => {

    const [state, setState] = useState(initialState);
    const [loading, setLoading] = useState(false);
    const [errorNotification, setErrorNotification] = useState(notificationState);
    const [loadAgain, setLoadAgain] = useState(false);
    const productsApi = new ProductsAPI();

    const fetchProducts = useCallback(async (userRole, userToken) => {
        try {
            setLoading(true)
            const result = await productsApi.getAllProducts(userToken);
            if (result.error === false) {
                setErrorNotification({
                    isError: false,
                    message: ''
                })
                setState({
                    userRole: userRole,
                    products: result.data
                })
            } else {
                setErrorNotification({
                    isError: true,
                    message: result.message
                })
            }

        } catch (error) {
            setErrorNotification({
                isError: true,
                message: error.message
            })
        }
        setLoading(false)
    }, [productsApi]);

    const fetchProductsInStock = useCallback(async (userRole, userToken) => {
        try {
            setLoading(true)
            const result = await productsApi.getAllProductsInStock(userToken);
            if (result.error === false) {
                setErrorNotification({
                    isError: false,
                    message: ''
                })
                setState({
                    userRole: userRole,
                    products: result.data
                })
            } else {
                setErrorNotification({
                    isError: true,
                    message: result.message
                })
            }

        } catch (error) {
            setErrorNotification({
                isError: true,
                message: error.message
            })
        }
        setLoading(false)
    }, [productsApi]);

    // Initial
    useEffect(() => {
        console.log("Fetching all products")
        const sessionUser = isPersistedLocalStorageState('user')
        const token = sessionUser.token;
        const role = getUserRole(token);
        if(role == "Admin"){
            fetchProducts(role, token)
        } else if (role=="User"){
            fetchProductsInStock(role, token)
        }
    }, []) //runs only once with empty dependencies

    //Load Again
    useEffect(() => {
        if (!loadAgain)
            return;

        console.log("Fetching all products")
        const sessionUser = isPersistedLocalStorageState('user')
        const token = sessionUser.token;
        const role = getUserRole(token);
        if(role == "Admin"){
            fetchProducts(role, token)
        } else if (role=="User"){
            fetchProductsInStock(role, token)
        }
        setLoadAgain(false);
    }, [fetchProducts, fetchProductsInStock, loadAgain])

    return {state, loading, errorNotification, setLoadAgain}
}