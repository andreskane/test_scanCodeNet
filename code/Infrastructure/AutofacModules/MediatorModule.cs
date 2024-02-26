
using Autofac;
using MediatR;
using System.Reflection;

namespace Infrastructure.AutofacModules;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();


        //builder.RegisterAssemblyTypes(typeof(GetPersonByRutQuery).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        //builder.RegisterAssemblyTypes(typeof(GetFullPersonByRutQuery).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        //builder.RegisterAssemblyTypes(typeof(GetFullPersonByClientNumberQuery).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

        // builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }
}
