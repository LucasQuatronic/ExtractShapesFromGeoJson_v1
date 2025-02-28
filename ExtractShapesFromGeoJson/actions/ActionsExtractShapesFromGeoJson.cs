using ExtractShapesFromGeoJson.interfaces;
using ExtractShapesFromGeoJson.structures;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExtractShapesFromGeoJson.actions {
    public class ActionsExtractShapesFromGeoJson : InterfaceExtractShapesFromGeoJson {
        
        public Shapes ExtractShapesFromGeoJson(string GeoJSONText, int MaxNumberOfLocations) {

            // Parse the GeoJSON
            var geoJsonObject = JObject.Parse(GeoJSONText);

            // Get the features array
            var features = geoJsonObject["features"] as JArray;

            // Initialize Shapes object
            var shapes = new Shapes
            {
                Linestrings = new List<Linestring>(),
                Polygons = new List<Polygon>()
            };

            // Take the first 10 features or less if there are fewer than 10
            var limitedFeatures = features.Take(MaxNumberOfLocations);
            // Iterate through each feature
            foreach (var feature in limitedFeatures)
            {
                // Get the geometry type and coordinates
                var geometry = feature["geometry"];
                var geometryType = geometry["type"].ToString();
                var coordinates = geometry["coordinates"];

                if (geometryType == "LineString")
                {
                    // Convert LineString to Linestring structure
                    var lineStringCoordinates = ConvertLineStringToCoordinates(coordinates);
                    shapes.Linestrings.Add(lineStringCoordinates);
                }
                else if (geometryType == "MultiLineString")
                {
                    // Convert MultiLineString to multiple Linestring structures
                    var multiLineStringCoordinates = ConvertMultiLineStringToCoordinates(coordinates);
                    shapes.Linestrings.Add(multiLineStringCoordinates);
                }
                else if (geometryType == "Polygon")
                {
                    var PolygonCoordinates = ConvertPolygonToCoordinates(coordinates);
                    shapes.Polygons.Add(PolygonCoordinates);
                }
            }

            
            return shapes;
        }

        private static Linestring ConvertLineStringToCoordinates(JToken coordinates)
        {
            var lineString = new Linestring
            {
                CoordinateList = new List<Coordinates>()
            };

            foreach (var coordinate in coordinates)
            {
                var point = new Coordinates
                {
                    XCoordinate = coordinate[0].ToObject<decimal>(),
                    YCoordinate = coordinate[1].ToObject<decimal>()
                };
                lineString.CoordinateList.Add(point);
            }

            return lineString;
        }

        private static Linestring ConvertMultiLineStringToCoordinates(JToken coordinates)
        {
            var maxLineString = new Linestring();
            int maxCount = 0;

            foreach (var line in coordinates)
            {
                var lineString = new Linestring
                {
                    CoordinateList = new List<Coordinates>()
                };

                foreach (var coordinate in line)
                {
                    var point = new Coordinates
                    {
                        XCoordinate = coordinate[0].ToObject<decimal>(),
                        YCoordinate = coordinate[1].ToObject<decimal>()
                    };
                    lineString.CoordinateList.Add(point);
                }

                if (lineString.CoordinateList.Count > maxCount)
                {
                    maxCount = lineString.CoordinateList.Count;
                    maxLineString = lineString;
                }
            }

            // Return the Linestring with the most coordinates
            return maxLineString;
        }
        
        private static Polygon ConvertPolygonToCoordinates(JToken coordinates)
        {
            var polygon = new Polygon
            {
                CoordinateList = new List<Coordinates>()
            };

            foreach (var ring in coordinates)
            {
                foreach (var coordinate in ring)
                {
                    var point = new Coordinates
                    {
                        XCoordinate = coordinate[0].ToObject<decimal>(),
                        YCoordinate = coordinate[1].ToObject<decimal>()
                    };
                    polygon.CoordinateList.Add(point);
                }
            }

            return polygon;
        }
    }
}