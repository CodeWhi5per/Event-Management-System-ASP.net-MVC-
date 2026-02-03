Write-Host "=== Event Management System - Build & Run Test ===" -ForegroundColor Cyan
Write-Host ""

Set-Location "C:\Users\DanushkaNan\Downloads\EventManagementSystem"

Write-Host "Step 1: Cleaning previous build..." -ForegroundColor Yellow
dotnet clean | Out-Null

Write-Host "Step 2: Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore | Out-Null

Write-Host "Step 3: Building project..." -ForegroundColor Yellow
$buildOutput = dotnet build 2>&1 | Out-String

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ BUILD SUCCESSFUL!" -ForegroundColor Green
    Write-Host ""
    Write-Host "The project built successfully with no errors!" -ForegroundColor Green
    Write-Host ""
    Write-Host "To run the application:" -ForegroundColor Cyan
    Write-Host "  1. Open a terminal" -ForegroundColor White
    Write-Host "  2. Navigate to: C:\Users\DanushkaNan\Downloads\EventManagementSystem" -ForegroundColor White
    Write-Host "  3. Run: dotnet run" -ForegroundColor White
    Write-Host "  4. Open browser to: https://localhost:5001" -ForegroundColor White
    Write-Host ""
    Write-Host "Test Login Credentials:" -ForegroundColor Cyan
    Write-Host "  Email: john.doe@example.com" -ForegroundColor White
    Write-Host "  Password: password123" -ForegroundColor White
    Write-Host ""
} else {
    Write-Host "`n❌ BUILD FAILED!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Build Output:" -ForegroundColor Yellow
    Write-Host $buildOutput
}

Write-Host ""
Read-Host "Press Enter to exit"
