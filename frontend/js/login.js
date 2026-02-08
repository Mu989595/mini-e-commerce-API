// Login Page Script
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('login-form');
    const errorMessage = document.getElementById('error-message');

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        errorMessage.style.display = 'none';
        errorMessage.textContent = '';

        try {
            const response = await api.post('/Account/Login', {
                userName: username,
                password: password
            });

            if (response.isSuccess && response.data) {
                // Store user data and token
                auth.setUser({
                    userName: response.data.userName,
                    email: response.data.email
                }, response.data.token);

                // Redirect to products page
                window.location.href = 'products.html';
            } else {
                errorMessage.textContent = response.message || 'Login failed';
                errorMessage.style.display = 'block';
            }
        } catch (error) {
            errorMessage.textContent = error.message || 'An error occurred during login';
            errorMessage.style.display = 'block';
        }
    });
});
