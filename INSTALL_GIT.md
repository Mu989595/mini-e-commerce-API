# How to Install Git on Windows

## 🔧 The Error Means:
**'git' is not recognized** = Git is not installed or not in your system PATH.

---

## 📥 Step 1: Download Git

1. Go to: **https://git-scm.com/download/win**
2. Click on the download button
3. The download will start automatically (about 50MB)

---

## 🚀 Step 2: Install Git

1. **Run the installer** (git-installer.exe)
2. Click **"Next"** on the welcome screen
3. **Select installation location** (keep default: `C:\Program Files\Git`) → Click **"Next"**
4. **Select components** - Keep default selections → Click **"Next"**
5. **Choose default editor** - Select "Visual Studio Code" (or keep default) → Click **"Next"**
6. **Adjust PATH environment** - **IMPORTANT!** Select:
   - ✅ **"Git from the command line and also from 3rd-party software"**
   - Click **"Next"**
7. **HTTPS transport** - Keep default → Click **"Next"**
8. **Line ending conversions** - Keep default → Click **"Next"**
9. **Terminal emulator** - Keep default → Click **"Next"**
10. **Default behavior** - Keep default → Click **"Next"**
11. **Extra options** - Keep default → Click **"Install"**
12. Wait for installation to complete
13. Click **"Finish"**

---

## ✅ Step 3: Verify Installation

### Option A: Close and Reopen PowerShell
1. **Close** current PowerShell window
2. **Open new** PowerShell window
3. Type:
   ```powershell
   git --version
   ```
4. You should see: `git version 2.x.x`

### Option B: Restart Computer
If it still doesn't work, restart your computer.

---

## 🔍 Step 4: Test Git Commands

```powershell
# Check Git version
git --version

# Check Git configuration
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"

# Verify
git config --list
```

---

## 🎯 Step 5: Now You Can Use Git!

Go back to your project folder and try again:

```powershell
cd E:\My_Projects\WebAPIDotNet
git init
```

It should work now! ✅

---

## 🆘 Troubleshooting

### If it still doesn't work after installation:

#### Method 1: Check PATH
1. Search "Environment Variables" in Windows
2. Edit System Environment Variables
3. Click "Environment Variables"
4. Under "System variables", find "Path"
5. Check if `C:\Program Files\Git\cmd` is in the list
6. If not, add it
7. Restart PowerShell

#### Method 2: Use Git Bash
1. Search "Git Bash" in Windows Start Menu
2. Open Git Bash
3. Navigate to your project:
   ```bash
   cd /e/My_Projects/WebAPIDotNet
   git init
   ```

#### Method 3: Reinstall
1. Uninstall Git from Control Panel
2. Download and install again
3. Make sure to select "Git from the command line" option

---

## 📚 Quick Reference

After installation, you can use:
- **Git Bash** - Command line interface for Git
- **Git GUI** - Graphical interface (optional)
- **Git in PowerShell** - What we're using

---

## ✅ Verification Checklist

- [ ] Git downloaded from official website
- [ ] Installation completed successfully
- [ ] Selected "Git from the command line" option
- [ ] Closed and reopened PowerShell
- [ ] `git --version` command works
- [ ] Ready to use Git commands

---

**Once installed, you can proceed with the GitHub setup! 🚀**

