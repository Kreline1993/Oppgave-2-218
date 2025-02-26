using Newtonsoft.Json;
using RestSharp;
using Oppgave_2_218.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oppgave_2_218.Services
{
    public class PowerPlantService
    {
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;
        private readonly RestClient _client;
        private const string TABLE_NAME = "Vindkraftverk"; // Update this to match your actual table name in Supabase

        public PowerPlantService(IConfiguration configuration)
        {
            _supabaseUrl = configuration["Supabase:Url"];
            _supabaseKey = configuration["Supabase:ApiKey"];
            _client = new RestClient(_supabaseUrl);
        }

        /// <summary>
        /// Get all power plants from the database
        /// </summary>
        public async Task<List<PowerPlant>> GetAllPowerPlantsAsync()
        {
            return await GetPowerPlantsAsync();
        }

        /// <summary>
        /// Get power plants with optional filtering
        /// </summary>
        public async Task<List<PowerPlant>> GetPowerPlantsAsync(string filterColumn = null, string filterOperator = null, string filterValue = null)
        {
            var request = new RestRequest($"/rest/v1/{TABLE_NAME}");
            request.Method = Method.Get;

            // Add Supabase headers
            request.AddHeader("apikey", _supabaseKey);
            request.AddHeader("Authorization", $"Bearer {_supabaseKey}");
            request.AddHeader("Content-Type", "application/json");

            // Add basic select
            request.AddQueryParameter("select", "*");

            // Add filtering if provided
            if (!string.IsNullOrEmpty(filterColumn) &&
                !string.IsNullOrEmpty(filterOperator) &&
                !string.IsNullOrEmpty(filterValue))
            {
                request.AddQueryParameter(filterColumn, $"{filterOperator}.{filterValue}");
            }

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve power plant data: {response.ErrorMessage ?? response.Content}");
            }

            return JsonConvert.DeserializeObject<List<PowerPlant>>(response.Content);
        }

        /// <summary>
        /// Get power plants by municipality
        /// </summary>
        public async Task<List<PowerPlant>> GetPowerPlantsByMunicipalityAsync(string municipality)
        {
            return await GetPowerPlantsAsync("kommunenavn", "eq", municipality);
        }

        /// <summary>
        /// Get power plants by county
        /// </summary>
        public async Task<List<PowerPlant>> GetPowerPlantsByCountyAsync(string county)
        {
            return await GetPowerPlantsAsync("fylkesnavn", "eq", county);
        }

        /// <summary>
        /// Get power plants by status
        /// </summary>
        public async Task<List<PowerPlant>> GetPowerPlantsByStatusAsync(string status)
        {
            return await GetPowerPlantsAsync("status", "eq", status);
        }

        /// <summary>
        /// Get power plants with minimum effect
        /// </summary>
        public async Task<List<PowerPlant>> GetPowerPlantsByMinimumEffectAsync(double minEffectMw)
        {
            return await GetPowerPlantsAsync("effekt_mw", "gte", minEffectMw.ToString());
        }

        /// <summary>
        /// Get power plant by ID
        /// </summary>
        public async Task<PowerPlant> GetPowerPlantByIdAsync(int id)
        {
            var plants = await GetPowerPlantsAsync("id", "eq", id.ToString());
            return plants.FirstOrDefault();
        }
    }
}