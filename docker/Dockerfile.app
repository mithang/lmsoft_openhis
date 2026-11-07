# OpenHIS app image (Linux).
#
# The Dockerfiles already in this repo (HIS.SSO, AuthenticationCenter,
# WebAPI.Manage) are Windows-based (aspnet:6.0-windowsservercore-ltsc2019)
# and CANNOT run on Linux Docker. This is the Linux replacement: one
# shared Dockerfile for all 23 apps, parameterised by build args.
#
# Build args:
#   PROJECT - .csproj path relative to the repo root (the build context)
#   DLL     - assembly file name produced by publish (e.g. HIS.SSO.dll)
#   PORT    - port the app listens on (only used for EXPOSE)
#   CONFIG  - Release (default) or Debug
#
# NOTE on UseUrls: many Program.cs hardcode UseUrls("http://127.0.0.1:PORT").
# ASPNETCORE_URLS cannot override UseUrls, and 127.0.0.1 inside a container
# is not reachable from the host. The build stage rewrites 127.0.0.1 -> *
# in the STAGE copy of the app's Program.cs only (the host repo is never
# touched) so the app binds all interfaces inside the container.
# Sett.API already reads its URL from env (ASPNETCORE_URLS) with a
# 127.0.0.1:7002 fallback; the same rewrite applies to its fallback.
#
# NOTE on publish: the build needs network access the first time (NuGet
# restore + SDK packages). No local bin/obj is used; .dockerignore keeps
# build outputs and staging copies out of the context.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PROJECT
ARG DLL
ARG PORT
ARG CONFIG=Release
WORKDIR /src
COPY . .
RUN set -e; \
    PROJDIR="$(dirname "${PROJECT}")"; \
    find "${PROJDIR}" -maxdepth 1 -name 'Program.cs' \
      -exec sed -i 's#http://127\.0\.0\.1:#http://*:#g' {} + ; \
    dotnet publish "${PROJECT}" -c "${CONFIG}" -o /out --nologo -v q

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
ARG DLL
ARG PORT
WORKDIR /app
COPY --from=build /out .
# curl is only for the compose healthcheck (any HTTP response = up).
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
ENV APP_DLL=${DLL} \
    ASPNETCORE_ENVIRONMENT=Development \
    DOTNET_SYSTEM_NET_DISABLEIPV6=true
EXPOSE ${PORT}
ENTRYPOINT ["sh", "-c", "exec dotnet \"$APP_DLL\""]
