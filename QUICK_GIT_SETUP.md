# Quick Git Setup - 5 Minutes

## 🎯 Fast Setup

### Step 1: Create GitHub Repository (1 minute)

1. Go to [github.com](https://github.com) and sign in
2. Click **"+"** → **"New repository"**
3. Name: `WebAPIDotNet`
4. Choose **Public** or **Private**
5. **DON'T** check "Add README"
6. Click **"Create repository"**

### Step 2: Copy Repository URL

Copy the URL from GitHub (e.g., `https://github.com/yourusername/WebAPIDotNet.git`)

### Step 3: Open PowerShell in Project Folder

```powershell
cd E:\My_Projects\WebAPIDotNet
```

### Step 4: Run These Commands

```powershell
# Initialize Git
git init

# Add all files
git add .

# Create first commit
git commit -m "Initial commit: Web API with Authentication"

# Connect to GitHub (REPLACE with your URL!)
git remote add origin https://github.com/yourusername/WebAPIDotNet.git

# Push to GitHub
git branch -M main
git push -u origin main
```

### Step 5: Enter GitHub Credentials

When prompted:
- Username: Your GitHub username
- Password: Use a **Personal Access Token** (not your password!)

**How to create Personal Access Token:**
1. GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Generate new token
3. Select scope: `repo` (full control)
4. Copy the token and use it as password

---

## ✅ Done!

Your code is now on GitHub!

**Repository URL:** `https://github.com/yourusername/WebAPIDotNet`

---

## 🔒 Important

The `.gitignore` file I created will:
- ✅ Prevent uploading `appsettings.json` (contains secrets)
- ✅ Exclude `bin/` and `obj/` folders
- ✅ Protect sensitive files

**Never commit:**
- Connection strings with passwords
- JWT secret keys
- Personal data

---

## 📝 Next Steps

After uploading to GitHub:

1. **Clone on other machines:**
   ```bash
   git clone https://github.com/yourusername/WebAPIDotNet.git
   ```

2. **Make changes and push:**
   ```bash
   git add .
   git commit -m "Description of changes"
   git push
   ```

3. **Deploy from GitHub:**
   - Azure can deploy directly from GitHub
   - Netlify can deploy frontend from GitHub

---

## 🆘 Need Help?

- **Git not installed?** Download: https://git-scm.com/downloads
- **Authentication issues?** Use Personal Access Token
- **Repository not found?** Check the URL is correct

---

**Your code is safe and backed up! 🎉**


