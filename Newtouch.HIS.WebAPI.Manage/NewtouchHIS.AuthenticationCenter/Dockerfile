# Xay dung image trong goi trien khai

FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2019
WORKDIR /app
EXPOSE 80

COPY . .
ENTRYPOINT ["dotnet", "NewtouchHIS.AuthenticationCenter.dll"]