# Web API .NET - Authentication System

A complete Web API with user registration, login, and JWT token authentication built with ASP.NET Core.

## 🚀 Features

- ✅ User Registration
- ✅ User Login
- ✅ JWT Token Authentication
- ✅ ASP.NET Core Identity
- ✅ Entity Framework Core
- ✅ Modern Frontend UI
- ✅ CORS Support

## 📁 Project Structure

```
WebAPIDotNet/
├── Controllers/
│   ├── AcountController.cs    # Registration & Login endpoints
│   └── ApplicationUser.cs     # User entity
├── Models/
│   ├── Context.cs             # Database context
│   ├── Department.cs
│   └── Employee.cs
├── DTO/
│   ├── RegisterDTO.cs         # Registration data model
│   └── LoginDTO.cs            # Login data model
├── Services/
│   └── JWTService.cs          # JWT token generation
└── WebAPIDotNet-Frontend/     # Frontend application
    ├── index.html
    ├── styles.css
    └── app.js
```

## 🛠️ Technology Stack

- **Backend:** ASP.NET Core 8.0
- **Database:** SQL Server (LocalDB)
- **Authentication:** ASP.NET Core Identity + JWT
- **Frontend:** HTML, CSS, JavaScript

## 📋 Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 (or VS Code)
- SQL Server (LocalDB included with Visual Studio)

## ⚙️ Setup Instructions

### 1. Clone Repository

```bash
git clone https://github.com/yourusername/WebAPIDotNet.git
cd WebAPIDotNet
```

### 2. Configure Database

1. Update `appsettings.json` with your connection string
2. Run migrations:
   ```bash
   dotnet ef database update
   ```

### 3. Configure JWT

Edit `appsettings.json`:
```json
{
  "JWT": {
    "SecretKey": "YourSuperSecretKeyForJWTTokenGenerationThatShouldBeAtLeast32CharactersLong!",
    "Issuer": "WebAPIDotNet",
    "Audience": "WebAPIDotNetUsers",
    "DurationInDays": 7
  }
}
```

### 4. Run the API

```bash
dotnet run
```

API will run on: `http://localhost:5053`

### 5. Run Frontend

1. Open `WebAPIDotNet-Frontend/index.html` in browser
2. Or use Live Server in VS Code

## 📚 API Endpoints

### Register
```
POST /api/Acount/Register
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "SecurePass123"
}
```

### Login
```
POST /api/Acount/Login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "SecurePass123"
}
```

**Response:**
```json
{
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "...",
  "userName": "john@example.com",
  "email": "john@example.com",
  "name": "John Doe",
  "roles": []
}
```

## 🔒 Security Notes

⚠️ **Important:** Before deploying to production:

1. Change JWT SecretKey in `appsettings.json`
2. Use HTTPS
3. Secure your connection string
4. Enable proper CORS settings
5. Review and harden security settings

## 📖 Documentation

- [Learning Guide](LEARNING_GUIDE.md) - Complete step-by-step explanation
- [Code Walkthrough](CODE_WALKTHROUGH.md) - Visual code explanation
- [Deployment Guide](DEPLOYMENT_GUIDE.md) - How to deploy
- [Git Setup Guide](.github/GIT_SETUP_GUIDE.md) - GitHub setup

## 🧪 Testing

### Using Swagger
1. Run the API
2. Navigate to: `http://localhost:5053/swagger`
3. Test endpoints directly

### Using Frontend
1. Open `WebAPIDotNet-Frontend/index.html`
2. Register a new user
3. Login to get JWT token

## 📝 License

This project is open source and available for learning purposes.

## 👤 Author

Your Name

## 🙏 Acknowledgments

- Built with ASP.NET Core
- Uses ASP.NET Core Identity
- Frontend built with vanilla JavaScript

---

**Happy Coding! 🚀**


