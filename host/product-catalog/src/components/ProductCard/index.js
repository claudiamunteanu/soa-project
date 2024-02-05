import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { Wrapper } from "./ProductCard.styles"
import Image from 'react-bootstrap/Image';
import Button from "../Button";
import FormModal from "../Modals/FormModal";
import ConfirmModal from "../Modals/ConfirmModal";
import { useEffect, useState, useRef, useContext } from "react";
import { getProductCartQuantity, isPersistedLocalStorageState } from "../../helpers";
import { useDispatch } from "react-redux";

const ProductActions = ({ isAddedToCart, addToCartCallback, productStock, quantity, incrementQuantityCallback, decrementQuantityCallback }) => {
    if (isAddedToCart) {
        return <div className="modify-quantity">
            <button className="modify-quantity-button" onClick={decrementQuantityCallback}>-</button>
            <div className="quantity-label">
                {quantity}
            </div>
            <button disabled={productStock === quantity} className="modify-quantity-button" onClick={incrementQuantityCallback}>+</button>
        </div>
    }

    return <Button className="add-to-cart-button" text="Add to cart" callback={addToCartCallback} />
}

const ProductCard = ({ product, isAdmin = false, editProductCallback, deleteProductCallback }) => {
    const dispatch = useDispatch();
    const productCartQuantity = getProductCartQuantity(product.id);

    const [addedToCart, setAddedToCart] = useState(productCartQuantity > 0);
    const [quantity, setQuantity] = useState(productCartQuantity);

    const didMount = useRef(false);

    useEffect(() => {
        if (didMount.current)
            updateLocalCart();
        else
            didMount.current = true;    
    }, [quantity]);

    const addToCartCallback = () => {
        setAddedToCart(true);
        setQuantity(1);
        dispatch({type: 'INCREMENT_CART_SIZE'});
    }

    const incrementQuantityCallback = () => {
        setQuantity(prev => prev + 1);
        dispatch({type: 'INCREMENT_CART_SIZE'});
    }

    const decrementQuantityCallback = () => {
        if (quantity == 1) {
            setAddedToCart(false);
        }
        setQuantity(prev => prev - 1);
        dispatch({type: 'DECREMENT_CART_SIZE'});
    }

    const updateLocalCart = () => {
        var products = isPersistedLocalStorageState("cart");
        if (products) {
            var productIndex = products.map(p => p.id).indexOf(product.id);
            if (productIndex >= 0) {
                if (quantity > 0) {
                    products[productIndex].quantity = quantity;
                } else {
                    console.log("deleting")
                    products.splice(productIndex, 1);
                }
            } else {
                products.push({ ...product, quantity: quantity })
            }
            if (products.length > 0) {
                localStorage.setItem("cart", JSON.stringify(products));
            } else {
                localStorage.removeItem("cart");
            }
        } else {
            var cartProducts = [{ ...product, quantity: quantity }]
            localStorage.setItem("cart", JSON.stringify(cartProducts));
        }        
    }

    return (
        <Wrapper>
            <Image src={product.photo} />
            <Row>
                <Col className="product-name">
                    {product.name}
                </Col>
            </Row>
            <Row>
                <Col className="product-price">
                    ${product.price}
                </Col>
            </Row>
            {!isAdmin &&
                <Row>
                    <Col>
                        <ProductActions
                            isAddedToCart={addedToCart}
                            addToCartCallback={addToCartCallback}
                            quantity={quantity}
                            productStock={product.stock}
                            incrementQuantityCallback={incrementQuantityCallback}
                            decrementQuantityCallback={decrementQuantityCallback}
                        />
                    </Col>
                </Row>
            }
            {isAdmin &&
                <>
                    <Row>
                        <Col className="product-stock">
                            Quantity: {product.stock}
                        </Col>
                    </Row>
                    <Row>
                        <Col sm={6}>
                            <FormModal buttonName="updateProductButton" triggerText="Edit"
                                modalTitle="Edit product"
                                callbackText="Save"
                                callback={editProductCallback} updateProduct={true} product={product} />
                        </Col>
                        <Col sm={6}>
                            <ConfirmModal triggerText="Delete" modalTitle="Delete product" modalText="Do you wish to delete the product?" callback={() => deleteProductCallback(product)} />
                        </Col>
                    </Row>
                </>
            }
        </Wrapper>
    )
}

export default ProductCard