import { Wrapper, Content } from './Login.styles'
import { useState } from "react";
import AuthAPI from "../../apis/AuthAPI";
import { isPersistedLocalStorageState } from "../../helpers";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import Spinner from "../../components/Spinner";
import Button from '../../components/Button';
import 'react-toastify/dist/ReactToastify.css';

const Login = () => {
    const [emailAddress, setEmailAddress] = useState('');
    const [password, setPassword] = useState('');

    const [loading, setLoading] = useState(false)

    const authApi = new AuthAPI();

    const validateLoginForm = (email, password) => {
        if (email === ''){
            return {error: true, message: 'Email field is required!'}
        }
        if (password === ''){
            return {error: true, message: 'Password field is required!'}
        }
        const validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
        if (!email.match(validRegex)){
            return {error: true, message: 'Invalid email format!'}
        }
        return {error: false, message: ""}
    }


    const handleInput = e => {
        const name = e.currentTarget.name;
        const value = e.currentTarget.value;

        switch (name) {
            case 'emailAddress':
                setEmailAddress(value);
                break;
            case 'password':
                setPassword(value);
                break;
            default:
                break;
        }
    };

    let navigate = useNavigate()

    const showErrorToast = (message) => {
        toast.error(message, {
            position: "top-center",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }

    const handleSubmit = async () => {
        setLoading(true)
        const result = validateLoginForm(emailAddress, password);
        if (result.error) {
            setLoading(false)
            showErrorToast(result.message)
            return;
        }
        const loginResult = await authApi.login(emailAddress, password);
        if (loginResult.error) {
            setLoading(false)
            showErrorToast(loginResult.message)
            return;
        }
        const sessionState = isPersistedLocalStorageState('user')
        if (sessionState) {
            setLoading(false)
            navigate('/product-catalog')
        }
        setLoading(false)
    }

    document.title = "Login"

    return (
        <Wrapper>
            <Content>
                {loading && <Spinner />}
                <input
                    type='email'
                    value={emailAddress}
                    name='emailAddress'
                    onChange={handleInput}
                    placeholder='Email address'
                />
                <input
                    type='password'
                    value={password}
                    name='password'
                    onChange={handleInput}
                    placeholder='Password'
                />
                <Button text='Submit' name="submitButton" callback={handleSubmit} isEnabled={true} />
            </Content>
        </Wrapper>
    )
}

export default Login;