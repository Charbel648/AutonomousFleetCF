
AutonomousFleet is a .NET 8 backend platform built using a microservices architecture.
The system manages autonomous delivery vehicles and customer orders while applying Clean Architecture, Domain-Driven Design, CQRS, Entity Framework Core Code-First, Docker infrastructure, and multi-tenant isolation.

This project currently includes two isolated microservices:

Fleet.API
Order.API

Each microservice is separated into architectural layers:

API
Application
Domain
Infrastructure
Persistance


AutonomousFleet
│   AutonomousFleet.sln
│   docker-compose.infra.yml
│   README.md
│
├── Fleet.API
├── Fleet.Application
├── Fleet.Domain
├── Fleet.Infrastructure
├── Fleet.Persistance
│
├── Order.API
├── Order.Application
├── Order.Domain
├── Order.Infrastructure
└── Order.Persistance



In Order.Domain/Entities/Order.cs:


The Order aggregate manages the order lifecycle using encapsulated state transition methods.

Supported states:

Created
Queued
Assigned
Running
Completed
Failed
Cancelled

Main behavior:

Create order
Queue order
Assign vehicle
Start order
Complete order
Fail order
Cancel order


In Fleet.Domain/Entities/Vehicle.cs:


The Vehicle aggregate manages vehicle state, battery information, and coordinate telemetry.

Supported states:

Available
Assigned
Running
Charging
Maintenance
Offline

Main behavior:

Register vehicle
Update telemetry
Modify state
Assign vehicle
Mark vehicle offline


Base routes:

POST /orders
GET /orders
GET /orders/{id}


Base routes:

POST /vehicles
GET /vehicles
PUT /vehicles/{id}/state

All requests must include:

X-Tenant-Id: tenant-a

The tenant header is required because the system uses multi-tenant isolation.


The JWT Bearer Authentication is a token to authenticate and give access to selected staff like admins, it does not allow anyone without the token to change or access data.
To get the token you need to fill the header with an id, a password and a tenantId and then you input the token to authorize access.



The database schema was updated without deleting or resetting the existing database tables.

The goal was to evolve the system by adding new fields to the existing domain entities and then generating a new Entity Framework Core migration.

TelemetryData stores extra telemetry information about the vehicle, such as speed, efficiency, sensor data, or real-time profiling values.

PriorityScore allows the system to give an order a priority level.


We used the following commands for migration:

dotnet ef migrations add AddTelemetryAndPriorityFields --project .\Order.Persistance\Order.Persistance.csproj --startup-project .\Order.API\Order.API.csproj
dotnet ef migrations add AddTelemetryAndPriorityFields --project .\Fleet.Persistance\Fleet.Persistance.csproj --startup-project .\Fleet.API\Fleet.API.csproj



