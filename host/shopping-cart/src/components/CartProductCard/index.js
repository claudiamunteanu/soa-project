import { Wrapper } from "./CartProductCard.styles";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Image from "react-bootstrap/Image";
import { useEffect, useRef, useState } from "react";
import { isPersistedLocalStorageState } from "../../helpers";
import { useDispatch } from "react-redux";

const CartProductCard = ({ product, setLoadAgain}) => {
    const dispatch = useDispatch();


    const incrementQuantityCallback = () => {
        product.quantity++;
        dispatch({type: 'INCREMENT_CART_SIZE'});
        updateLocalCart();
    }

    const decrementQuantityCallback = () => {
       product.quantity--;
        dispatch({type: 'DECREMENT_CART_SIZE'});
        updateLocalCart();
    }

    const updateLocalCart = () => {
        var products = isPersistedLocalStorageState("cart");
        if (products) {
            var productIndex = products.map(p => p.id).indexOf(product.id);
            if (product.quantity > 0) {
                products[productIndex].quantity = product.quantity;
            } else {
                console.log("deleting")
                products.splice(productIndex, 1);
            }
            if (products.length > 0) {
                localStorage.setItem("cart", JSON.stringify(products));
            } else {
                localStorage.removeItem("cart");
            }
            setLoadAgain(true);  
        }  
    }

    return (
        <Wrapper>
            <Row>
                <Col className="col" sm={6}>
                    <Image src="https://retailminded.com/wp-content/uploads/2016/03/EN_GreenOlive-1.jpg" />
                    {product.name}
                </Col>
                <Col className="col" sm={3}>
                    <button className="modify-quantity-button" onClick={decrementQuantityCallback}>-</button>
                    <div className="quantity-label">
                        {product.quantity}
                    </div>
                    <button disabled={product.stock === product.quantity} className="modify-quantity-button" onClick={incrementQuantityCallback}>+</button>
                </Col>
                <Col className="price-col" sm={3}>
                    <p className="total-price">${product.price * product.quantity}</p>
                    <p>${product.price} / per item</p>
                </Col>
            </Row>
        </Wrapper>
    );
}

export default CartProductCard;