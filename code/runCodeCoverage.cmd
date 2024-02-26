rem Blog del que sabe lo que hizo
rem https://gunnarpeipman.com/aspnet-core-code-coverage/#targetText=Getting%20code%20coverage%20data&targetText=Cobertura%20is%20popular%20code%20coverage,xUnit%20formats%20to%20UnitTests%20folder.
rem https://danielpalme.github.io/ReportGenerator/usage.html?ref=https://githubhelp.com
cd %0\..\

dotnet test  --collect:"XPlat Code Coverage"   --logger "trx;LogFileName=TestResults.trx" --results-directory  ./BuildReports/UnitTests  /p:CollectCoverage=true /p:IncludeTestAssembly=true   /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=opencover  
dotnet %USERPROFILE%\.nuget\packages\reportgenerator\5.0.3\tools\net6.0\ReportGenerator.dll "-reports:%cd%\api.system.persona.unit.test\BuildReports\Coverage\coverage.opencover.xml" "-targetdir:%cd%\BuildReports\Coverage" -reporttypes:Html;Cobertura  
start chrome "%cd%\BuildReports\Coverage\index.html"
 