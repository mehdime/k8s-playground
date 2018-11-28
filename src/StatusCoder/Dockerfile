FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers

# Copying the csproj file only and restoring nuget packages from the csproj file only,
# enables future docker builds where the csproj file hasn't been modified 
# to use the saved image snapshot containing the already restored nuget package
# - hence allowing skipping the lengthy nuget restore operation when nuget 
# references haven't been changed since the last build. 
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
# Note how the built files are copied into another docker 
# image - allowing the runtime Docker image to be left with
# the source code.
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .

# The port our HTTP API is hardocded to listen on
EXPOSE 5100
ENTRYPOINT ["dotnet", "StatusCoder.dll"]

