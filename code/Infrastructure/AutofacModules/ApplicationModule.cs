using Autofac;

namespace Infrastructure.AutofacModules;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //   builder.RegisterType<GenericRepositoryAsync<DynamicFormProductAttributes>>().As<IGenericRepositoryAsync<DynamicFormProductAttributes>>().InstancePerLifetimeScope();
        //  builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

    }
}
