using System.Collections.Generic;

namespace TwitterClientMobile.JsonFormatterTypes
{
    public class Address_Component
    {
        public string Long_Name { get; set; }
        public string Short_Name { get; set; }
        public List<string> Types { get; set; }
    }

    public class Northeast
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Southwest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Bounds
    {
        public Northeast NorthEast { get; set; }
        public Southwest SouthWest { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class NorthEast2
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class SouthWest2
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Viewport
    {
        public NorthEast2 NorthEast { get; set; }
        public SouthWest2 SouthWest { get; set; }
    }

    public class Geometry
    {
        public Bounds Bounds { get; set; }
        public Location Location { get; set; }
        public string LocationType { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Result
    {
        public List<Address_Component> Address_Components { get; set; }
        public string Formatted_Address { get; set; }
        public Geometry Geometry { get; set; }
        public string Place_Id { get; set; }
        public List<string> Types { get; set; }
    }

    public class GeoCode
    {
        public List<Result> Results { get; set; }
        public string Status { get; set; }
    }
}
