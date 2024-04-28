#The first image to pull down from docker hub (https://hub.docker.com/_/microsoft-dotnet) that is used to start the build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#Sets the working directory for any RUN, CMD, ENTRYPOINT, COPY, and ADD instructions that follow it.
WORKDIR /app

#Copies the project file and adds it to the filesystem of the image at the path (working directory)
#The project file is used to determine what dependencies are needed to build the image
COPY *.csproj ./
#Downloads the dependencies that are specified in the project file
RUN dotnet restore

#Copies the rest of the files
COPY . ./
#Build and publish the application to the output directory (out)
RUN dotnet publish -c Release -o out

#Final stage to build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
#Specify the working directory
WORKDIR /app
#Copy the output from build 
COPY --from=build /app/out .
#Set the entry point (where the image kicks-off)
ENTRYPOINT ["dotnet","PlatformService.Api.dll"]