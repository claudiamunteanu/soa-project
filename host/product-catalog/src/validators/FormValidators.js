export const validateProduct = (name, price, stock, photo) => {
    if (name === '') {
        return { error: true, message: "The name cannot be empty!" }
    }
    if (price < 0.1) {
        return { error: true, message: "The price has to be at least 0.1!" }
    }
    if (photo === '') {
        return { error: true, message: "The photo cannot be empty!" }
    }
    return { error: false, message: '' }
}