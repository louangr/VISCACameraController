using System;
using Microsoft.Extensions.DependencyInjection;
using VISCACameraController.Views;

namespace VISCACameraController.Core
{
    public class IoCInitializer
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Repositories
            // services.AddSingleton<IMyRepository, MyRepository>();

            // Services
            // services.AddSingleton(typeof(MyService));

            // ViewModels
            services.AddSingleton(typeof(ControllerPageViewModel));
            
            return services.BuildServiceProvider();
        }
    }
}