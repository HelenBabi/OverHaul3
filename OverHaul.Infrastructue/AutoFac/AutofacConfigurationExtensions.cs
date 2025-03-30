using Core.Data;
using Overhaul.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Overhaul.Domain.Common;
using Overhaul.Application.AutoFac;

namespace Overhaul.Infrastructuer.AutoFac
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddAutofacDependencyServices(this ContainerBuilder containerBuilder)
        {
            var currentAssembly = Assembly.Load("Overhaul.Infrastructure");
            var coreAssembly = Assembly.Load("Overhaul.Application");
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}
