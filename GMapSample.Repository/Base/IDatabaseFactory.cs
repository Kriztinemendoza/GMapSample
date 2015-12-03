using System;
using GMapSample.DataModel;

namespace GMapSample.Repository
{
    public interface IDatabaseFactory : IDisposable
    {
        GMapSampleContext Get();
    }
}
