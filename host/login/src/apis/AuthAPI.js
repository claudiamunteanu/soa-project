import {SERVER_URL} from "../config";
import { API } from "./API";

export default class AuthAPI{
    constructor() {
        this.api = new API();
    }

    async login(email, password){
        const loginData = {
            email: email,
            password: password
        }
        const endpoint = `${SERVER_URL}/accounts/login`
        return this.api.post(endpoint, loginData)
            .then(response => {
                 if(!response.message){
                     const session = {token: response.accessToken, tokenExpiry: response.accessTokenExpiration, refreshToken: response.refreshToken, email: email};
                     localStorage.setItem("user", JSON.stringify(session));
                 }
                 return {error: false, data: response}
            }).catch((error) => {
                return {error: true, message: error.statusText};
            });
    }
}