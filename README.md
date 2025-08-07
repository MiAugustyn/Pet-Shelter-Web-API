# Pet Shelter Web API

Author: Michał Augustyn

#### Requirements
- .NET 6 SDK or later
- SQL Server (2017+)

## Overwiew
Pet Shelter Web API is a RESTful service built with ASP.NET Core and Entity Framework Core to manage all aspects of an animal shelter. Project utilizes the Entity Framework and AutoMapper packages

### Models & Relations
The API defines seven entities—Breed, Note, Owner, Pet, Shelter, Specie, and Worker—and configures one-to-one, one-to-many, and many-to-many relationships through a join table to maintain data integrity without redundancy. \
<sub>Model Files Overview:</sub> \
<img width="218" height="240" alt="image" src="https://github.com/user-attachments/assets/f3db9507-7046-4e40-a09c-5aa4f989b5ce" />

### CRUD
Full CRUD operations are provided for each entity, including endpoints to retrieve all records, fetch by ID or name, create new entries, update existing ones, and delete. Additional GET endpoints enable queries by foreign keys, for example listing all pets belonging to a specific owner. \
<sub>Swagger API Interface:</sub> \
<img width="1821" height="907" alt="image" src="https://github.com/user-attachments/assets/72232b32-0fee-409d-a7ca-91fa29da8e47" />

### DTOs & Mapping
Data Transfer Objects map incoming and outgoing data to internal models, preserving stability of the API contract when the database schema changes. 

### Dependency Injection
Services and repositories are registered via the built-in dependency injection container to allow easy swapping of implementations and to support unit testing.

### Controllers & Validation
Controller actions apply validation rules using data annotations and custom validators. Every endpoint returns the correct HTTP status code to clearly communicate results.

### Database Seeding
A database seeding mechanism populates the database with sample data when the command `dotnet -run seeddata` is executed, enabling immediate testing against a realistic dataset.

## Configuration & Deployment

#### Configuration
- Specify the database connection string in the *appsettings.json* file under the **ConnectionStrings** section by setting the **DefaultConnection** key. Refer to the sample connection string, provided as an example structure for local development. \
<img width="1568" height="238" alt="image" src="https://github.com/user-attachments/assets/715f8198-0860-4c09-9c3d-9de05f5066d7" />

#### Deployment
- In terminal or command prompt navigate to project root folder
- Confirm *appsettings.json* contains correct connection string
- Run database migrations: `dotnet ef database update`\
  <sub>Database tables should be structured as follows:</sub> \
  <img width="226" height="277" alt="image" src="https://github.com/user-attachments/assets/0a56bc04-9373-43d5-9b69-264c19381620" />
- **(Optional)** Seed database with sample data: `dotnet run seeddata` \
  <sub>Every data table will be filled with sample records with relations:</sub> \
  <img width="442" height="330" alt="image" src="https://github.com/user-attachments/assets/f04a4589-9a70-4343-b03f-6e9635b06cfc" />

- Build and run application (locally): `dotnet run`
