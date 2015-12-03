using System.Reflection;
using Autofac;
using GMapSample.Repository;
using GMapSample.DataContract;

namespace GMapSample.DependencyInjection
{
    public class RepositoryInstaller
    {
        public static void Install()
        {
            //Register Generic Repositories
            Bootstrapper.Builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            Bootstrapper.Builder.RegisterType<DatabaseFactory>()
                .As<IDatabaseFactory>()
                .InstancePerLifetimeScope();

            Bootstrapper.Builder.RegisterAssemblyTypes(Assembly.Load("GMapSample.Repository"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
