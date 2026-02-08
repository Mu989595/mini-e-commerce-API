// Register Page Script
document.addEventListener('DOMContentLoaded', () => {
    const registerForm = document.getElementById('register-form');
    const errorMessage = document.getElementById('error-message');
    const successMessage = document.getElementById('success-message');
    const passwordInput = document.getElementById('password');
    const togglePasswordBtn = document.getElementById('toggle-password');
    const eyeIcon = document.getElementById('eye-icon');

    // Password validation requirements
    const requirements = {
        length: { element: document.getElementById('req-length'), test: (pwd) => pwd.length > 5 },
        uppercase: { element: document.getElementById('req-uppercase'), test: (pwd) => /[A-Z]/.test(pwd) },
        number: { element: document.getElementById('req-number'), test: (pwd) => /[0-9]/.test(pwd) }
    };

    // Toggle password visibility
    togglePasswordBtn.addEventListener('click', () => {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        eyeIcon.textContent = type === 'password' ? '👁️' : '🙈';
    });

    // Validate password in real-time
    passwordInput.addEventListener('input', () => {
        const password = passwordInput.value;
        
        // Check each requirement
        Object.keys(requirements).forEach(key => {
            const req = requirements[key];
            if (req.test(password)) {
                req.element.classList.add('valid');
            } else {
                req.element.classList.remove('valid');
            }
        });
    });

    // Validate password before submission
    function validatePassword(password) {
        const errors = [];
        
        if (password.length <= 5) {
            errors.push('Password must be more than 5 characters');
        }
        
        if (!/[A-Z]/.test(password)) {
            errors.push('Password must contain at least 1 uppercase letter');
        }
        
        if (!/[0-9]/.test(password)) {
            errors.push('Password must contain at least 1 number');
        }
        
        return {
            isValid: errors.length === 0,
            errors: errors
        };
    }

    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        errorMessage.style.display = 'none';
        successMessage.style.display = 'none';
        errorMessage.textContent = '';
        successMessage.textContent = '';

        // Validate password
        const passwordValidation = validatePassword(password);
        if (!passwordValidation.isValid) {
            errorMessage.textContent = passwordValidation.errors.join('. ');
            errorMessage.style.display = 'block';
            return;
        }

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
            let errorMsg = error.message || 'An error occurred during registration';
            
            // Provide more helpful error messages
            if (errorMsg.includes('Cannot connect')) {
                errorMsg = 'Cannot connect to the server. Please make sure:\n1. The API is running\n2. CORS is enabled in the API\n3. The API URL is correct in js/api.js';
            }
            
            errorMessage.textContent = errorMsg;
            errorMessage.style.display = 'block';
        }
    });
});
