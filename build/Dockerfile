#build front
FROM node:10.15 as front-builder
WORKDIR /front-app

RUN npm i -g @angular/cli

COPY front/links-shortener/package.json package.json
RUN npm i

COPY front/links-shortener .
RUN ng build --prod

#build back
FROM microsoft/dotnet:2.2-sdk as back-builder
WORKDIR /back-app

COPY LinksShortener .
RUN dotnet publish -c Release -o ../published LinksShortener/LinksShortener.csproj

#merge and run
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY --from=back-builder /back-app/published .
COPY --from=front-builder /front-app/dist/links-shortener wwwroot

ENTRYPOINT ["dotnet", "LinksShortener.dll"]
