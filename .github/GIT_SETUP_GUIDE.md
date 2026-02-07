# GitHub Setup Guide - Step by Step

This guide will help you upload your project to GitHub.

---

## 📋 Prerequisites

1. **Git installed** - Download from: https://git-scm.com/downloads
2. **GitHub account** - Sign up at: https://github.com

---

## 🚀 Step-by-Step Setup

### Step 1: Create GitHub Repository

1. Go to [github.com](https://github.com)
2. Click the **"+"** icon → **"New repository"**
3. Repository name: `WebAPIDotNet` (or any name)
4. Description: "Web API with Authentication and JWT"
5. Choose: **Public** (for free) or **Private**
6. **DON'T** check "Initialize with README" (we'll do it manually)
7. Click **"Create repository"**

---

### Step 2: Initialize Git in Your Project

1. Open **PowerShell** or **Command Prompt**
2. Navigate to your project folder:
   ```powershell
   cd E:\My_Projects\WebAPIDotNet
   ```

3. **Initialize Git:**
   ```powershell
   git init
   ```

---

### Step 3: Create .gitignore File

This prevents uploading sensitive files and unnecessary files.

I'll create this for you in the next step.

---

### Step 4: Add Files to Git

```powershell
# Add all files
git add .

# Check what will be committed
git status
```

---

### Step 5: Create First Commit

```powershell
git commit -m "Initial commit: Web API with JWT Authentication"
```

---

### Step 6: Connect to GitHub

Copy the repository URL from GitHub (e.g., `https://github.com/yourusername/WebAPIDotNet.git`)

```powershell
git remote add origin https://github.com/yourusername/WebAPIDotNet.git
```

**Replace `yourusername` with your GitHub username!**

---

### Step 7: Push to GitHub

```powershell
git branch -M main
git push -u origin main
```

You'll be asked to login to GitHub.

---

## 📝 What to Upload

### ✅ Upload These:
- Source code (Controllers, Models, Services, etc.)
- Frontend files (HTML, CSS, JS)
- Configuration files (except secrets)
- README files

### ❌ Don't Upload:
- `bin/` folder
- `obj/` folder
- `appsettings.json` (contains connection strings)
- `appsettings.Development.json`
- User secrets
- Database files

---

## 🔒 Important: Protect Sensitive Data

**Never commit:**
- Connection strings with passwords
- JWT secret keys
- API keys
- Personal information

We'll create `.gitignore` to prevent this automatically.

---

## 📚 Next Steps After GitHub

1. Clone repository on other machines
2. Deploy from GitHub to Azure
3. Collaborate with others
4. Track changes and versions

---

## 🆘 Troubleshooting

### "Authentication failed"
- Use GitHub Personal Access Token instead of password
- Generate token: GitHub Settings → Developer settings → Personal access tokens

### "Repository not found"
- Check repository URL is correct
- Make sure repository exists on GitHub

### "Files too large"
- Check `.gitignore` is working
- Remove large files from commit history

---

Need help? Check the full guide in the repository!


