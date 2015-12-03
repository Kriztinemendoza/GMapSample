
using GMapSample.DataContract;
using GMapSample.DataModel;
using GMapSample.Repository;

namespace GMapSample.Repository
{
	
	public class AddressRepository : GenericRepository<Address>, IAddressRepository
	{
		public AddressRepository(IDatabaseFactory databaseFactory)
			: base(databaseFactory)
			{
			}
	}

}
