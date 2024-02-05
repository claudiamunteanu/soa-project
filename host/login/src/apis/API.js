export class API {
    handleResponse(response) {
        if (response.ok) {
            return response.json();
        } else {
            return response.json().then(data => {
                return Promise.reject({
                    status: response.status,
                    statusText: data.message
                });
            })
        }
    }

    async get(url) {
        return await fetch(url)
            .then(response => {
                return this.handleResponse(response)
            });
    }

    async post(url, body = {}) {
        const config = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        return await fetch(url, {
            ...config,
            body: JSON.stringify(body)
        }).then(response => {
            return this.handleResponse(response)
        })
    }
}