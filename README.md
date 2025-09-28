﻿# Online Shop - Domain Driven Design Learning Project

Ein wachsendes Lernprojekt zur Implementierung von **Domain Driven Design (DDD)** und **Clean Architecture** mit .NET 8, entwickelt durch Event Storming und Vertical Slice Architecture.

## Projektübersicht

Dieses Projekt ist ein **kontinuierlich wachsendes Lernprojekt**, das einen vollständigen Online-Shop implementiert. Aktuell ist der **Product Catalog** Bounded Context implementiert, weitere Contexts folgen schrittweise.

### Geplante Bounded Contexts
- ✅ **Product Catalog** - Produktverwaltung (Aktuell implementiert)
- 🔄 **Shopping Basket** - Warenkorbfunktionalität (Geplant)
- 🔄 **Checkout Process** - Bestellabwicklung (Geplant)
- 🔄 **Payment** - Zahlungsabwicklung (Geplant)
- 🔄 **Fulfillment** - Versand und Lieferung (Geplant)

## Event Storming Basis

Das Projekt basiert auf einem Event Storming Workshop, bei dem die folgenden Domain Events für den Product Catalog identifiziert wurden:

- `ProductAdded` - Neues Produkt hinzugefügt
- `ProductDetailsUpdated` - Produktdetails aktualisiert (Geplant)
- `PriceChanged` - Preis geändert (Geplant)
- `StockUpdated` - Lagerbestand aktualisiert (Geplant)
- `ProductBecameUnavailable` - Produkt nicht mehr verfügbar (Geplant)
- `StockValidationRequested` - Lagerbestand-Validierung angefordert (Geplant)
- `StockValidationFailed` - Lagerbestand-Validierung fehlgeschlagen (Geplant)

## Architektur

### Clean Architecture + DDD
- **Domain Layer**: Entities, Value Objects, Domain Events, Repository Interfaces
- **Application Layer**: Use Cases, Commands, Queries, Handlers (CQRS)
- **Infrastructure Layer**: Database, Repository Implementations, External Services
- **API Layer**: HTTP Endpoints, Dependency Injection Setup

### Vertical Slice Architecture
Jedes Feature ist als eigenständiger "Slice" implementiert:
```
Features/AddProduct/
├── AddProductCommand.cs
├── AddProductCommandHandler.cs
├── AddProductCommandValidator.cs
└── AddProductEndpoint.cs
```

## Technologie-Stack

### Backend (.NET 8)
- **Framework**: ASP.NET Core 8 Web API
- **Architecture**: Clean Architecture + DDD + Vertical Slice
- **CQRS**: MediatR für Command/Query Separation
- **Validation**: FluentValidation für Input-Validierung
- **ORM**: Entity Framework Core mit SQLite
- **API Documentation**: Swagger/OpenAPI

### Development Tools
- **Code Quality**: StyleCop für Code-Standards
- **CI/CD**: GitHub Actions
- **Code Analysis**: SonarCloud Integration (Geplant)
- **Testing**: xUnit, FluentAssertions, Moq

### Future Integrations
- **Service Discovery**: .NET Aspire (Geplant)
- **Event Bus**: RabbitMQ für Domain Events (Geplant)
- **Caching**: Redis für Performance (Geplant)
- **Monitoring**: Observability mit Aspire (Geplant)

## Quick Start

### Voraussetzungen
- .NET 8 SDK
- Git

### Installation
```bash
# Repository klonen
git clone https://github.com/[username]/online-shop-ddd.git
cd online-shop-ddd

# Dependencies installieren
dotnet restore

# Datenbank erstellen
cd src/ProductCatalog/ProductCatalog.Api
dotnet ef database update --project ../ProductCatalog.Infrastructure

# API starten
dotnet run
```

### API testen
1. Öffne `https://localhost:7xxx/swagger`
2. Teste den AddProduct Endpoint:
```json
{
  "name": "iPhone 15",
  "description": "Latest Apple smartphone",
  "priceAmount": 999.99,
  "currency": "EUR",
  "initialStock": 50
}
```

## Projektstruktur

```
src/ProductCatalog/
├── ProductCatalog.Api/          # HTTP Endpoints, Program.cs
├── ProductCatalog.Application/  # Use Cases, Commands, Queries
├── ProductCatalog.Domain/       # Entities, Value Objects, Events
├── ProductCatalog.Infrastructure/ # Database, Repositories
└── ProductCatalog.Tests/        # Unit & Integration Tests
```

## Tests

```bash
# Alle Tests ausführen
dotnet test

# Tests mit Coverage
dotnet test --collect:"XPlat Code Coverage"
```

## Lernziele

Dieses Projekt dient dem Erlernen von:
- ✅ Domain Driven Design (DDD) Prinzipien
- ✅ Clean Architecture Implementierung
- ✅ CQRS Pattern mit MediatR
- ✅ Event Storming als Design-Methode
- ✅ Vertical Slice Architecture
- 🔄 Event-Driven Architecture (in Entwicklung)
- 🔄 Microservices Communication (geplant)
- 🔄 .NET Aspire für Service Orchestration (geplant)


## Status

**Aktueller Stand**: Product Catalog Bounded Context begonnen
**Nächste Schritte**: Product Catalog Bounded Context weiter ausbauen

