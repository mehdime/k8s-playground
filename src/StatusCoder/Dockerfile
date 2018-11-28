# This was taken straight from https://docs.docker.com/engine/examples/dotnetcore/

FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers

#############
# Copying the csproj file only and restoring nuget packages from it (the csproj is 
# where nuget references are listed in .NET Core) enables future docker builds 
# where the csproj file hasn't been modified (which would be most builds since the 
# csproj no longer contain the list of .cs files in .NET Core - it only contains the 
# project's properties and nuget references) to use the saved image snapshot containing 
# the already restored nuget package. This allows skipping the lengthy nuget restore 
# operation when nuget references haven't been modified since the last build. 
#
# Try it for yourself:
# 1. Do a docker build. This will restore all the nuget packages and build the code.
# 2. Make a modifification to the C# source code. 
# 3. Do a docker build again. See in the build output how steps 1 to 4 (which includes the
# nuget package restore) are skipped as docker is able to re-use the saved image snapshot 
# from the previous build.
# 4. Now add or remove a nuget reference to the project to cause the csproj file to be modified.
# 5. Do another docker build. See how docker detects the csproj file modification and now does
# a full build, including the nuget restore step.
#############
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
# Note how the built files are copied into another docker 
# image - allowing the runtime Docker image to be left without
# any of the source code.
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

# The port our HTTP API is hardocded to listen on
EXPOSE 5100
ENTRYPOINT ["dotnet", "StatusCoder.dll"]

