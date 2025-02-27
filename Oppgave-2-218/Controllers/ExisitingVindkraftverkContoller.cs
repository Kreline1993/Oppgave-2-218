using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Oppgave_2_218.Models;
using System.Collections.Generic;
using Supabase;
using Npgsql;
using System.Text.Json;
using Supabase.Postgrest; // Needed for .From<T>()
using System.Text.Json.Serialization;

namespace Oppgave_2_218.Controllers
{
    public class ExistingVindkraftverkController : Controller
    {
        private readonly Supabase.Client _supabaseClient;

        public ExistingVindkraftverkController(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public IActionResult MapVindkraftverk()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetVindkraftverkData()
        {
            try
            {
                var response = await _supabaseClient
                    .From<ExistingVindkraftverk>()
                    .Select("coord_geojson")
                    .Get();

                var vindkraftverks = response.Models;
                return Json(vindkraftverks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }
    }
}