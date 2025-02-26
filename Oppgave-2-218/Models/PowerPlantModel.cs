using System;

namespace Oppgave_2_218.Models
{
    /// <summary>
    /// Model representing power plant data with geospatial information
    /// </summary>
    public class PowerPlant
    {
        // Database columns
        public int Id { get; set; }
        public string Gml { get; set; }
        public string Objekttype { get; set; }
        public int SkaId { get; set; }
        public string SakTittel { get; set; }
        public string Tiltakshaver { get; set; }
        public int SakKategori { get; set; }
        public string Status { get; set; }
        public double EffektMw { get; set; }
        public double EffektIdriftMw { get; set; }
        public double ForventetProduksjonGwh { get; set; }
        public string SakLenke { get; set; }
        public string KommuneNavn { get; set; }
        public string FylkesNavn { get; set; }
        public string IdIftDato { get; set; }
        public string UtAvDriftDato { get; set; }
        public int TotalAntTurbiner { get; set; }
        public int ObjekStatus { get; set; }
        public string LokalId { get; set; }
        public string DataUttaksDato { get; set; }
        public string EksportType { get; set; }
    }
}
