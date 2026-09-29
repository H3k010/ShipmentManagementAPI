# Shipment Management API

Shipment Management is a REST API for handling shipments from creation through delivery. It provides the core operations needed to manage packages, record their movement, and keep track of the facilities and delivery attempts involved along the way.

The API is versioned, allowing changes to be introduced in newer API versions while keeping existing clients supported. Authentication and authorization are handled through JWT bearer tokens, with access to operations depending on the user's role.

## Architecture

The project follows a Clean Architecture approach, separating the core application logic from infrastructure and HTTP concerns. The domain contains the core entities and rules, the application layer handles business operations and DTOs, the infrastructure layer handles persistence, and the API layer exposes everything through HTTP endpoints.

This keeps the different parts of the application independent and makes the business logic easier to maintain as the project grows.

## Packages and Tracking

Packages represent the shipments being managed by the system. A package contains information about the shipment, sender, recipient, addresses, delivery type, and its current status.

Tracking events are recorded as a package moves through its delivery process. Status transitions are validated before they are applied, and delivery attempts are recorded when applicable. Public tracking endpoints also allow shipment progress and tracking history to be retrieved using a package's tracking number.

### Shipments Tracking

These endpoints are intended for tracking shipments using a tracking number, without requiring access to a user's package management features.
```
GET /api/v1/Tracking/{trackingNumber}
GET /api/v1/Tracking/{trackingNumber}/events
```
The first endpoint returns public package information, while the second returns the package's tracking history.

### User Endpoints

Authenticated users can create and manage their own packages, retrieve package information, and view tracking events.
```
GET   /api/v1/Packages
GET   /api/v1/Packages/{id}
POST  /api/v1/Packages/create
GET   /api/v1/Packages/{id}/tracking
PATCH /api/v1/Packages/{id}/cancel
```
V2 provides the updated package representation and additional package operations.
```
GET  /api/v2/Packages
POST /api/v2/Packages/create
GET  /api/v2/Packages/{id}
GET  /api/v2/Packages/{id}/tracking
```
The package creation endpoints return the newly created package, while cancellation and tracking operations return the appropriate status or tracking information.

Authentication endpoints are available for registration, login, token refresh, and logout.
```
POST /api/v1/Auth/register
POST /api/v1/Auth/login
POST /api/v1/Auth/refresh
POST /api/v1/Auth/logout
```
Login returns a JWT token that can be used when accessing protected endpoints.

### Administrative Endpoints

Administrative endpoints provide broader management capabilities for packages and facilities.
```
GET   /api/v1/admin/packages
GET   /api/v1/admin/packages/{id}
GET   /api/v1/admin/packages/{id}/events
POST  /api/v1/admin/packages/{id}/events
GET   /api/v1/admin/packages/{id}/delivery-attempts
```
Facilities can also be created, updated, retrieved, activated, and deactivated.
```
GET   /api/v1/admin/Facilities
GET   /api/v1/admin/Facilities/{id}
POST  /api/v1/admin/Facilities/add
PATCH /api/v1/admin/Facilities/{id}
PATCH /api/v1/admin/Facilities/{id}/activate
PATCH /api/v1/admin/Facilities/{id}/deactivate
```
V2 includes the corresponding administrative package endpoints, including tracking events and delivery attempts.
```
POST /api/v2/admin/packages/{id}/events
GET  /api/v2/admin/packages/{id}/delivery-attempts
```

## Validation

Request validation is handled with FluentValidation. Validation rules are kept outside the controllers and cover both request properties and nested objects such as package addresses.

This keeps invalid requests from reaching the application logic while allowing the validation rules to remain organized and reusable.

## Database

The API uses MySQL for persistent data storage, with Entity Framework Core handling database access and migrations. Repository and infrastructure components manage communication with the database while keeping persistence details separate from the application's business logic.

## Logging

Serilog is used for application logging, with logs written to the console and stored in MySQL. Logging is used to record important application events such as package operations, status changes, validation failures, missing resources, and unexpected exceptions.

## API Documentation

The API provides OpenAPI documentation and Swagger UI with separate documentation for V1 and V2. Swagger UI is intended only for development and testing. It provides an interactive view of the available V1 and V2 endpoints, their request and response models, authentication requirements, and possible responses.

The project also generates XML documentation from the API's controller and action comments, which is used to improve the generated API documentation.
