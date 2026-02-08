# Mini E-Commerce Frontend

This is the frontend application for the Mini E-Commerce API, built with React, TypeScript, and Tailwind CSS.

## Features

- ✅ User Registration
- ✅ User Login with JWT Authentication
- ✅ Products Page (displays products from API)
- ✅ Protected Routes (requires authentication)
- ✅ Responsive Design

## Setup Instructions

1. **Install Dependencies:**
   ```bash
   cd mapple-react-1.0.0
   npm install
   ```

2. **Configure API URL:**
   - Create a `.env` file in the `mapple-react-1.0.0` directory
   - Add your API URL:
     ```
     VITE_API_URL=http://localhost:5000/api
     ```
   - Replace `http://localhost:5000/api` with your actual API URL

3. **Run the Development Server:**
   ```bash
   npm run dev
   ```

4. **Build for Production:**
   ```bash
   npm run build
   ```

## Project Structure

```
src/
├── components/       # Reusable components (Navbar, Footer, etc.)
├── config/          # API configuration
├── context/         # React Context (AuthContext)
├── pages/           # Page components (Login, Register, Products)
├── services/        # API service functions
└── sections/        # Home page sections

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
4. Protected routes check authentication status

## Notes

- Make sure your API is running before testing the frontend
- The API URL must be configured correctly in the `.env` file
- CORS must be enabled on your API to allow requests from the frontend
