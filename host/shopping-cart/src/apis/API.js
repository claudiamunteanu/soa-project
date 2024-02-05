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

    async get(url, token) {
        const config = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
            }
        };

        return await fetch(url, {
            ...config
        })
            .then(response => {
                return this.handleResponse(response)
            });
    }

    async post(url, body = {}, token) {
        const config = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
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