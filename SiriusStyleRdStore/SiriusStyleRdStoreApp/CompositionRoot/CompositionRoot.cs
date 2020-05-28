using System.Collections.Generic;
using System.Reflection;
using Autofac;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Repositories.Repositories;
using Module = Autofac.Module;

namespace SiriusStyleRdStoreApp.CompositionRoot
{
    public class CompositionRoot : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<SiriusStyleRdStoreContext>().InstancePerRequest();

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