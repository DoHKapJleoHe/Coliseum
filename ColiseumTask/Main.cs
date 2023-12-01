using AbstractPlayer;
using DbLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DeckShuffler;
using ElonLib;
using ExperimentWorker;
using MarkLib;
using Microsoft.EntityFrameworkCore;
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
            var action = " ";
            switch (args[0])
            {
                case "generate":
                {
                    action = "generate";
                    break;
                }
                case "use":
                {
                    action = "use";
                    break;
                }
                default:
                {
                    Console.WriteLine("Not enough arguments!");
                    break;
                }
            }

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(action);
                    services.AddHostedService<Worker>();
                    services.AddScoped<IDeckShuffler, Shuffler>();
                    services.AddScoped<Sandbox>();
                    services.AddScoped<Player, Elon>();
                    services.AddScoped<Player, Mark>();
                    services.AddScoped<IPlayerAsker, PlayerAsker>();
                    services.AddDbContextFactory<DeckDbContext>(options => options.UseSqlite($"Data Source=decks.db"));
                    services.AddScoped<IReaderWriter, ReaderWriter>();
                });
        }
    }    
}
