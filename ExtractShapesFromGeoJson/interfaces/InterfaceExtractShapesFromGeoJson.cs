using OutSystems.ExternalLibraries.SDK;
using ExtractShapesFromGeoJson.structures;

namespace ExtractShapesFromGeoJson.interfaces
{
    [OSInterface(
        Name = "ExtractShapesFromGeoJson",
        Description = ""
    )]
    public interface InterfaceExtractShapesFromGeoJson
    {   
        [OSAction(
            Description = "",
            ReturnDescription = "Return Description",
            ReturnName = "Shapes",
            ReturnType = OSDataType.InferredFromDotNetType
        )]
        Shapes ExtractShapesFromGeoJson(string GeoJSONText);
    }
}