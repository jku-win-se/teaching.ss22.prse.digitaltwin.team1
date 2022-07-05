dotnet test SmartRoom.sln /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:ExcludeByFile="**/*migrations/*.cs" /p:ExcludeByFile="**/program.cs"
reportgenerator -reports:coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html
start "" "CoverageReport/index.html"