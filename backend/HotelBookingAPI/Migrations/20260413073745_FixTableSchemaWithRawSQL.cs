using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixTableSchemaWithRawSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add missing columns to Rooms table
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'BasePrice')
                    ALTER TABLE Rooms ADD BasePrice DECIMAL(18,2) NOT NULL DEFAULT 0;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'CurrentPrice')
                    ALTER TABLE Rooms ADD CurrentPrice DECIMAL(18,2) NOT NULL DEFAULT 0;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'Capacity')
                    ALTER TABLE Rooms ADD Capacity INT NOT NULL DEFAULT 2;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'Description')
                    ALTER TABLE Rooms ADD Description NVARCHAR(MAX) NULL;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'Amenities')
                    ALTER TABLE Rooms ADD Amenities NVARCHAR(MAX) NULL;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'AvailableRooms')
                    ALTER TABLE Rooms ADD AvailableRooms INT NOT NULL DEFAULT 1;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'IsActive')
                    ALTER TABLE Rooms ADD IsActive BIT NOT NULL DEFAULT 1;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'Rating')
                    ALTER TABLE Rooms ADD Rating REAL NOT NULL DEFAULT 0;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'ReviewCount')
                    ALTER TABLE Rooms ADD ReviewCount INT NOT NULL DEFAULT 0;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'CreatedAt')
                    ALTER TABLE Rooms ADD CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE();
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rooms') AND name = 'UpdatedAt')
                    ALTER TABLE Rooms ADD UpdatedAt DATETIME2 NULL;
            ");
            
            // Add missing columns to Amenities table
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Amenities') AND name = 'Description')
                    ALTER TABLE Amenities ADD Description NVARCHAR(MAX) NULL;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Amenities') AND name = 'Price')
                    ALTER TABLE Amenities ADD Price DECIMAL(18,2) NOT NULL DEFAULT 0;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Amenities') AND name = 'IsActive')
                    ALTER TABLE Amenities ADD IsActive BIT NOT NULL DEFAULT 1;
                    
                IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Amenities') AND name = 'CreatedAt')
                    ALTER TABLE Amenities ADD CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
