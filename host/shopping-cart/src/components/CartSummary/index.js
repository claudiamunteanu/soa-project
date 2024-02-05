import Container from "react-bootstrap/Container";
import CartProductCard from "../CartProductCard";
import { Wrapper } from "./CartSummary.styles";

const CartSummaryContent = ({products, setLoadAgain}) => {
    if(products.length > 0)
    return products.map((product, index) => (
            <CartProductCard key={index} product={product} setLoadAgain={setLoadAgain}/>
        ));
    else
    return <div className="empty-message">
        <h2>Your shopping cart is empty!</h2>
        <h3>Try do add something to your cart</h3>
        </div>
}

const CartSummary = ({ products, setLoadAgain }) => {
    return (
        <Wrapper>
            <Container fluid>
               <CartSummaryContent products={products} setLoadAgain={setLoadAgain}/>
            </Container>
        </Wrapper>
    );
}

export default CartSummary;