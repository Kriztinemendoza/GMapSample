
using System.Data.Entity;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GMapSample.DataModel;

namespace GMapSample.DependencyInjection
{
    public class Bootstrapper
    {
        public static ContainerBuilder Builder;

        public static void Initialize()
        {
            Builder = new ContainerBuilder();
            
            ControllerInstaller.Install();
            RepositoryInstaller.Install();

            var container = Builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        
    }
}
