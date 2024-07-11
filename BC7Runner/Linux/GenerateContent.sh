# used to generate the ../Content/*.Generated.* files
cd "$(dirname "$0")"
dotnet t4 ../_GenerateContentFiles.tt
