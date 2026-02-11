// API Configuration
// Update this URL to match your API port (check Properties/launchSettings.json)
const API_BASE_URL = 'http://localhost:5045/api'; // Default port from launchSettings.json

// API Helper Functions
const api = {
    async request(url, options = {}) {
        const token = localStorage.getItem('token');
        
        const headers = {
            'Content-Type': 'application/json',
            ...options.headers
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        try {
            const response = await fetch(`${API_BASE_URL}${url}`, {
                ...options,
                headers
            });

            // Handle 401 Unauthorized
            if (response.status === 401) {
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                window.location.href = 'login.html';
                return;
            }

            const data = await response.json();
            
            if (!response.ok) {
                throw new Error(data.message || 'An error occurred');
            }

            return data;
        } catch (error) {
            console.error('API Error:', error);
            // Handle network errors (CORS, connection issues, etc.)
            if (error.message === 'Failed to fetch' || error.name === 'TypeError') {
                const errorMsg = 'Cannot connect to the server.\n\n' +
                    'Please make sure:\n' +
                    '1. The API is running (run: dotnet run)\n' +
                    '2. Check the API port in Properties/launchSettings.json\n' +
                    '3. Update API_BASE_URL in js/api.js if needed\n' +
                    '4. The API URL is: ' + API_BASE_URL;
                throw new Error(errorMsg);
            }
            throw error;
        }
    },

    get(url) {
        return this.request(url, { method: 'GET' });
    },

    post(url, data) {
        return this.request(url, {
            method: 'POST',
            body: JSON.stringify(data)
        });
    },

    put(url, data) {
        return this.request(url, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
    },

    delete(url) {
        return this.request(url, { method: 'DELETE' });
    }
};
