
using GMapSample.DataContract;
using GMapSample.DataModel;

namespace GMapSample.Repository
{
	public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
	{
		public PlaceRepository(IDatabaseFactory databaseFactory)
			: base(databaseFactory)
			{
			}
	}

}
