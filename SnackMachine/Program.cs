using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnackMachine;

var hostBuilder = Host.CreateDefaultBuilder();
hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
hostBuilder.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyInjectionModule());
});

var host = hostBuilder.Build();

var snackMachine = host.Services.GetRequiredService<ISnackMachine>();
snackMachine.Run();