# cd "$(dirname "$0")"
cd ../BytingLib
git pull
cd ..

cd ./BC7
git pull
cd ./BC7Runner/Linux/Troubleshoot/
./GenerateContentTroubleshoot.sh
cd ..
./BuildAndRun.sh
cd ../../
