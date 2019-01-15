# LinksShortener
Simple links shortener

# Linux
1. git clone
2. sudo docker-compose up
3. localhost:34131

# Windows
1. git clone
2. cd LinksShortener
3. dotnet publish -c Release -o ../published LinksShortener/LinksShortener.csproj
4. cd ../front/links-shortener
5. npm i -g @angular/cli
6. npm i
7. ng build --prod
8. mkdir ../../LinksShortener/published/wwwroot
9. cp -r dist/links-shortener/* ../../LinksShortener/wwwroot
10. setup mongo 3+
11. change mongo url in /LinksShortener/published/appsettings.json (relative from repo root)
12. dotnet /LinksShortener/published/LinksShortener.dll
13. localhost:34131
