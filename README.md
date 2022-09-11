# Sectors
Backend: C#, .NET 6.0, ASP.NET Core <br />
Database: MSSQL <br />
Frontend: AngularTS 14.2.0 <br />

# Setup
## SectorsBackend Setup
- Open the SectorsBackend solution in the IDE of your choice and run the solution. <br />
- Database "ConnectionString" can be found in appsettings.json. By default it will create database to your local "mssqllocaldb". <br />

## SectorsFrontend Setup
If you do not have node.js and Angular installed locally: <br />
1) Download node.js: https://nodejs.org/en/#home-downloadhead <br />
2) Install Angular CLI:  Open cmd and write "npm install -g @angular/cli" <br />

- When nodeJS and Angular have been installed, then open the SectorsFrontend folder in the IDE of your choice. <br />
- Open terminal within the folder and type "npm install". <br />
- Confirm that the proxy.conf.json "target" URL is the same as your SectorsBackend URL. If not, then edit it to be the same. <br />
- Run the project writing "npm start" in the terminal. <br />
- Open http://localhost:4200/ from browser. <br />
- If your NPM start does not listen to http://localhost:4200/, then edit the baseUrl in src/app/services/sector.service to be the same. <br />

# Database Diagram
![SectorsDB diagram](https://user-images.githubusercontent.com/60730038/189535392-47c2e42e-5e05-4cc9-9439-a52fc01881f7.png)

## Database Dump
Database will be automatically created running SectorsBackend solution, but if you want to create it manually: <br />
1) Download and open Microsoft SQL Server Management Studio <br />
2) Connect to MSSQLLocalDB and right-click on Databases folder. Choose "Import Data-tier Application" <br />
3) Add SectorsDB.bacpac file from there and you are set <br />
