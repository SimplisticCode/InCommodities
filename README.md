# Wind Turbine Remote Controlling API

## Overview
This project implements a system for managing a park of 5 wind turbines. 
It prioritizes cost-efficiency in electricity production based on market price and production targets. 
The system consists of two main components: a RESTful HTTP server for user interaction and a backend service that contains all business logic for managing the turbines.

## Getting Started

### Prerequisites
- .NET 8.0 SDK.
- Visual Studio 2019/VS Code or any other compatible IDE that supports C#

### Installation
1. Clone the repository using Git:

```bash
git clone https://github.com/SimplisticCode/InCommodities
```

2. Open the project in your IDE.

3. Build the project using the IDE or the following command:

```bash
dotnet build
```

4. Run the project using the IDE or the following command:

```bash
dotnet run --project InCommodities
```

5. The server should start successfully. You can now interact with the API using a REST client like Postman or cURL. 
A Swagger UI is also available at `http://localhost:5000/swagger/index.html`, which provides a user-friendly interface for testing the API.

## API Endpoints

### GET /api/turbines

