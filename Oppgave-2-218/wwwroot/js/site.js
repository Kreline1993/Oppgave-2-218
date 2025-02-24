// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


async function loadVindkraftverkGeoJSON() {
    try {
        const response = await fetch('/api/vindkraftverk/geojson');
        const data = await response.json();

        console.log("GeoJSON Data:", data); // Debugging

        if (data && data.length > 0) {
            L.geoJSON(data, {
                onEachFeature: function (feature, layer) {
                    if (feature.properties) {
                        layer.bindPopup(`<b>${feature.properties.name}</b>`);
                    }
                }
            }).addTo(map);
        } else {
            console.warn("No features found in GeoJSON response.");
        }
    } catch (error) {
        console.error("Error fetching GeoJSON:", error);
    }
}

// Initialize Leaflet
var map = L.map('map').setView([58.1633, 8.0025], 16);


L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topo/default/webmercator/{z}/{y}/{x}.png', {
    maxZoom: 19,
    attribution: '&copy; Kartverket</a>'
}).addTo(map);

// Load GeoJSON data
loadVindkraftverkGeoJSON();
