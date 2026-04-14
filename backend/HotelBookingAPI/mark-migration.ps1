$connectionString = "Server=tcp:medicineserver.database.windows.net,1433;Initial Catalog=free-sql-db-5742464;Persist Security Info=False;User ID=sqladmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
$migrationId = "20260413071054_InitialCreateAzure"
$productVersion = "10.0.5"

$SqlConnection = New-Object System.Data.SqlClient.SqlConnection
$SqlConnection.ConnectionString = $connectionString

try {
    $SqlConnection.Open()
    Write-Host "Connected to Azure SQL Database successfully!"
    
    $SqlCmd = New-Object System.Data.SqlClient.SqlCommand
    $SqlCmd.CommandText = @"
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES ('$migrationId', '$productVersion')
"@
    $SqlCmd.Connection = $SqlConnection
    
    $SqlCmd.ExecuteNonQuery()
    Write-Host "Migration marked as applied successfully!"
}
catch {
    Write-Host "Error: $_"
}
finally {
    $SqlConnection.Close()
}
