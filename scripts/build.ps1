nuget restore
dotnet publish -c "Release" -f "net8.0" -r "win-x86" -o "artifacts/win-x86"
dotnet publish -c "Release" -f "net8.0" -r "win-x64" -o "artifacts/win-x64"
Compress-Archive -Path "artifacts/win-x86" -DestinationPath "artifacts/RapidFireSim-win-x86.zip" -Force
Compress-Archive -Path "artifacts/win-x64" -DestinationPath "artifacts/RapidFireSim-win-x64.zip" -Force
