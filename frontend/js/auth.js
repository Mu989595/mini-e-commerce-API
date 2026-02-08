// Authentication Helper Functions
const auth = {
    isAuthenticated() {
        return !!localStorage.getItem('token');
    },

    getUser() {
        const user = localStorage.getItem('user');
        return user ? JSON.parse(user) : null;
    },

    setUser(user, token) {
        localStorage.setItem('token', token);
        localStorage.setItem('user', JSON.stringify(user));
    },

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    },

    updateNavbar() {
        const isAuth = this.isAuthenticated();
        const user = this.getUser();
        
        const authLinks = document.getElementById('auth-links');
        const userInfo = document.getElementById('user-info');
        const usernameDisplay = document.getElementById('username-display');
        const loginLink = document.getElementById('login-link');
        const registerLink = document.getElementById('register-link');

        if (authLinks) {
            authLinks.style.display = isAuth ? 'none' : 'flex';
        }

        if (userInfo) {
            userInfo.style.display = isAuth ? 'flex' : 'none';
        }

        if (usernameDisplay && user) {
            usernameDisplay.textContent = `Welcome, ${user.userName}`;
        }

        if (loginLink && registerLink) {
            loginLink.style.display = isAuth ? 'none' : 'inline';
            registerLink.style.display = isAuth ? 'none' : 'inline';
        }
    }
};

// Update navbar on page load
document.addEventListener('DOMContentLoaded', () => {
    auth.updateNavbar();

    // Logout button handler
    const logoutBtn = document.getElementById('logout-btn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', () => {
            auth.logout();
            window.location.href = 'index.html';
        });
    }
});
