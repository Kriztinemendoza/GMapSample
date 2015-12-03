
namespace GMapSample.DataModel
{

    public partial class Place
    {
        public int PlaceID { get; set; }
        public string GoogleMapPlaceID { get; set; }
        public int AddressID { get; set; }
        public string PlaceName { get; set; }
    
        public virtual Address Address { get; set; }
    }
}
