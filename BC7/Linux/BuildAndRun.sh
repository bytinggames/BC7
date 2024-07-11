# builds and runs the game
cd "$(dirname "$0")"
cd ..
gameName=${PWD##*/} 
dotnet build -graphBuild:True
cd ./bin/Debug/net8.0
./$gameName
