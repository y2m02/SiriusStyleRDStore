using System.Collections.Generic;
using System.Reflection;
using Autofac;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Repository.Repositories;
using SiriusStyleRd.Services.Services;
using Module = Autofac.Module;

namespace SiriusStyleRdApp.CompositionRoot
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SiriusStyleRdContext>().InstancePerRequest();

            RegisterQueryServices(builder, typeof(CustomerService).Assembly);
            RegisterQueryServices(builder, typeof(CustomerRepository).Assembly);
        }

        private static void RegisterQueryServices(ContainerBuilder builder, Assembly assembly)
        {
            var names = new List<string> {"Repository", "Adapter", "Service", "Factory", "Operation"};
            foreach (var name in names)
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith(name))
                    .AsImplementedInterfaces();
        }
    }
}