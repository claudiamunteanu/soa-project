import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { Wrapper } from './TopBar.styles';
import { getUserRole, isPersistedLocalStorageState } from '../../helpers';
import { useNavigate } from "react-router-dom";
import Button from '../Button';
import { FaShoppingCart } from "react-icons/fa";
import { useSelector } from 'react-redux';

const TopBar = () => {
    const cartSize = useSelector((state) => state.cartSize)

    let navigate = useNavigate()

    const user = isPersistedLocalStorageState("user");
    var isAdmin = false;

    if (user) {
        const role = getUserRole(user.token);
        isAdmin = role == "Admin"
    }

    const handleLogout = () => {
        localStorage.clear()
        navigate('/')
    }

    return (
        <Wrapper>
            <Navbar collapseOnSelect expand="md">
                <Container fluid>
                    <Navbar.Brand href="/">Online Shop</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="ms-auto">
                            {user && (
                                <>
                                    {!isAdmin &&
                                        <>
                                            <Nav.Link href="/cart" value={cartSize}>
                                                <FaShoppingCart />
                                            </Nav.Link>
                                        </>
                                    }
                                    <Button className="button" callback={handleLogout} text="Logout" />
                                </>
                            )}
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </Wrapper>
    );
}

export default TopBar;