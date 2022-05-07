using Autofac;
using Stacks;

namespace SnackMachine
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SnackMachine>()
                .As<ISnackMachine>()
                .InstancePerLifetimeScope();
        }
    }
}

