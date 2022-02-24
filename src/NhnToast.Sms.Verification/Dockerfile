FROM mcr.microsoft.com/azure-functions/dotnet:4

LABEL org.aliencube.nhn-toast-notification-service-custom-connector.author="Justin Yoo"
LABEL org.aliencube.nhn-toast-notification-service-custom-connector.profile="https://twitter.com/justinchronicle"
LABEL org.aliencube.nhn-toast-notification-service-custom-connector.projectUrl="https://github.com/aliencube/nhn-toast-notification-service-custom-connector"
LABEL org.aliencube.nhn-toast-notification-service-custom-connector.repositoryUrl="https://github.com/aliencube/nhn-toast-notification-service-custom-connector"
LABEL org.aliencube.nhn-toast-notification-service-custom-connector.license="https://raw.githubusercontent.com/aliencube/nhn-toast-notification-service-custom-connector/main/LICENSE"
LABEL description="This API works as a proxy to NHN Toast Notification Services - SMS API.\n\n \
For more details to the API, please visit the [NHN Toast document page](https://docs.toast.com/ko/Notification/SMS/ko/api-guide/)."

RUN mkdir -p /home/site/wwwroot

COPY ./published /home/site/wwwroot
