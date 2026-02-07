# Postman Guide - Testing API with JWT Tokens

Complete guide on how to test your API using Postman, including authentication with JWT tokens.

---

## 📋 Prerequisites

1. **Postman installed** - Download from: https://www.postman.com/downloads/
2. **API running** - Make sure your API is running on `http://localhost:5053`
3. **Postman account** (optional but recommended)

---

## 🚀 Step-by-Step: Testing Your API

### Step 1: Test Registration

#### Create Request:
1. Open Postman
2. Click **"New"** → **"HTTP Request"**
3. Set method to **POST**
4. Enter URL: `http://localhost:5053/api/Acount/Register`
5. Go to **"Body"** tab
6. Select **"raw"** and **"JSON"**
7. Enter this JSON:
```json
{
  "name": "Ahmed Ali",
  "email": "ahmed@example.com",
  "password": "SecurePass123"
}
```

#### Send Request:
8. Click **"Send"**
9. You should see: `"Created"` or success message

**Expected Response:**
- Status: `200 OK`
- Body: `"Created"`

---

### Step 2: Get JWT Token (Login)

#### Create Request:
1. Click **"New"** → **"HTTP Request"**
2. Set method to **POST**
3. Enter URL: `http://localhost:5053/api/Acount/Login`
4. Go to **"Body"** tab
5. Select **"raw"** and **"JSON"**
6. Enter this JSON (use same email/password from registration):
```json
{
  "email": "ahmed@example.com",
  "password": "SecurePass123"
}
```

#### Send Request:
7. Click **"Send"**

**Expected Response:**
```json
{
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI...",
  "userId": "123e4567-e89b-12d3-a456-426614174000",
  "userName": "ahmed@example.com",
  "email": "ahmed@example.com",
  "name": "Ahmed Ali",
  "roles": []
}
```

#### Copy the Token:
8. **Copy the entire `token` value** - you'll need it for next steps!

---

### Step 3: Use Token in Protected Requests

#### Method 1: Authorization Header (Recommended)

1. Create new request (GET, POST, etc.)
2. Go to **"Headers"** tab
3. Add new header:
   - **Key:** `Authorization`
   - **Value:** `Bearer YOUR_TOKEN_HERE`
   
   Example:
   ```
   Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
   ```

4. Send request

#### Method 2: Using Postman Authorization Tab

1. Create new request
2. Go to **"Authorization"** tab
3. Select **Type:** `Bearer Token`
4. Paste your token in **"Token"** field
5. Postman automatically adds it to headers
6. Send request

---

## 🔐 Creating a Protected Endpoint (Example)

Let's create an example protected endpoint to test with:

### Add to AcountController.cs:

```csharp
using Microsoft.AspNetCore.Authorization; // Add this using

[HttpGet("GetMyInfo")]
[Authorize]  // This requires authentication!
public async Task<IActionResult> GetMyInfo()
{
    // Get current user ID from token
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    if (userId == null)
    {
        return Unauthorized("User not found in token");
    }
    
    // Find user in database
    var user = await userManager.FindByIdAsync(userId);
    
    if (user == null)
    {
        return NotFound("User not found");
    }
    
    return Ok(new
    {
        UserId = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        Name = user.Name
    });
}
```

### Now Test Protected Endpoint:

1. Create GET request
2. URL: `http://localhost:5053/api/Acount/GetMyInfo`
3. Add Authorization header:
   - Key: `Authorization`
   - Value: `Bearer YOUR_TOKEN_HERE`
4. Send request

**With Valid Token:**
- Status: `200 OK`
- Returns user information

**Without Token or Invalid Token:**
- Status: `401 Unauthorized`
- Message: "Unauthorized"

---

## 📝 Postman Collection Setup

### Create Environment Variables (Recommended)

1. Click **"Environments"** (top left)
2. Click **"+"** to create new environment
3. Name it: `Local Development`
4. Add variables:
   - `base_url`: `http://localhost:5053`
   - `token`: (leave empty, will be set after login)
   - `api_base`: `{{base_url}}/api`

### Use Variables in Requests:

- URL: `{{api_base}}/Acount/Login`
- Header: `Authorization: Bearer {{token}}`

### Set Token Automatically:

1. After Login request, go to **"Tests"** tab
2. Add this script:
```javascript
if (pm.response.code === 200) {
    var jsonData = pm.response.json();
    pm.environment.set("token", jsonData.token);
    console.log("Token saved:", jsonData.token);
}
```

Now token is saved automatically! 🎉

---

## 🧪 Complete Testing Flow

### 1. Register New User
```
POST {{api_base}}/Acount/Register
Body: {
  "name": "Test User",
  "email": "test@example.com",
  "password": "Test123456"
}
```

### 2. Login (Get Token)
```
POST {{api_base}}/Acount/Login
Body: {
  "email": "test@example.com",
  "password": "Test123456"
}
→ Save token from response
```

### 3. Access Protected Endpoint
```
GET {{api_base}}/Acount/GetMyInfo
Headers: Authorization: Bearer {{token}}
```

---

## 🔍 Testing Different Scenarios

### Test 1: Valid Token
- ✅ Should return `200 OK`
- ✅ Should return user data

### Test 2: No Token
- Remove Authorization header
- ❌ Should return `401 Unauthorized`

### Test 3: Invalid Token
- Use fake token: `Bearer fake_token_123`
- ❌ Should return `401 Unauthorized`

### Test 4: Expired Token
- Wait for token to expire (7 days in your case)
- ❌ Should return `401 Unauthorized`

### Test 5: Wrong Format
- Use: `Token YOUR_TOKEN` (missing "Bearer")
- ❌ Should return `401 Unauthorized`

---

## 📸 Postman Screenshots Guide

### Login Request Setup:
```
Method: POST
URL: http://localhost:5053/api/Acount/Login
Headers: Content-Type: application/json
Body (raw JSON):
{
  "email": "ahmed@example.com",
  "password": "SecurePass123"
}
```

### Protected Request Setup:
```
Method: GET
URL: http://localhost:5053/api/Acount/GetMyInfo
Headers:
  - Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

---

## 🎯 Quick Reference

### Register
- **Method:** POST
- **URL:** `/api/Acount/Register`
- **Body:** JSON with name, email, password
- **Auth:** Not required

### Login
- **Method:** POST
- **URL:** `/api/Acount/Login`
- **Body:** JSON with email, password
- **Auth:** Not required
- **Response:** Contains `token`

### Protected Endpoints
- **Method:** GET/POST/PUT/DELETE
- **URL:** Any endpoint with `[Authorize]`
- **Headers:** `Authorization: Bearer TOKEN`
- **Auth:** **REQUIRED**

---

## 💡 Pro Tips

1. **Save Requests:** Create a Postman Collection to save all requests
2. **Use Variables:** Use environment variables for URLs and tokens
3. **Auto-Save Token:** Use Tests tab to automatically save token
4. **Organize:** Group related requests in folders
5. **Export Collection:** Share your collection with team

---

## 🆘 Troubleshooting

### "401 Unauthorized"
- ✅ Check token is copied correctly
- ✅ Make sure "Bearer " prefix is included (with space)
- ✅ Verify token hasn't expired
- ✅ Check API is running

### "Cannot connect"
- ✅ Verify API URL is correct
- ✅ Make sure API is running (`http://localhost:5053`)
- ✅ Check firewall settings

### "Token not working"
- ✅ Get fresh token by logging in again
- ✅ Check token format: `Bearer TOKEN_HERE`
- ✅ Verify token is not expired

---

## 📚 Next Steps

1. Test all endpoints
2. Create more protected endpoints
3. Test role-based authorization
4. Export Postman collection
5. Share with team/client

---

**Happy Testing! 🚀**

