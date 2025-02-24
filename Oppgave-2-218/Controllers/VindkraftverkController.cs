using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/vindkraftverk")]
[ApiController]
public class VindkraftverkController : ControllerBase
{
    private readonly SupabaseService _supabaseService;

    public VindkraftverkController(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    [HttpGet("geojson")]
    public async Task<IActionResult> GetGeoJson()
    {
        var geoJson = await _supabaseService.GetVindkraftverkGeoJsonAsync();
        return Content(geoJson, "application/json");
    }
}
