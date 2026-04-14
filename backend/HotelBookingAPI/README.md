# Hotel Booking System - ASP.NET Core Web API

## Overview
This is the backend API for the Hotel Booking System built with ASP.NET Core, Entity Framework Core, and Azure SQL Database.

## Project Structure

```
HotelBookingAPI/
├── Models/                    # Entity Models
│   ├── User.cs
│   ├── Room.cs
│   ├── Booking.cs
│   ├── Bill.cs
│   ├── Amenity.cs
│   ├── BookingAmenity.cs
│   └── Review.cs
├── DTOs/                      # Data Transfer Objects
│   ├── AuthDto.cs
│   ├── RoomDto.cs
│   ├── BookingDto.cs
│   ├── BillDto.cs
│   ├── AmenityDto.cs
│   ├── PricingDto.cs
│   └── ReviewDto.cs
├── Data/                      # Database Context
│   └── ApplicationDbContext.cs
├── Services/                  # Business Logic
│   ├── AuthService.cs
│   ├── JwtService.cs
│   ├── RoomService.cs
│   ├── BookingService.cs
│   ├── BillingService.cs
│   ├── PricingService.cs
│   └── AmenityService.cs
├── Controllers/               # API Endpoints
│   ├── AuthController.cs
│   ├── RoomsController.cs
│   ├── BookingsController.cs
│   ├── BillingController.cs
│   ├── PricingController.cs
│   └── AmenitiesController.cs
├── Migrations/                # Database Migrations
├── appsettings.json          # Configuration
├── Program.cs                # Startup Configuration
└── HotelBookingAPI.csproj    # Project File
```

## Database Configuration

### Azure SQL Database Connection
- **Server**: medicineserver.database.windows.net
- **Database**: free-sql-db-5742464
- **Username**: sqladmin
- **Password**: admin@123

Connection string is configured in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:medicineserver.database.windows.net,1433;Initial Catalog=free-sql-db-5742464;Persist Security Info=False;User ID=sqladmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

## Database Schema

### Tables
- **Users**: Stores user account information
- **Rooms**: Available hotel rooms
- **Bookings**: Room bookings by users
- **Bills**: Billing information for bookings
- **Amenities**: Additional services/amenities
- **BookingAmenities**: Bridge table for booking amenities
- **Reviews**: User reviews for rooms

## Authentication

The API uses JWT (JSON Web Tokens) for authentication.

### JWT Configuration
```json
{
  "JwtSettings": {
    "SecretKey": "your-very-long-secret-key-that-is-at-least-256-bits-for-security-purposes-change-this-in-production",
    "Issuer": "HotelBookingAPI",
    "Audience": "HotelBookingClient",
    "ExpirationHours": 24
  }
}
```

## CORS Configuration

The API is configured to accept requests from:
- `http://localhost:4200` (Angular dev server)
- `http://localhost:3000` (Alternative frontend)

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user

### Rooms
- `GET /api/rooms` - Get all rooms
- `GET /api/rooms/{id}` - Get room by ID
- `GET /api/rooms/filter` - Filter rooms by type, price, availability
- `POST /api/rooms` - Create room (Admin)
- `PUT /api/rooms/{id}` - Update room (Admin)
- `DELETE /api/rooms/{id}` - Delete room (Admin)

### Bookings
- `GET /api/bookings` - Get user's bookings
- `GET /api/bookings/{id}` - Get booking details
- `POST /api/bookings` - Create booking
- `POST /api/bookings/{id}/cancel` - Cancel booking
- `POST /api/bookings/{id}/checkout` - Complete checkout

### Billing
- `GET /api/billing/booking/{bookingId}` - Get bill for booking

### Pricing
- `POST /api/pricing/estimate` - Calculate price estimate

### Amenities
- `GET /api/amenities` - Get all amenities
- `GET /api/amenities/{id}` - Get amenity by ID
- `POST /api/amenities` - Create amenity
- `PUT /api/amenities/{id}` - Update amenity
- `DELETE /api/amenities/{id}` - Delete amenity

## Installation & Setup

### Prerequisites
- .NET 10.0 SDK
- SQL Server (Local or Azure)
- Visual Studio Code or Visual Studio

### Steps

1. **Navigate to project directory**
   ```bash
   cd D:\rooms-Booking\hotelBooking\backend\HotelBookingAPI
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run migrations (if needed)**
   ```bash
   dotnet ef database update
   ```

5. **Start the API**
   ```bash
   dotnet run
   ```

The API will start on: `http://localhost:5282`

## Running the API

### Development Mode
```bash
dotnet run
```

### Production Mode
```bash
dotnet publish -c Release
cd bin/Release/net10.0/publish
dotnet HotelBookingAPI.dll
```

## Technologies Used

- **Framework**: ASP.NET Core 10.0
- **Database**: Entity Framework Core 10.0.5 with SQL Server
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hashing**: BCrypt.Net-Next
- **ORM**: Entity Framework Core

## NuGet Packages

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Authentication.JwtBearer
- System.IdentityModel.Tokens.Jwt
- BCrypt.Net-Next

## Environment Variables

For production, set these environment variables:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ASPNETCORE_URLS=http://+:5000`
- Connection string from Azure

## Error Handling

All API endpoints return standardized error responses:
```json
{
  "success": false,
  "message": "Error description"
}
```

## Security Notes

⚠️ **Important**:
1. Change the JWT secret key in production
2. Use environment variables for sensitive data
3. Enable HTTPS in production
4. Validate all inputs
5. Use proper authorization checks

## Troubleshooting

### Connection Issues
- Verify Azure SQL credentials
- Check firewall rules on Azure SQL server
- Ensure network connectivity

### Build Errors
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Database Issues
```bash
# Reset migrations
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Support

For issues or questions, contact the development team.

---

**Last Updated**: April 13, 2026
**API Version**: 1.0.0
