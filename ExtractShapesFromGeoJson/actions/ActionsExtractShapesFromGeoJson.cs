using ExtractShapesFromGeoJson.interfaces;
using ExtractShapesFromGeoJson.structures;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExtractShapesFromGeoJson.actions {
    public class ActionsExtractShapesFromGeoJson : InterfaceExtractShapesFromGeoJson {
        
        public Shapes ExtractShapesFromGeoJson(string GeoJSONText) {

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

            // Iterate through each feature
            foreach (var feature in features)
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
                    shapes.Linestrings.AddRange(multiLineStringCoordinates);
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

        private static List<Linestring> ConvertMultiLineStringToCoordinates(JToken coordinates)
        {
            var result = new List<Linestring>();

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

                result.Add(lineString);
            }

            return result;
        }
        
        private static Polygon ConvertPolygonToCoordinates(JToken coordinates)
        {
            var Polygon = new Polygon
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
                Polygon.CoordinateList.Add(point);
            }

            return Polygon;
        }
    }
}