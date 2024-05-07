# to-dos-api
Template of REST api service
## How to install template to your dotnet?
Run `dotnet new --install .` command in your terminal.
If you got an error, use `dotnet new --install . --force` command
## How to create project based on this template?
1. Create folder for your project
2. Run `dotnet new to-dos-api -n <replace-with-your-project-name>` command in this folder from your terminal
## How to run project based on this template?
1. Run `docker-compose up -d` command from folder with your project to run database
2. You can access this service by following this link: localhost:35000 or run it in IDE and follow localhost:36000 link
> **_NOTE:_**  You can go to /swagger/index.html page to see available endpoints and test it.

## Ports
- localhost:36000 - IDE
- localhost:35000 - Docker
- 6432 - postgres port