using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        // Geometry property from database
        [JsonProperty("geom")]
        public JObject Geometry { get; set; }

        // Additional GeoJSON property if present in database
        [JsonProperty("geojson")]
        public JObject GeoJson { get; set; }

        // Calculated properties for coordinates
        [JsonIgnore]
        public (double X, double Y)? CoordinatesUTM => ExtractCoordinatesFromGeometry();

        [JsonIgnore]
        public string CoordinatesDisplay => CoordinatesUTM.HasValue
            ? $"{CoordinatesUTM.Value.X}, {CoordinatesUTM.Value.Y}"
            : "No coordinates";

        // Helper methods for extracting coordinates
        private (double X, double Y)? ExtractCoordinatesFromGeometry()
        {
            try
            {
                // Try to extract from Geometry property first
                if (Geometry != null)
                {
                    // Check if it's a Feature with geometry
                    if (Geometry["type"]?.ToString() == "Feature" && Geometry["geometry"] != null)
                    {
                        var geometry = Geometry["geometry"];
                        var geoType = geometry["type"]?.ToString();

                        if (geoType == "MultiPoint" && geometry["coordinates"] is JArray coords &&
                            coords.Count > 0 && coords[0] is JArray point &&
                            point.Count >= 2)
                        {
                            return (point[0].Value<double>(), point[1].Value<double>());
                        }
                        else if (geoType == "Point" && geometry["coordinates"] is JArray pointCoords &&
                                 pointCoords.Count >= 2)
                        {
                            return (pointCoords[0].Value<double>(), pointCoords[1].Value<double>());
                        }
                    }
                    // Direct MultiPoint geometry
                    else if (Geometry["type"]?.ToString() == "MultiPoint" &&
                             Geometry["coordinates"] is JArray multiCoords &&
                             multiCoords.Count > 0 && multiCoords[0] is JArray firstPoint &&
                             firstPoint.Count >= 2)
                    {
                        return (firstPoint[0].Value<double>(), firstPoint[1].Value<double>());
                    }
                    // Direct Point geometry
                    else if (Geometry["type"]?.ToString() == "Point" &&
                             Geometry["coordinates"] is JArray pointCoords &&
                             pointCoords.Count >= 2)
                    {
                        return (pointCoords[0].Value<double>(), pointCoords[1].Value<double>());
                    }
                }

                // If not found in Geometry, try GeoJson
                if (GeoJson != null)
                {
                    if (GeoJson["geometry"] != null)
                    {
                        var geometry = GeoJson["geometry"];
                        var geoType = geometry["type"]?.ToString();

                        if (geoType == "Point" && geometry["coordinates"] is JArray pointCoords &&
                            pointCoords.Count >= 2)
                        {
                            return (pointCoords[0].Value<double>(), pointCoords[1].Value<double>());
                        }
                        else if (geoType == "MultiPoint" && geometry["coordinates"] is JArray multiCoords &&
                                 multiCoords.Count > 0 && multiCoords[0] is JArray firstPoint &&
                                 firstPoint.Count >= 2)
                        {
                            return (firstPoint[0].Value<double>(), firstPoint[1].Value<double>());
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // Convert UTM coordinates to WGS84 (lat/lng) for map display
        public (double Lat, double Lng)? GetWGS84Coordinates()
        {
            if (!CoordinatesUTM.HasValue)
                return null;

            // Get UTM coordinates
            double easting = CoordinatesUTM.Value.X;
            double northing = CoordinatesUTM.Value.Y;

            // Only convert to WGS84 if they look like UTM coordinates
            // This is a simple check to see if they're in a reasonable range for UTM Zone 33N
            if (easting > 0 && easting < 1000000 && northing > 6000000 && northing < 10000000)
            {
                return ConvertUTM33ToLatLng(easting, northing);
            }

            // If they don't look like UTM, they might already be in WGS84 format
            if (CoordinatesUTM.Value.Y >= -90 && CoordinatesUTM.Value.Y <= 90 &&
                CoordinatesUTM.Value.X >= -180 && CoordinatesUTM.Value.X <= 180)
            {
                return (CoordinatesUTM.Value.Y, CoordinatesUTM.Value.X); // Lat, Lng
            }

            return null;
        }

        // Convert UTM Zone 33N (EPSG:32633) to WGS84 lat/lng
        private (double Lat, double Lng) ConvertUTM33ToLatLng(double easting, double northing)
        {
            // Constants for UTM Zone 33N to WGS84 conversion
            const double a = 6378137.0;          // WGS84 semi-major axis
            const double f = 1.0 / 298.257223563; // WGS84 flattening
            const double k0 = 0.9996;            // UTM scale factor
            const double centralMeridian = 15.0; // Central meridian for zone 33
            const double falseEasting = 500000.0; // UTM false easting
            const double falseNorthing = 0.0;    // UTM false northing (Northern hemisphere)

            // Derived constants
            double e2 = f * (2 - f);             // Eccentricity squared
            double e4 = e2 * e2;
            double e6 = e4 * e2;
            double n = f / (2 - f);              // Third flattening
            double n2 = n * n;
            double n3 = n2 * n;

            // Remove false easting and northing
            double x = easting - falseEasting;
            double y = northing - falseNorthing;

            // Meridional arc
            double M = y / k0;

            // Coefficients for latitude calculation
            double mu = M / (a * (1 - e2 / 4 - 3 * e4 / 64 - 5 * e6 / 256));

            // Latitude calculation
            double phi1 = mu + (3 * n / 2 - 27 * n3 / 32) * Math.Sin(2 * mu) + (21 * n2 / 16 - 55 * n3 * n / 32) * Math.Sin(4 * mu) + (151 * n3 / 96) * Math.Sin(6 * mu);

            // Terms for calculating longitude
            double cosPhiSq = Math.Cos(phi1) * Math.Cos(phi1);
            double tanPhi = Math.Tan(phi1);
            double tanPhiSq = tanPhi * tanPhi;

            double N1 = a / Math.Sqrt(1 - e2 * Math.Sin(phi1) * Math.Sin(phi1));
            double T1 = tanPhiSq;
            double C1 = e2 * cosPhiSq / (1 - e2);
            double R1 = a * (1 - e2) / Math.Pow(1 - e2 * Math.Sin(phi1) * Math.Sin(phi1), 1.5);

            double D = x / (N1 * k0);
            double D2 = D * D;
            double D3 = D2 * D;
            double D4 = D2 * D2;
            double D5 = D4 * D;
            double D6 = D4 * D2;

            // Calculate latitude
            double lat = phi1 - (N1 * tanPhi / R1) * (D2 / 2 - (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * e2) * D4 / 24 + (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * e2 - 3 * C1 * C1) * D6 / 720);

            // Calculate longitude
            double lon = centralMeridian + (D - (1 + 2 * T1 + C1) * D3 / 6 + (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * e2 + 24 * T1 * T1) * D5 / 120) / Math.Cos(phi1);

            // Convert to degrees
            lat = lat * 180 / Math.PI;
            lon = lon * 180 / Math.PI;

            return (lat, lon);
        }
    }
}