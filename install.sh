dotnet build 
dotnet pack -p:PackageID=datadict
dotnet tool install --global datadict --add-source ./nupkg
