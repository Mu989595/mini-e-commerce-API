# Mini E-Commerce Frontend (HTML/CSS/JavaScript)

Simple frontend application for the Mini E-Commerce API built with pure HTML, CSS, and JavaScript.

## Features

- ✅ User Registration
- ✅ User Login with JWT Authentication
- ✅ Products Page (displays products from API)
- ✅ Protected Routes (requires authentication)
- ✅ Responsive Design

## Setup Instructions

1. **Configure API URL:**
   - Open `js/api.js`
   - Change the `API_BASE_URL` to match your API URL:
     ```javascript
     const API_BASE_URL = 'http://localhost:5000/api';
     ```

2. **Open the Application:**
   - Simply open `index.html` in your web browser
   - Or use a local server (recommended):
     ```bash
     # Using Python
     python -m http.server 8000
     
     # Using Node.js (if you have http-server installed)
     npx http-server
     ```
   - Then visit: `http://localhost:8000`

## Project Structure

```
frontend/
├── index.html          # Home page
├── login.html         # Login page
├── register.html       # Registration page
├── products.html       # Products page
├── css/
│   └── style.css      # All styles
└── js/
    ├── api.js         # API configuration and helper functions
    ├── auth.js        # Authentication helper functions
    ├── login.js       # Login page logic
    ├── register.js    # Registration page logic
    ├── products.js    # Products page logic
    └── main.js        # Main page logic
```

## API Integration

The frontend connects to the following API endpoints:

- `POST /api/Account/Register` - User registration
- `POST /api/Account/Login` - User login (returns JWT token)
- `GET /api/Product` - Get all products (requires authentication)

## Authentication Flow

1. User registers or logs in
2. JWT token is stored in localStorage
3. Token is automatically included in API requests
4. Protected pages check authentication status

## Notes

- Make sure your API is running before testing the frontend
- The API URL must be configured correctly in `js/api.js`
- CORS must be enabled on your API to allow requests from the frontend
- For production, use a proper web server instead of opening HTML files directly
