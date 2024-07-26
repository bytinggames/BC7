# clone BytingLib
git submodule init
git submodule update

# install wine
sudo apt install wine64 p7zip-full

# setup wine for MonoGame (gets win dotnet and d3dcompiler for fx compilation)
wget -qO- https://raw.githubusercontent.com/bytinggames/MonoGame/byting-lib/Tools/MonoGame.Effect.Compiler/mgfxc_wine_setup.sh | bash

# install .net 8 sdk
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0

# install t4
install t4 for executing *.tt files:
dotnet tool uninstall -g dotnet-t4
dotnet tool install -g dotnet-t4 --version 2.2.1

# install libassimp for fbx model importer 
sudo apt install libassimp-dev

# build & run
./BuildAndRun.sh
