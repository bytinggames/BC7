# builds and runs the game
cd "$(dirname "$0")"
cd ..
gameName=${PWD##*/}
MGFXC_WINE_PATH=~/.winemonogame dotnet build -graphBuild:True
