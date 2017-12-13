dotnet restore
dotnet build
dotnet publish -o dockerout

docker build  -t vac/identity:0.1 -t gcr.io/sandbox-180712/vacidentity:0.26 .

gcloud docker -- push gcr.io/sandbox-180712/vacidentity:0.26