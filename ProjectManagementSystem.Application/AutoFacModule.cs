

using Autofac;
using ProjectManagementSystem.Entity.Data;

namespace ProjectManagementSystem.Application
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Context>().InstancePerLifetimeScope();
            
        }
    }
}
