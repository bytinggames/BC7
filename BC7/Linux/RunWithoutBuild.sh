# builds and runs the game
cd "$(dirname "$0")"
cd ..
gameName=${PWD##*/} 
cd ./bin/Debug/net8.0
./$gameName
