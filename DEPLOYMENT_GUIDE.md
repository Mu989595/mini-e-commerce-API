# Deployment Guide - How to Share Your App with Clients

This guide explains different ways to deploy and share your Web API and Frontend application.

---

## 📋 Table of Contents

1. [Quick Local Testing](#quick-local-testing)
2. [Deployment Options Overview](#deployment-options-overview)
3. [Option 1: Azure Deployment (Recommended)](#option-1-azure-deployment-recommended)
4. [Option 2: Simple Hosting (IIS)](#option-2-simple-hosting-iis)
5. [Option 3: Docker Deployment](#option-3-docker-deployment)
6. [Frontend Deployment](#frontend-deployment)
7. [Production Checklist](#production-checklist)

---

## 🚀 Quick Local Testing

For quick testing on the same network:

### Step 1: Run API
1. Open Visual Studio
2. Run the API project
3. Note the URL (usually `http://localhost:5053`)

### Step 2: Update Frontend
Edit `app.js` and change:
```javascript
const API_BASE_URL = 'http://YOUR_COMPUTER_IP:5053/api';
// Example: const API_BASE_URL = 'http://192.168.1.100:5053/api';
```

### Step 3: Find Your IP Address
```powershell
# In PowerShell
ipconfig
# Look for "IPv4 Address" (e.g., 192.168.1.100)
```

### Step 4: Share
- Share the frontend folder with client
- Tell them to open `index.html`
- Make sure your computer is on the same network

**Limitations:**
- Only works on same network
- Your computer must be running
- Not secure for production

---

## 🌐 Deployment Options Overview

### For Production (Recommended):
1. **Azure** - Microsoft cloud platform (Best for .NET)
2. **AWS** - Amazon cloud platform
3. **Heroku** - Simple cloud hosting
4. **IIS** - Windows Server hosting

### For Development/Testing:
1. **Local Network** - Same Wi-Fi
2. **ngrok** - Tunnel to localhost (Free)
3. **Cloudflare Tunnel** - Free tunnel service

---

## ☁️ Option 1: Azure Deployment (Recommended)

Azure is the easiest way to deploy .NET applications.

### Prerequisites:
- Azure account (Free tier available)
- Visual Studio with Azure tools

### Steps:

#### Part A: Deploy API to Azure

1. **Right-click on your WebAPIDotNet project**
   - Select "Publish"
   - Choose "Azure" → "Azure App Service (Windows)"
   - Sign in to Azure account

2. **Create App Service:**
   - Resource Group: Create new (e.g., "WebAPI-RG")
   - App Service: Create new (e.g., "webapidotnet-api")
   - Plan: Create new (Free tier available)
   - Click "Create"

3. **Configure Connection String:**
   - In Azure Portal → Your App Service → Configuration
   - Add Connection String:
     - Name: `DefaultConnection` or match your connection string name
     - Value: Your SQL Server connection string
   - Or use Azure SQL Database

4. **Publish:**
   - Click "Publish" in Visual Studio
   - Wait for deployment
   - Your API URL: `https://webapidotnet-api.azurewebsites.net`

#### Part B: Deploy Frontend

1. **Option 1: Azure Static Web Apps (Recommended)**
   - Create Static Web App in Azure Portal
   - Upload frontend files
   - URL: `https://your-app.azurestaticapps.net`

2. **Option 2: Azure Storage Static Website**
   - Create Storage Account
   - Enable Static Website
   - Upload HTML/CSS/JS files
   - URL: `https://yourstorage.z13.web.core.windows.net`

3. **Update Frontend API URL:**
   ```javascript
   const API_BASE_URL = 'https://webapidotnet-api.azurewebsites.net/api';
   ```

#### Cost:
- **Free tier available** for testing
- Free SQL Database available (limited)
- Pay-as-you-go for production

---

## 🖥️ Option 2: Simple Hosting (IIS)

Host on your own Windows server or computer.

### Steps:

#### Part A: Publish API

1. **Publish API:**
   ```powershell
   dotnet publish -c Release -o ./publish
   ```

2. **Install IIS:**
   - Windows Features → Enable IIS
   - Install .NET Hosting Bundle from Microsoft

3. **Create Website in IIS:**
   - Open IIS Manager
   - Right-click "Sites" → "Add Website"
   - Name: "WebAPI"
   - Physical path: Point to `publish` folder
   - Binding: `http://localhost:80` (or your port)
   - Click OK

4. **Configure Database:**
   - Update `appsettings.json` with production connection string
   - Or use environment variables

5. **Access API:**
   - `http://your-server-ip/api/Acount/Login`

#### Part B: Host Frontend

1. **Option 1: Same IIS Site:**
   - Copy frontend files to `wwwroot` folder
   - Access: `http://your-server-ip/index.html`

2. **Option 2: Separate IIS Site:**
   - Create new website for frontend
   - Point to frontend folder

3. **Update API URL in Frontend:**
   ```javascript
   const API_BASE_URL = 'http://your-server-ip/api';
   ```

---

## 🐳 Option 3: Docker Deployment

Containerize your application for easy deployment.

### Steps:

#### Create Dockerfile for API:

```dockerfile
# Dockerfile (in WebAPIDotNet folder)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAPIDotNet.csproj", "./"]
RUN dotnet restore "WebAPIDotNet.csproj"
COPY . .
RUN dotnet build "WebAPIDotNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPIDotNet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPIDotNet.dll"]
```

#### Build and Run:

```bash
# Build image
docker build -t webapi-dotnet .

# Run container
docker run -d -p 8080:80 --name myapi webapi-dotnet
```

---

## 📱 Frontend Deployment

### Option 1: Netlify (Easiest - Free)

1. Go to [netlify.com](https://netlify.com)
2. Sign up (free)
3. Drag and drop your frontend folder
4. Get URL: `https://your-app.netlify.app`
5. Update API URL in `app.js`

### Option 2: Vercel (Free)

1. Go to [vercel.com](https://vercel.com)
2. Sign up
3. Import project
4. Deploy
5. Get URL: `https://your-app.vercel.app`

### Option 3: GitHub Pages (Free)

1. Create GitHub repository
2. Upload frontend files
3. Settings → Pages
4. Select branch
5. Get URL: `https://username.github.io/repo-name`

### Option 4: Simple HTTP Server

For local sharing:
```bash
# Python
python -m http.server 8000

# Node.js
npx http-server

# Then access: http://localhost:8000
```

---

## 🔒 Production Checklist

Before deploying to production:

### Security:
- [ ] Change JWT SecretKey in `appsettings.json`
- [ ] Use HTTPS (SSL certificate)
- [ ] Enable CORS only for your frontend domain
- [ ] Remove debug/development code
- [ ] Use strong passwords for database
- [ ] Enable authentication/authorization on all endpoints

### Configuration:
- [ ] Update connection string for production database
- [ ] Set correct API URLs in frontend
- [ ] Configure environment variables
- [ ] Set up logging
- [ ] Configure error handling

### Database:
- [ ] Backup database
- [ ] Run migrations: `dotnet ef database update`
- [ ] Test database connection
- [ ] Set up database backups

### Performance:
- [ ] Enable caching
- [ ] Optimize database queries
- [ ] Enable compression
- [ ] Set up CDN for frontend assets

### Monitoring:
- [ ] Set up application monitoring
- [ ] Configure error tracking
- [ ] Set up alerts
- [ ] Monitor API usage

---

## 🚀 Quick Deploy Commands

### Publish API:
```powershell
dotnet publish -c Release -o ./publish
```

### Run Migrations:
```powershell
dotnet ef database update
```

### Test API Locally:
```powershell
cd publish
dotnet WebAPIDotNet.dll
```

---

## 📝 Example: Complete Azure Deployment

### 1. API:
```
Azure Portal → Create Resource → Web App
Name: webapidotnet-api
Publish: Code
Runtime: .NET 8
Region: Choose closest
```

### 2. Database:
```
Azure Portal → Create Resource → SQL Database
Name: webapidotnet-db
Server: Create new SQL Server
```

### 3. Frontend:
```
Azure Portal → Create Resource → Static Web App
Name: webapidotnet-frontend
```

### 4. Update Connection String:
In Azure Portal → Your API → Configuration → Connection Strings
```
DefaultConnection = Server=tcp:your-server.database.windows.net;Database=webapidotnet-db;...
```

### 5. Update Frontend:
```javascript
const API_BASE_URL = 'https://webapidotnet-api.azurewebsites.net/api';
```

### 6. Deploy:
- Visual Studio → Publish → Azure
- Or: Azure Portal → Deployment Center → Connect GitHub

---

## 🌍 Using ngrok for Quick Testing

Share your local API with anyone:

1. **Download ngrok:** [ngrok.com](https://ngrok.com)
2. **Run:**
   ```bash
   ngrok http 5053
   ```
3. **Get URL:** `https://abc123.ngrok.io`
4. **Update frontend:**
   ```javascript
   const API_BASE_URL = 'https://abc123.ngrok.io/api';
   ```
5. **Share frontend folder**

**Free tier limitations:**
- URL changes each time
- Limited requests
- Good for testing only

---

## 📞 Support Resources

- **Azure Documentation:** https://learn.microsoft.com/en-us/azure/
- **.NET Deployment:** https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/
- **IIS Setup:** https://learn.microsoft.com/en-us/iis/

---

## 💡 Recommended for Beginners

**Easiest Path:**
1. **API:** Deploy to Azure App Service (Free tier)
2. **Frontend:** Deploy to Netlify (Free)
3. **Database:** Use Azure SQL Database (Free tier available)

**Total Cost:** FREE for testing/small apps

---

Good luck with your deployment! 🚀

