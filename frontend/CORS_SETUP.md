# CORS Setup Instructions

## API Configuration

CORS has been configured in `Program.cs` to allow requests from:
- `http://localhost:8000` (Python http.server default)
- `http://localhost:3000` (Common React dev server)
- `http://127.0.0.1:8000`
- `file://` (for opening HTML files directly)

## How to Run

### 1. Start the API:
```bash
cd "E:\My_Projects\Mini E-Commerce API"
dotnet run
```

The API will run on:
- HTTP: `http://localhost:5045`
- HTTPS: `https://localhost:7181`

### 2. Start the Frontend:

**Option A: Using Python (Recommended)**
```bash
cd frontend
python -m http.server 8000
```
Then open: `http://localhost:8000`

**Option B: Using Node.js http-server**
```bash
cd frontend
npx http-server -p 8000
```

**Option C: Open directly (may have CORS issues)**
- Just double-click `index.html`
- Note: This might not work due to browser security restrictions

### 3. Update API URL if needed:

If your API runs on a different port, update `frontend/js/api.js`:
```javascript
const API_BASE_URL = 'http://localhost:YOUR_PORT/api';
```

## Troubleshooting

### If you still get "Failed to fetch":

1. **Check if API is running:**
   - Open `http://localhost:5045/api/Product` in browser
   - You should see a response (or 401 if not authenticated)

2. **Check the port:**
   - Look at the console output when running `dotnet run`
   - Update `frontend/js/api.js` with the correct port

3. **Use HTTP server:**
   - Don't open HTML files directly (file://)
   - Always use a local server (python -m http.server)

4. **Check CORS:**
   - Make sure `app.UseCors("AllowFrontend");` is in Program.cs
   - It should be BEFORE `UseAuthentication()` and `UseAuthorization()`
