# ProductAmin.API

This is a .NET 8 solution for the project Product Administration.

## Description

This is a .NET 8 solution for the Product Administration project. It provides a basic API for managing products, including functionalities for creating, updating, and retrieving records.

## Project Structure

The project is structured following a hexagonal architecture in multiple layers, where each layer manages its own dependency injection to facilitate the change of implemented technologies. The architecture is domain-oriented (DDD), where domain entities are responsible for their own state changes and modifications.

For data access, the repository pattern is used. This approach abstracts data storage, making it possible to switch from a file storage system to a database without changing the application layer (business logic).

## Testing

For unit tests, we use "Mother" classes that build data for different mocks used. The unit tests cover the domain layer, application layer, and part of the infrastructure.

## Getting Started

### Prerequisites

- .NET 8 SDK

### Installation

1. Clone the repository.
2. Open the solution in Visual Studio or your preferred IDE.
3. Restore the Nuget packages.

```
    dotnet restore    
```

4. Build the solution using the following commands:

```
    dotnet clean
    dotnet build
```

### Usagee

To run the application, use the following command:

```
    dotnet run --project ./ProductAdmin.API/ProductAdmin.API.csproj -lp ProdcutAmin.API
```


## Contact

Provide contact information for any questions or inquiries.
