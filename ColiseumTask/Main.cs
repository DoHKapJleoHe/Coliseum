using AbstractPlayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DeckShuffler;
using ElonLib;
using ExperimentWorker;
using MarkLib;
using SandboxLib;

namespace ColiseumTask
{
    class ColiseumTask
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddScoped<IDeckShuffler, Shuffler>();
                    services.AddScoped<Sandbox>();
                    services.AddScoped<Player, Elon>();
                    services.AddScoped<Player, Mark>();
                });
        }
    }    
}
