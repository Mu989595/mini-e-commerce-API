// Login Page Script
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('login-form');
    const errorMessage = document.getElementById('error-message');
    const passwordInput = document.getElementById('password');
    const togglePasswordBtn = document.getElementById('toggle-password');
    const eyeIcon = document.getElementById('eye-icon');

    // Toggle password visibility
    if (togglePasswordBtn && eyeIcon) {
        togglePasswordBtn.addEventListener('click', () => {
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);
            eyeIcon.textContent = type === 'password' ? '👁️' : '🙈';
        });
    }

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
            let errorMsg = error.message || 'An error occurred during login';
            
            // Provide more helpful error messages
            if (errorMsg.includes('Cannot connect')) {
                errorMsg = 'Cannot connect to the server. Please make sure:\n1. The API is running\n2. CORS is enabled in the API\n3. The API URL is correct in js/api.js';
            }
            
            errorMessage.textContent = errorMsg;
            errorMessage.style.display = 'block';
        }
    });
});
