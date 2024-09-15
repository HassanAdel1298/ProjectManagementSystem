

using Autofac;
using ProjectManagementSystem.Entity.Data;
using ProjectManagementSystem.Repository.Interface;
using ProjectManagementSystem.Repository.Repositories;

namespace ProjectManagementSystem.Application
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Context>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        }
    }
}
