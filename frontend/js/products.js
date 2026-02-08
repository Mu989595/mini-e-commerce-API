// Products Page Script
document.addEventListener('DOMContentLoaded', async () => {
    // Check authentication
    if (!auth.isAuthenticated()) {
        window.location.href = 'login.html';
        return;
    }

    // Update navbar
    auth.updateNavbar();

    const productsContainer = document.getElementById('products-container');
    const loading = document.getElementById('loading');
    const errorMessage = document.getElementById('error-message');

    try {
        // Fetch products from API
        const products = await api.get('/Product');

        loading.style.display = 'none';

        if (!products || products.length === 0) {
            productsContainer.innerHTML = '<p class="loading">No products available</p>';
            return;
        }

        // Display products
        productsContainer.innerHTML = products.map(product => `
            <div class="product-card">
                <h3>${product.name}</h3>
                <div class="product-price">$${product.price}</div>
                <div class="product-category">Category ID: ${product.catogryId}</div>
            </div>
        `).join('');

    } catch (error) {
        loading.style.display = 'none';
        errorMessage.textContent = error.message || 'Failed to load products';
        errorMessage.style.display = 'block';
    }
});
