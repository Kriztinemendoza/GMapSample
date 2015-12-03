using GMapSample.DataContract;
using GMapSample.DataModel;

namespace GMapSample.Repository
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private GMapSampleContext _dataContext;
        public GMapSampleContext Get()
        {
            return _dataContext ?? (_dataContext = new GMapSampleContext());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
