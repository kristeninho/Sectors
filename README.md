# Sectors
Backend: C#, .NET 6.0, ASP.NET Core <br />
Testing: Xunit, Moq, EF Core InMemoryDatabase <br />
Database: MSSQL <br />
Frontend: AngularTS 14.2.0 <br />

# Setup (without Docker)
## SectorsBackend Setup
- Open the SectorsBackend solution in Visual Studio and run the project. <br />
- Database "ConnectionString" can be found in appsettings.json. By default it will create database to your local "mssqllocaldb" with name "SectorsDB". <br />

## SectorsFrontend Setup
If you do not have node.js and Angular installed locally: <br />
1) Download node.js: https://nodejs.org/en/#home-downloadhead <br />
2) Install Angular CLI:  Open cmd and write "npm install -g @angular/cli" <br />

- When nodeJS and Angular have been installed, then open the SectorsFrontend folder in Visual Studio Code. <br />
- Open terminal within the folder and type "npm install". <br />
- Confirm that the proxy.conf.json "target" URL is the same as your SectorsBackend URL. If not, then edit it to be the same. <br />
- Make sure that backend is running!
- Run the project writing "npm start" in the terminal. (It is important to use this command, because it is configured to use proxy) <br />
- If your Angular does not listen to http://localhost:4200/, then edit the baseUrl in src/app/services/sector.service to be the same URL Angular is listening to. <br />
- Open http://localhost:4200/ from browser and done <br />


# Database Diagram
![SectorsDB diagram](https://user-images.githubusercontent.com/60730038/189535392-47c2e42e-5e05-4cc9-9439-a52fc01881f7.png)

## Database Dump
Database will be automatically created running SectorsBackend solution, but if you want to create it manually: <br />
1) Download and open Microsoft SQL Server Management Studio <br />
2) Connect to MSSQLLocalDB and right-click on Databases folder. Choose "Import Data-tier Application" <br />
3) Add SectorsDB.bacpac file from there and you are set <br />



# Docker Setup

- Make sure you have docker installed to your machine <br />
- Run "docker compose up" on the root root folder where docker-compose.yml is <br />
- Open http://localhost:4200/ from browser and done <br />
