# used to generate the ../Content/*.Generated.* files
cd "$(dirname "$0")"
rm -rf ../../../../BytingLib/BuildTemplates/bin/
../GenerateContent.sh
