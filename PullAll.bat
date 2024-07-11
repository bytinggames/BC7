REM @echo off

pushd .

git pull
cd ..
if exist BytingLib\ (
	cd BytingLib
	git pull
) else (
	git clone https://github.com/bytinggames/BytingLib.git
)

popd
