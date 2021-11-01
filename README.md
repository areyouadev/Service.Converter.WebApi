## Service.Converter.WebApi
### C# .Net Core Web API application for restful services. aYo - .NET Technical Test

User registration API's with C# .Net Core, PostgreSQL, authentication by JWT and Swagger to document them, also with authentication control to test them.as.

Running in Docker container through Docker-Compose.

## Database with PostgreSQL
* Uses the official PostgreSQL Docker image for database publishing
* It is necessary to create a local folder /PostgreSQL/dbto store the physical database files

## Backend with C# .Net Core
* Use the package Swashbuckle.AspNetCore@5.6.4 for API documentation via Swagger with possibility to test them
* Uses the package Microsoft.EntityFrameworkCore.Tools@5.0.10 and Npgsql.EntityFrameworkCore.PostgreSQL@5.0.10 to connect to the PostgreSQL database through the Entity Framework Coretity Framework Core
* Uses the package Microsoft.AspNetCore.Authentication@5.0.10 and Microsoft.AspNetCore.Authentication.JwtBearer@5.0.10 for authentication and authorization via JWT (JSON Web Token)Token)
* Uses the BCrypt.Net-Core@1.6.0user password encryption package
* Use the package FluentValidation@10.3.4 to validate the business rules of domain entities

## Run the application
* Docker installation is required.

## Docker-Compose
1. Run docker-compose up --force-recreatevia Finish or Docker Quickstart Terminal (for Windows 10 Home).
2. Navigate to http://localhost:5000orhttp://<container ip>:5000
3. After accessing the address, the application will open on the API's documentation page
