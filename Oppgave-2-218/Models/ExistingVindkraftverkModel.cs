using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Oppgave_2_218.Models
{
    [Table("vindkraftverk")]
    public class ExistingVindkraftverk : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id")]
        public int Id { get; set; }

        [JsonPropertyName("coord_geojson")]
        [Column("coord_geojson")]
        public required string CoordGeoJson { get; set; }

        // Add other properties that exist in your table
        // For example:
        // [JsonPropertyName("name")]
        // [Column("name")]
        // public string Name { get; set; }
    }
}