dotnet build 
dotnet pack -p:PackageID=datadict
dotnet tool install --global datadict2 --add-source ./nupkg
