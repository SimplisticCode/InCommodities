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



- By default, the server will start on `http://localhost:5000/`. You can change the port in the `appsettings.json` file.

## Usage

### Endpoints
- **Increase Production Target**: `POST /api/production/increase`
- Body: `{ "amount": 10 }`
- **Decrease Production Target**: `POST /api/production/decrease`
- Body: `{ "amount": 5 }`
- **Submit Market Price**: `POST /api/market/submit-price`
- Body: `{ "price": 6 }`
- **Get Turbines Status**: `GET /api/turbines/status`
- No body required. Returns a list of turbines with their expected production.

### Examples
- To set the market price to 6€ and increase the production target by 10MWh:

