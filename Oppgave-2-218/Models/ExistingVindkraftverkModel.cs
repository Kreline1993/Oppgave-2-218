using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Oppgave_2_218.Models
{
    [Table("Vindkraftverk")]
    public class ExistingVindkraftverk : BaseModel
    {
        public int Id { get; set; }
        public string? Gml { get; set; } // Made nullable
        public string? Objekttype { get; set; } // Made nullable
        public int SkaId { get; set; }
        public string? SakTittel { get; set; } // Made nullable
        public string? Tiltakshaver { get; set; } // Made nullable
        public int SakKategori { get; set; }
        public string? Status { get; set; } // Made nullable
        public double EffektMw { get; set; }
        public double EffektIdriftMw { get; set; }
        public double ForventetProduksjonGwh { get; set; }
        public string? SakLenke { get; set; } // Made nullable
        public string? KommuneNavn { get; set; } // Made nullable
        public string? FylkesNavn { get; set; } // Made nullable
        public string? IdIftDato { get; set; } // Made nullable
        public string? UtAvDriftDato { get; set; } // Made nullable
        public int TotalAntTurbiner { get; set; }
        public int ObjekStatus { get; set; }
        public string? LokalId { get; set; } // Made nullable
        public string? DataUttaksDato { get; set; } // Made nullable
        public string? EksportType { get; set; } // Made nullable
        public string? Coord_GeoJson { get; set; } // Made nullable
    }
}