# Mini E-Commerce API

This project is a mini e-commerce API built with ASP.NET Core, providing core functionalities for user authentication and product management.

## Tech Stack

*   ASP.NET 9 Core Web API
*   Entity Framework Core
*   SQL Server
*   ASP.NET Core Identity
*   JWT (JSON Web Tokens) for authentication

## Features

*   **User Registration:** Allows new users to register with the system.
*   **User Login:** Authenticates users and generates a JWT for authorization.
*   **JWT Token Generation:** Securely generates JWT tokens for authenticated users.
*   **Product Management:** (Assuming you have CRUD operations for products) - You can add more specific details here.
*   **Category Management:** (Assuming you have CRUD operations for categories) - You can add more specific details here.

## Setup

Follow these steps to get the project up and running on your local machine:

1.  **Database Setup (Entity Framework Core Migrations):**
    If you have not already, run the following commands in your Package Manager Console (or terminal if using `dotnet ef` commands) to create and update your database:

    ```bash
    Update-Database
    ```
    *Note: Ensure you have the Entity Framework Core tools installed.* If you encounter issues, you may need to add a migration first:
    ```bash
    Add-Migration InitialCreate
    Update-Database
    ```

2.  **Configure Connection String:**
    Open the `appsettings.json` file and update the `DefaultConnection` string under `ConnectionStrings` to point to your SQL Server instance. Replace `"YourServerPath"` with your actual SQL Server connection string.

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=Your_Server_Name;Database=Your_Database_Name;Integrated Security=True;TrustServerCertificate=True"
    }
    ```

3.  **Configure JWT Secret Key:**
    Open the `appsettings.json` file and update the `SecretKey` in the `JWT` section. Replace `"YourSecretKeyHere"` with a strong, secret key of your choice. This key is crucial for the security of your JWT tokens.

    ```json
    "JWT": {
        "SecretKey": "Your_Strong_And_Secret_Key_Here",
        "Issuer": "MiniEcommerceBackend",
        "Audience": "MiniEcommerceFrontend",
        "DurationInDays": "7"
    }
    ```

4.  **Run the Application:**
    You can run the application using Visual Studio or from the terminal:

    ```bash
    dotnet run
    ```

## Postman Collection

To easily test the API endpoints, import the Postman Collection located in the `Postman_Collection` folder into your Postman application. This collection contains pre-configured requests for registration, login, and other API functionalities. (The Postman Collection file needs to be exported from your Postman environment and placed in the `Postman_Collection` folder.)