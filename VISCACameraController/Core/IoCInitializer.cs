using System;
using Microsoft.Extensions.DependencyInjection;
using VISCACameraController.Repositories.Implementations;
using VISCACameraController.Repositories.Interfaces;
using VISCACameraController.Views;

namespace VISCACameraController.Core
{
    public class IoCInitializer
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Repositories
            services.AddSingleton<ISettingsRepository, SettingsRepository>();

            // Services
            // services.AddSingleton(typeof(MyService));

            // ViewModels
            services.AddSingleton(typeof(ControllerPageViewModel));
            
            return services.BuildServiceProvider();
        }
    }
}