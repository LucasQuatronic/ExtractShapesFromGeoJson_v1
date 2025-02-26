using OutSystems.ExternalLibraries.SDK;

namespace ExtractShapesFromGeoJson.structures
{
    [OSStructure(
        Description = ""
    )]
    public struct Coordinates {
        [OSStructureField(
            DataType = OSDataType.Decimal,
            Description = "",
            IsMandatory = false,
            Decimals = 8,
            Length = 21
        )]
        public decimal XCoordinate;
        [OSStructureField(
            DataType = OSDataType.Decimal,
            Description = "",
            IsMandatory = false,
            Decimals = 8,
            Length = 21
        )]
        public decimal YCoordinate;
    }

    [OSStructure(
        Description = ""
    )]
    public struct Linestring {
        [OSStructureField(
            DataType = OSDataType.InferredFromDotNetType,
            Description = "",
            IsMandatory = false
        )]
        public List<Coordinates> CoordinateList;
    }

    [OSStructure(
        Description = ""
    )]
    public struct Polygon {
        [OSStructureField(
            DataType = OSDataType.InferredFromDotNetType,
            Description = "",
            IsMandatory = false
        )]
        public List<Linestring> CoordinateList;
    }

    [OSStructure(
        Description = ""
    )]
    public struct Shapes {
        [OSStructureField(
            DataType = OSDataType.InferredFromDotNetType,
            Description = "",
            IsMandatory = false
        )]
        public List<Linestring> Linestrings;
        [OSStructureField(
            DataType = OSDataType.InferredFromDotNetType,
            Description = "",
            IsMandatory = false
        )]
        public List<Polygon> Polygons;
    }

}