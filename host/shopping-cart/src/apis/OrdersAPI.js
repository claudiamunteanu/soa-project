import { SERVER_URL } from "../config";
import { isPersistedLocalStorageState } from "../helpers";
import { API } from "./API";

export default class OrdersAPI {
    constructor() {
        this.api = new API();
    }

    async sendOrder(cartProducts) {
        const orderedProducts = cartProducts.map(p => {
            return {
                productId: p.id,
                quantity: p.quantity,
            }
        });
        const user = isPersistedLocalStorageState("user");
        const email = user.email;
        const token = user.token;
        const currentDate = new Date().toJSON().slice(0, 10);
        const data = {
            date: currentDate,
            placedByEmail: email,
            orderedProducts: orderedProducts,
        }
        const endpoint = `${SERVER_URL}/orders`
        return this.api.post(endpoint, data, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }
}