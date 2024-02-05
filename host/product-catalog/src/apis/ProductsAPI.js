import { SERVER_URL } from "../config";
import { API } from "./API";

export default class ProductsAPI {
    constructor() {
        this.api = new API();
    }

    async getAllProductsInStock(token) {
        const endpoint = `${SERVER_URL}/products/in-stock`
        return this.api.get(endpoint, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }

    async getAllProducts(token) {
        const endpoint = `${SERVER_URL}/products`
        return this.api.get(endpoint, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }

    async addProduct(product, token) {
        const productData = {
            name: product.name,
            stock: parseInt(product.stock),
            price: parseFloat(product.price),
            photo: product.photo
        }
        const endpoint = `${SERVER_URL}/products`
        return this.api.post(endpoint, productData, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }

    async editProduct(product, token) {
        const productData = {
            name: product.name,
            stock: parseInt(product.stock),
            price: parseFloat(product.price),
            photo: product.photo
        }
        const endpoint = `${SERVER_URL}/products/${product.id}`
        return this.api.put(endpoint, productData, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }

    async deleteProduct(productId, token) {
        const endpoint = `${SERVER_URL}/products/${productId}`
        return this.api.delete(endpoint, token)
            .then(response => {
                return { error: false, data: response }
            }).catch((error) => {
                return { error: true, message: error.statusText };
            });
    }
}