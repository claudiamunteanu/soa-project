import React, { useCallback, useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import CartSummary from '../../components/CartSummary';
import { useCartProductsFetch } from '../../hooks/useCartProductsFetch';
import { toast, ToastContainer } from "react-toastify";
import CartSummaryTotal from '../../components/CartSummaryTotal';

const ShoppingCart = () => {
    const { state, loading, errorNotification, setLoadAgain } = useCartProductsFetch();

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

    useEffect(() => {
        if (errorNotification.isError) {
            showErrorToast(errorNotification.message)
        }
    }, [errorNotification.isError, errorNotification.message, showErrorToast])


    return (
        <>
            {loading && <Spinner />}
            <Container fluid>
                <Row>
                    <Col lg={8}>
                        <CartSummary products={state.products} setLoadAgain={setLoadAgain} />
                    </Col>
                    <Col lg={4} className="da d-lg-block">
                        <CartSummaryTotal products={state.products}/>
                    </Col>
                </Row>
            </Container>
        </>
    );
};

export default ShoppingCart;