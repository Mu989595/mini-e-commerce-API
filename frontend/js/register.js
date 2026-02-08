// Register Page Script
document.addEventListener('DOMContentLoaded', () => {
    const registerForm = document.getElementById('register-form');
    const errorMessage = document.getElementById('error-message');
    const successMessage = document.getElementById('success-message');

    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        errorMessage.style.display = 'none';
        successMessage.style.display = 'none';
        errorMessage.textContent = '';
        successMessage.textContent = '';

        try {
            const response = await api.post('/Account/Register', {
                userName: username,
                email: email,
                password: password
            });

            if (response.isSuccess) {
                successMessage.textContent = 'Registration successful! Redirecting to login...';
                successMessage.style.display = 'block';
                
                // Redirect to login page after 2 seconds
                setTimeout(() => {
                    window.location.href = 'login.html';
                }, 2000);
            } else {
                errorMessage.textContent = response.message || 'Registration failed';
                errorMessage.style.display = 'block';
            }
        } catch (error) {
            errorMessage.textContent = error.message || 'An error occurred during registration';
            errorMessage.style.display = 'block';
        }
    });
});
