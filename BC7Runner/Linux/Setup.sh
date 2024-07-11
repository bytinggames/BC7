# clone BytingLib
cd ../../..
git clone -b develop git@github.com:bytinggames/BytingLib.git
cd -

# install wine
sudo apt install wine64 p7zip-full

# setup wine for MonoGame (gets win dotnet and d3dcompiler for fx compilation)
wget -qO- https://raw.githubusercontent.com/bytinggames/MonoGame/byting-lib/Tools/MonoGame.Effect.Compiler/mgfxc_wine_setup.sh | bash

# install .net 6 sdk
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb        
sudo apt update
sudo apt install apt-transport-https
sudo apt install dotnet-sdk-6.0

# install t4
install t4 for executing *.tt files:
dotnet tool uninstall -g dotnet-t4
dotnet tool install -g dotnet-t4 --version 2.2.1

# install libassimp for fbx model importer 
sudo apt install libassimp-dev

# build & run
./BuildAndRun.sh
