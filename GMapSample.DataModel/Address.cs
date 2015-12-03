using System;
using System.Collections.Generic;


namespace GMapSample.DataModel
{

    public partial class Address
    {
        public Address()
        {
            this.Places = new HashSet<Place>();
        }

        public int AddressID { get; set; }
        public string AddressName { get; set; }
        public int RadiusMiles { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
