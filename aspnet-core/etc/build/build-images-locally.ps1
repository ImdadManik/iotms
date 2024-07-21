param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"
$company = "fandaqah"

Write-Host "********* BUILDING DbMigrator *********" -ForegroundColor Yellow
$dbMigratorFolder = Join-Path $slnFolder "src/iotms.DbMigrator"
Set-Location $dbMigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t $company/iotms-db-migrator:$version .
docker tag  $company/iotms-db-migrator imdadullah/iotms-db-migrator:latest
docker push imdadullah/iotms-db-migrator:latest

Write-Host "********* BUILDING Angular Application *********" -ForegroundColor Yellow
$angularAppFolder = Join-Path $slnFolder "../angular"
Set-Location $angularAppFolder
yarn
npm run build:prod
docker build -f Dockerfile.local -t $company/iotms-angular:$version .
docker tag $company/iotms-angular imdadullah/iotms-angular:latest 
docker push imdadullah/iotms-angular:latest

Write-Host "********* BUILDING Api.Host Application *********" -ForegroundColor Yellow
$hostFolder = Join-Path $slnFolder "src/iotms.HttpApi.Host"
Set-Location $hostFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t $company/iotms-api:$version .
docker tag $company/iotms-api imdadullah/iotms-api:latest
docker push imdadullah/iotms-api:latest


Write-Host "********* BUILDING Auth Server Application *********" -ForegroundColor Yellow
$authServerAppFolder = Join-Path $slnFolder "src/iotms.AuthServer"
Set-Location $authServerAppFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t $company/iotms-authserver:$version .
docker tag $company/iotms-authserver imdadullah/iotms-authserver:latest
docker push imdadullah/iotms-authserver:latest

### ALL COMPLETED
Write-Host "COMPLETED" -ForegroundColor Yellow
Set-Location $currentFolder