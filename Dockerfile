FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build-env
ARG TARGETARCH
WORKDIR /app

ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ./*.sln ./
COPY ./NuGet.Config ./
COPY ./global.json ./
COPY ./Directory.Build.props ./
COPY ./*/*/*.csproj ./
COPY ./src ./src/

RUN dotnet run --project ./src/MaterialPurchase -- codegen write
RUN dotnet run --project ./src/MaterialPurchase -- codegen test

RUN dotnet restore ./src/MaterialPurchase/ -a $TARGETARCH

RUN dotnet publish ./src/MaterialPurchase/ -c Release --no-restore -o out -a $TARGETARCH --self-contained false /p:PublishSingleFile=true

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled-extra
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["./MaterialPurchase"]
