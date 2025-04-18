
FROM mcr.microsoft.com/dotnet/sdk:9.0 as build

ENV DOTNET_CLI_HOME=/tmp
ENV PATH="$PATH:/tmp/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT=Production

# Switch to root for package installs
USER 0

WORKDIR /src
COPY services/net/ffmpeg services/net/ffmpeg
COPY libs/net libs/net

RUN fix_permissions() { while [ $# -gt 0 ] ; do chgrp -R 0 "$1" && chmod -R g=u "$1"; shift; done } && \
    fix_permissions "/tmp"

WORKDIR /src/services/net/ffmpeg
RUN dotnet build -c $ASPNETCORE_ENVIRONMENT -o /build

FROM mcr.microsoft.com/dotnet/aspnet:9.0 as deploy

RUN apt-get update --fix-missing && apt-get -y upgrade && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends procps curl ffmpeg libc6-dev libgdiplus

WORKDIR /app
COPY --from=build /build .

# This volume is the local storage for uploaded files.
RUN mkdir /data
VOLUME /data

# Run container by default as user with id 1001 (default)
USER 1001

ENTRYPOINT dotnet TNO.Services.FFmpeg.dll
