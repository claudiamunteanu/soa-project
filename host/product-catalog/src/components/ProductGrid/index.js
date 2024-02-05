import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import ProductCard from "../ProductCard"
import { Wrapper } from "./ProductGrid.styles";

const ProductGrid = ({ products, isAdmin = false, editProductCallback, deleteProductCallback }) => {
    return (
        <>
            <Wrapper>
                <Container fluid>
                    <Row>
                        {products.map((product) => (
                            <Col key={product.id} xxl={3} xl={4} md={6}>
                                <ProductCard product={product} isAdmin={isAdmin} editProductCallback={editProductCallback} deleteProductCallback={deleteProductCallback} />
                            </Col>
                        ))}
                    </Row>
                </Container>
            </Wrapper>
        </>
    )
}

export default ProductGrid