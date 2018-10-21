FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4444
EXPOSE 44306

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Web.Mvc.BillSales/Web.Mvc.BillSales.csproj Web.Mvc.BillSales/
RUN dotnet restore Web.Mvc.BillSales/Web.Mvc.BillSales.csproj
COPY . .
WORKDIR /src/Web.Mvc.BillSales
RUN dotnet build Web.Mvc.BillSales.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Web.Mvc.BillSales.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.Mvc.BillSales.dll"]
