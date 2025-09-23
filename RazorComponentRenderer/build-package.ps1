#!/usr/bin/env pwsh
param(
    [string]$Configuration = "Release"
)

Write-Host "🔨 Building RazorComponentRenderer NuGet Package..." -ForegroundColor Green

try {
    # Clean previous builds
    Write-Host "🧹 Cleaning previous builds..." -ForegroundColor Yellow
    dotnet clean --configuration $Configuration
    Remove-Item -Path "./bin" -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -Path "../RazorComponentRenderer.Generators/bin" -Recurse -Force -ErrorAction SilentlyContinue

    # Build generator first
    Write-Host "🔧 Building..." -ForegroundColor Yellow
    dotnet build --configuration $Configuration --verbosity minimal

    if ($LASTEXITCODE -ne 0) {
        throw "Generator build failed"
    }

    # Build and pack main project
    Write-Host "📦 Building and packing main project..." -ForegroundColor Yellow
    dotnet pack --configuration $Configuration --verbosity minimal

    if ($LASTEXITCODE -ne 0) {
        throw "Pack failed"
    }

    # Show package info
    $packagePath = Get-ChildItem -Path "./bin/$Configuration" -Filter "*.nupkg" | Select-Object -First 1
    if ($packagePath) {
        Write-Host "✅ Package created successfully!" -ForegroundColor Green
        Write-Host "📍 Location: $($packagePath.FullName)" -ForegroundColor Cyan
        Write-Host "📏 Size: $([math]::Round($packagePath.Length / 1KB, 2)) KB" -ForegroundColor Cyan
    }

} catch {
    Write-Host "❌ Build failed: $_" -ForegroundColor Red
    exit 1
}

Write-Host "🎉 Build completed successfully!" -ForegroundColor Green