namespace GisTransService.Models.Params
{
    public class Map8GeoCodeParam
    {
        public string key { get; set; }
        public string address { get; set; }
        public string latlng { get; set; }
        public bool postcode { get; set; }
        public bool formatted_address_embed_postcode { get; set; }
    }
}
