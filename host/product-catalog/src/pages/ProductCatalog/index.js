import React, { useEffect, useCallback } from 'react';
import ProductGrid from '../../components/ProductGrid';
import { useProductsFetch } from '../../hooks/useProductsFetch';
import Spinner from '../../components/Spinner';
import { toast, ToastContainer } from "react-toastify";
import FormModal from '../../components/Modals/FormModal';
import ProductsAPI from '../../apis/ProductsAPI';
import { validateProduct } from '../../validators/FormValidators';
import 'react-toastify/dist/ReactToastify.css';
import { isPersistedLocalStorageState } from '../../helpers';
import Button from '../../components/Button';
import { useNavigate } from 'react-router-dom';

const ProductCatalog = () => {
    const { state, loading, errorNotification, setLoadAgain } = useProductsFetch();
    const isAdmin = state.userRole=="Admin";
    const productsApi = new ProductsAPI();

    const showErrorToast = useCallback((message) => {
        toast.error(message, {
            position: "top-center",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }, [])

    const showSuccessToast = useCallback((message) => {
        toast.success(message, {
            position: "top-center",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }, [])

    useEffect(() => {
        if (errorNotification.isError) {
            showErrorToast(errorNotification.message)
        }
    }, [errorNotification.isError, errorNotification.message, showErrorToast])

    const addNewProductCallback = async (product) => {
        const result = validateProduct(product.name, product.price, product.stock, product.photo);
        if(result.error) {
            showErrorToast(result.message)
            return;
        }
        const token = isPersistedLocalStorageState("user").token;
        const addResult = await productsApi.addProduct(product, token)
        if(addResult.error){
            showErrorToast(addResult.message)
        } else {
            showSuccessToast("Product saved successfully!")
            setLoadAgain(true)
        }
    }

    const editProductCallback = useCallback(async (product) => {
        const result = validateProduct(product.name, product.price, product.stock, product.photo);
        if(result.error) {
            showErrorToast(result.message)
            return {error: true, data: product}
        }
        const token = isPersistedLocalStorageState("user").token;
        const updateResult = await productsApi.editProduct(product, token)
        if(updateResult.error){
            showErrorToast(updateResult.message)
            return {error: true, data: product}
        } else {
            showSuccessToast("Product updated successfully!")
            setLoadAgain(true)
            return {error: false, data: updateResult.data}
        }
    }, [productsApi, setLoadAgain, showErrorToast, showSuccessToast])

    const deleteProductCallback = useCallback(async (product) => {
        const token = isPersistedLocalStorageState("user").token;
        const result = await productsApi.deleteProduct(product.id, token)
        if(result.error){
            showErrorToast(result.message)
        } else {
            showSuccessToast("Product deleted successfully!")
            setLoadAgain(true)
        }
    }, [productsApi, setLoadAgain, showErrorToast, showSuccessToast])

    return (
        <div>
            {loading && <Spinner />}
            {isAdmin &&
                    <FormModal buttonName="newProductButton" triggerText="Add new product"
                        modalTitle="Add new product"
                        callbackText="Save"
                        callback={addNewProductCallback} updateProduct={false} product={{
                            id: 0,
                            name: '',
                            price: 0.0,
                            stock: 0,
                            photo: ''
                        }} />}
            <ProductGrid products={state.products} isAdmin={isAdmin} editProductCallback={editProductCallback} deleteProductCallback={deleteProductCallback}/>
        </div>
    );
};

export default ProductCatalog;