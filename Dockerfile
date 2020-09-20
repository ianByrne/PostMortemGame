FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

RUN apt-get update && apt-get install -y cron procps

WORKDIR /app

COPY ./application ./
COPY entrypoint.sh entrypoint.sh

RUN chmod +x ChatScript/BINARIES/LinuxChatScript64
RUN chmod +x entrypoint.sh

# Declare volumes for mount point directories
VOLUME ["ChatScript/USERS/", "ChatScript/LOGS/"]

EXPOSE 80

ENTRYPOINT ./entrypoint.sh