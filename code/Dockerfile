#https://hub.docker.com/_/microsoft-dotnet-aspnet
#7.0.402-alpine3.18-amd64, 7.0-alpine3.18-amd64, 7.0-alpine-amd64, 7.0.402-alpine3.18, 7.0-alpine3.18, 7.0-alpine	
#FROM mcr.microsoft.com/dotnet/aspnet:6.0.24-alpine3.18-amd64 AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bookworm-slim AS base
#mcr.microsoft.com/dotnet/aspnet:6.0 

 
ARG BUILD_CONFIGURATION=Release
ENV ASPNETCORE_ENVIRONMENT=Release
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80 
WORKDIR /app
EXPOSE 80
EXPOSE 443





# Install the agent
#RUN apk update
#RUN apk install -y wget ca-certificates gnupg 
RUN apt-get update 
RUN apt-get install -y wget ca-certificates gnupg  
RUN echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list  
RUN  wget https://download.newrelic.com/548C16BF.gpg  
RUN  apt-key add 548C16BF.gpg  
RUN  apt-get update  
RUN  apt-get install -y 'newrelic-dotnet-agent'  
RUN  rm -rf /var/lib/apt/lists/*

#RUN apt-get install -y libicu-dev && docker-php-ext-install intl



# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1  
ENV CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A}  
ENV CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent  
ENV CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so  
ENV NEW_RELIC_LICENSE_KEY=722f48b03f039b321ddf25b14754dc8cFFFFNRAL  
ENV NEW_RELIC_APP_NAME="drx-cos-api-workflow"


#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
#RUN apk add --no-cache icu-libs
 
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

#--------------------------------------------------------------




#https://hub.docker.com/_/microsoft-dotnet-sdk
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1.402-buster AS build
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:6.0.416-alpine3.18-amd64 AS build 




#RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates





#WORKDIR /code

WORKDIR /

COPY ./ConnecticureOS.Workflow.sln   ./
RUN true
  
COPY ./ApiOS/ApiOS.csproj ./ApiOS/ApiOS.csproj
#RUN true

COPY ./Application/Application.csproj ./Application/Application.csproj
RUN true

COPY ./Domain/Domain.csproj ./Domain/Domain.csproj
RUN true

COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/Infrastructure.csproj
RUN true
 

COPY ./drx.cos.workflow.rulemaneger/drx.cos.workflow.rulemanager.csproj ./drx.cos.workflow.rulemaneger/drx.cos.workflow.rulemanager.csproj
RUN true


COPY ./RulesEngine/RulesEngine.csproj ./RulesEngine/RulesEngine.csproj
RUN true
 
 
COPY ./nuget.config ./nuget.config
RUN true

#RUN dotnet nuget sources update -ValidAuthenticationTypes basic -Name ConnectureOSDotNetFundation@Release -UserName "ConnectureOSDotNetFundation@Release" -Password 25dybm5pvxam5qt4sqxggo63hdndygagv7helf63dspott6p4sua
RUN dotnet restore --configfile nuget.config

 
#RUN dotnet restore
COPY . .
RUN true

#build
 
RUN dotnet build "./ApiOS/ApiOS.csproj" -c Docker -o /app


#publish
FROM build AS publish
RUN dotnet publish "./ApiOS/ApiOS.csproj" -c Docker -o /app


#App image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "drx-cos-api-workflow.dll"]





