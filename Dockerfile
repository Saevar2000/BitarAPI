FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# Copy everything and build app
COPY . .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "BitarAPI.dll"]