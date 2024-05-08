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
## Running Karate-tests manually
To run Karate tests:
1. Download [karate.jar](https://github.com/karatelabs/karate/releases/download/v1.4.1/karate-1.4.1.jar) file and put it into Tests/E2E folder, also rename it to `karate.jar`
2. Open terminal in this folder
3. Run `java -jar karate.jar ToDoControllerEnd-to-EndTesting.feature`
## Ports
- localhost:36000 - IDE
- localhost:35000 - Docker
- 6432 - postgres port