## About
This repository is a reservation system custom solution for FMFT - the annual cultural festival hosted in the city of Kraków.  

### Features
* Custom user registration system
    - external Facebook authentication
    - password based authentication
    - change and reset password options
    - user roles
* Performances management
    - basic information
    - gallery
    - auditorium assigment
* Reservations
    - seat based
    - user limited to one reservation per show
    - cancellation
    - QR codes
    - validity check page for admins
* Emails
    - SMTP client

### Tech Information
#### Stack
 * .NET 6
 * Blazor WebAssembly in `FMFT.Web.Client` project
 * ASP.NET Core in `FMFT.Web.Server` project
 * SQL Server Database in `FMFT.Database` project
 * Docker

#### External libraries
* Dapper 
* BCrypt.Net-Next
* BlazorPanzoom
* FacebookCore
* Markdig
* QRCoder
* RESTFulSense
* Xeption
* PrettyBlazor
* LitJWT
* Swagger
* Bootstrap 5
* Fontawesome

#### Other features
* Authentication is made with JWT without using any Microsoft authentication packages
* Emails are generated from razor views using the RazorViewToEmailTemplates package
* The Web API and Blazor WebAssembly projects are running as seperate services
* The Web API returns error codes, check them [here](/ERROR-CODES.md)