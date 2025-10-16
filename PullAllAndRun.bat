@echo off

pushd .

call PullAll.bat

dotnet build .\BC7Runner\ -graphBuild:True

cd .\BC7Runner\bin\Debug\net8.0
".\BC7Runner.exe"

popd