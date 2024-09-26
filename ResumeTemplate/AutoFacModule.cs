using Autofac;
using ResumeTemplate.Data;
using ResumeTemplate.DTO;
using ResumeTemplate.Repositories;
using ResumeTemplate.Repositories.Interface;

namespace ResumeTemplate
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Context>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                                        .InstancePerLifetimeScope();

            builder.RegisterType<UserState>().InstancePerLifetimeScope();

            builder.RegisterType<ControllereParameters>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();


        }
    }
}
