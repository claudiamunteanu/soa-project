import { Wrapper } from "./OrderConfirmation.styles";
import './GlobalStyle';
import { GlobalStyle } from "./GlobalStyle";
import Button from "../../components/Button";
import { Navigate, useLocation, useNavigate } from "react-router-dom";

const OrderConfirmation = () => {
    const { state } = useLocation();

    if (!state?.fromApp) {
        return <Navigate to="/" />;
    }

    const navigate = useNavigate();

    const goToStore = () => {
        navigate("/");
    }
    return (
        <>
            <Wrapper>
                <h1>Thank you!</h1>
                <h3>Order number: {state?.orderNumber}</h3>
                <p> We are getting started on your order right away, and you will receive an order confirmation email shortly. In the meantime, explore the freshest products in our store.</p>
                <Button className="store-button" callback={goToStore} text="Go back to the store"/>
            </Wrapper>
            <GlobalStyle/>
        </>
    );
}

export default OrderConfirmation;