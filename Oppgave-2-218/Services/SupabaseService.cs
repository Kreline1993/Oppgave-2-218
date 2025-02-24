using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Newtonsoft.Json;

public class SupabaseService
{
    private readonly string _connectionString;

    public SupabaseService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SupabaseDB");
    }

    public async Task<string> GetVindkraftverkGeoJsonAsync()
    {
        await using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        const string query = "SELECT * FROM get_vindkraftverk_geojson();";

        await using var cmd = new NpgsqlCommand(query, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return reader.GetString(0); // Returns the JSON string
        }

        return "[]"; // Return empty JSON array if no data
    }
}
