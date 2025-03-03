using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Oppgave_2_218.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Oppgave_2_218.Services
{
    public class ExistingVindkraftverkService
    {
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;
        private readonly RestClient _client;
        private string _tableName;

        public ExistingVindkraftverkService(IConfiguration configuration)
        {
            _supabaseUrl = configuration["Supabase:Url"];
            _supabaseKey = configuration["Supabase:ApiKey"];
            _client = new RestClient(_supabaseUrl);
            _tableName = "Vindkraftverk"; // Set the table name
        }

        /// <summary>
        /// Get all existing wind power plants from the database
        /// </summary>
        public async Task<List<ExistingVindkraftverk>> GetAllExistingVindkraftverkAsync()
        {
            return await GetExistingVindkraftverkAsync();
        }

        /// <summary>
        /// Get GeoJSON feature collection for all existing wind power plants
        /// </summary>
        public async Task<string> GetExistingVindkraftverkGeoJsonAsync()
        {
            var vindkraftverks = await GetAllExistingVindkraftverkAsync();

            // Create GeoJSON feature collection
            var featureCollection = new
            {
                type = "FeatureCollection",
                features = vindkraftverks
                    .Select(v =>
                    {
                        var coordinates = JsonConvert.DeserializeObject<double[]>(v.Coord_GeoJson);
                        return new
                        {
                            type = "Feature",
                            geometry = new
                            {
                                type = "Point",
                                coordinates = coordinates
                            },
                            properties = new
                            {
                                id = v.Id,
                                // Add other properties as needed
                            }
                        };
                    }).ToArray()
            };

            return JsonConvert.SerializeObject(featureCollection);
        }

        /// <summary>
        /// Private method to get all existing wind power plants from the database
        /// </summary>
        private async Task<List<ExistingVindkraftverk>> GetExistingVindkraftverkAsync()
        {
            var request = new RestRequest($"rest/vindkraftverk?select=*", Method.Get);
            request.AddHeader("apikey", _supabaseKey);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Failed to retrieve data from Supabase");
            }

            return JsonConvert.DeserializeObject<List<ExistingVindkraftverk>>(response.Content);
        }
    }
}
