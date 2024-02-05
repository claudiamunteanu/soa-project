import { useCallback, useState } from "react";
import OrdersAPI from "../../apis/OrdersAPI";
import { getCartTotalPrice, isPersistedLocalStorageState } from "../../helpers";
import Button from "../Button";
import { Wrapper } from "./CartSummaryTotal.styles"
import Spinner from "../Spinner";
import { toast } from "react-toastify";

const CartSummaryTotal = ({ products }) => {
    const [isLoading, setIsLoading] = useState(false);

    const totalPrice = getCartTotalPrice(products);
    const delivery = 12;
    const total = totalPrice > 0 ? totalPrice + delivery : 0;

    const ordersApi = new OrdersAPI();

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

    const handleSubmit = async () => {
        if(isLoading)
            return;
        setIsLoading(true);
        const cartProducts = isPersistedLocalStorageState("cart");
        const result = await ordersApi.sendOrder(cartProducts);
        setIsLoading(false);
        if(result.error){
            showErrorToast(result.message);
        }
    }

    return (
        <Wrapper>
            <div className="price-row">
                <p>Total price:</p>
                <p>${totalPrice}</p>
            </div>
            {totalPrice > 0 && <div className="price-row">
                <p>Delivery:</p>
                <p>${delivery}</p>
            </div>}
            <div className="divider"/>
            <div className="total-price-row">
                <p>Total:</p>
                <p>${total}</p>
            </div>
            {isLoading && <Spinner/>}
            {totalPrice > 0 && <Button isEnabled={true} callback={handleSubmit} text="Send order"/>}
        </Wrapper>
    )
}

export default CartSummaryTotal;