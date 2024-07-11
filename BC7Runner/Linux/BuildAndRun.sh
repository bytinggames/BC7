# builds and runs the game
cd "$(dirname "$0")"
cd ..
gameName=${PWD##*/} 
dotnet build -graphBuild:True
cd ./bin/Debug/net6.0
./$gameName
