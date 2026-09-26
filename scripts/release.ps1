# Create release package
$projectDir = Split-Path -Parent $PSScriptRoot
$binDir = "$projectDir\TV Rename Missing XML Parser\bin\Release"
$zipPath = "$binDir\tv-rename-missing-xml-tool.zip"

# Remove old zip if it exists
if (Test-Path $zipPath) {
    Remove-Item $zipPath -Force
}

# Create temp folder for packaging
$tempDir = "$binDir\temp"
if (Test-Path $tempDir) {
    Remove-Item $tempDir -Recurse -Force
}
New-Item -ItemType Directory -Path $tempDir -Force | Out-Null

# Copy files
Copy-Item "$binDir\*.exe" $tempDir
Copy-Item "$binDir\*.dll" $tempDir

# Create Data folder and copy default settings
New-Item -ItemType Directory -Path "$tempDir\Data" -Force | Out-Null
Copy-Item "$projectDir\TV Rename Missing XML Parser\Data\settings.default.json" "$tempDir\Data\"

# Create zip
Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($tempDir, $zipPath)

# Cleanup temp folder
Remove-Item $tempDir -Recurse -Force

Write-Host "Release package created: $zipPath"
