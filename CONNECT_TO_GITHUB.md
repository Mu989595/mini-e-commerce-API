# Connect to Your GitHub Repository

## Your Repository URL:
```
git@github.com:Mu989595/WebAPI.git
```

---

## 🚀 Option 1: Using SSH (If you have SSH keys set up)

If you've set up SSH keys with GitHub:

```powershell
cd E:\My_Projects\WebAPIDotNet

# Connect to GitHub
git remote add origin git@github.com:Mu989595/WebAPI.git

# Check connection
git remote -v

# Push to GitHub
git branch -M main
git push -u origin main
```

---

## 🌐 Option 2: Using HTTPS (Easier - Recommended)

If SSH doesn't work, use HTTPS instead:

### Step 1: Get HTTPS URL

Your HTTPS URL is:
```
https://github.com/Mu989595/WebAPI.git
```

### Step 2: Connect and Push

```powershell
cd E:\My_Projects\WebAPIDotNet

# Remove SSH remote if added
git remote remove origin

# Add HTTPS remote
git remote add origin https://github.com/Mu989595/WebAPI.git

# Verify
git remote -v

# Push to GitHub
git branch -M main
git push -u origin main
```

### Step 3: Authentication

When prompted:
- **Username:** `Mu989595`
- **Password:** Use a **Personal Access Token** (not your GitHub password!)

**How to create Personal Access Token:**
1. GitHub → Settings → Developer settings
2. Personal access tokens → Tokens (classic)
3. Generate new token
4. Select scope: `repo` (full control)
5. Copy token and use it as password

---

## ✅ Complete Commands (HTTPS - Recommended)

```powershell
# Make sure you're in project folder
cd E:\My_Projects\WebAPIDotNet

# Check if git is initialized
git status

# If not initialized:
git init
git add .
git commit -m "Initial commit: Web API with Authentication"

# Connect to GitHub (HTTPS)
git remote add origin https://github.com/Mu989595/WebAPI.git

# Push to GitHub
git branch -M main
git push -u origin main
```

---

## 🆘 Troubleshooting

### "Permission denied (publickey)"
- Use HTTPS instead of SSH (Option 2 above)
- Or set up SSH keys (more complex)

### "Authentication failed"
- Use Personal Access Token, not password
- Create token with `repo` scope

### "Repository not found"
- Check repository name is correct: `WebAPI`
- Make sure repository exists on GitHub
- Check you have access to it

### "Updates were rejected"
- If repository has files (README, .gitignore), you need to pull first:
  ```powershell
  git pull origin main --allow-unrelated-histories
  git push -u origin main
  ```

---

## 📝 Your Repository Info

- **Username:** Mu989595
- **Repository:** WebAPI
- **SSH URL:** git@github.com:Mu989595/WebAPI.git
- **HTTPS URL:** https://github.com/Mu989595/WebAPI.git

---

## ✅ After Success

Your code will be available at:
```
https://github.com/Mu989595/WebAPI
```

You can share this URL with anyone!

---

**I recommend using HTTPS (Option 2) - it's easier for beginners! 🚀**


