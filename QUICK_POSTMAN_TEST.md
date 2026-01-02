# Quick Postman Testing Guide

## 🚀 Fast Steps

### Step 1: Register User

1. **Open Postman**
2. **Create new request:**
   - Method: `POST`
   - URL: `http://localhost:5053/api/Acount/Register`
   - Headers: `Content-Type: application/json`
   - Body (raw JSON):
   ```json
   {
     "name": "Test User",
     "email": "test@example.com",
     "password": "Test123456"
   }
   ```
3. **Click Send**
4. Should see: `"Created"`

---

### Step 2: Login (Get Token)

1. **Create new request:**
   - Method: `POST`
   - URL: `http://localhost:5053/api/Acount/Login`
   - Headers: `Content-Type: application/json`
   - Body (raw JSON):
   ```json
   {
     "email": "test@example.com",
     "password": "Test123456"
   }
   ```
2. **Click Send**
3. **Copy the `token` value** from response:
   ```json
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
   }
   ```

---

### Step 3: Test Protected Endpoint

1. **Create new request:**
   - Method: `GET`
   - URL: `http://localhost:5053/api/Acount/GetMyInfo`

2. **Add Authorization:**
   - Go to **"Authorization"** tab
   - Type: Select **"Bearer Token"**
   - Token: Paste your token here

   **OR** use Headers:
   - Go to **"Headers"** tab
   - Key: `Authorization`
   - Value: `Bearer YOUR_TOKEN_HERE`

3. **Click Send**

**Expected Result:**
- ✅ Status: `200 OK`
- ✅ Returns your user information

**Without Token:**
- ❌ Status: `401 Unauthorized`

---

## 📝 Visual Guide

### Login Request:
```
POST http://localhost:5053/api/Acount/Login

Headers:
  Content-Type: application/json

Body (raw - JSON):
{
  "email": "test@example.com",
  "password": "Test123456"
}
```

### Protected Request:
```
GET http://localhost:5053/api/Acount/GetMyInfo

Headers:
  Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

---

## ✅ What You Can Test

1. **Without Token** → Should get `401 Unauthorized`
2. **With Invalid Token** → Should get `401 Unauthorized`
3. **With Valid Token** → Should get `200 OK` with user info

---

## 💡 Pro Tip

Use Postman's **Authorization** tab:
1. Select **"Bearer Token"** type
2. Paste token once
3. Postman adds it automatically to all requests

---

**That's it! Happy testing! 🎉**

