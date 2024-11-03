FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build-env
ARG TARGETARCH
WORKDIR /app

# https://github.com/moby/moby/issues/15858#issuecomment-550315619
RUN dotnet tool install -g Notino.dotnet-references --version 1.2.0 --add-source https://nuget.notino.com/nuget
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ./*.sln ./
COPY ./NuGet.Config ./
COPY ./global.json ./
COPY ./Directory.Build.props ./

# This results in a layer for each step, but it is what it is...
COPY ./*/*/*.csproj ./

# https://github.com/benmccallum/dotnet-references/blob/master/README.md
RUN dotnet-references fix -ep ./*.sln -wd . -rupf

# Copy sources and build
COPY ./src ./src/

RUN dotnet run --project ./src/MaterialPurchase -- codegen write
RUN dotnet run --project ./src/MaterialPurchase -- codegen test

## restore dependencies
RUN dotnet restore ./src/MaterialPurchase/ -a $TARGETARCH

# build
RUN dotnet publish ./src/MaterialPurchase/ -c Release --no-restore -o out -a $TARGETARCH --self-contained false /p:PublishSingleFile=true

# Create runtime image
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled-extra
WORKDIR /app

ENV TZ Europe/Prague
# required to enable read only root filesystem
ENV COMPlus_EnableDiagnostics=0

USER $APP_UID

COPY --from=build-env /app/out .
ENTRYPOINT ["./MaterialPurchase"]
