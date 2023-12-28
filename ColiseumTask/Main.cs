using AbstractPlayer;
using ColiseumWebApp;
using DbLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DeckShuffler;
using ElonLib;
using ExperimentWorker;
using MarkLib;
using MassTransit;
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
            if(args[0] == "")
                Console.WriteLine("Not enough args!");
            
            var action = args[0];

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
                    services.AddScoped<IRabbitPlayerAsker, RabbitPlayerAsker>();
                    services.AddDbContextFactory<DeckDbContext>(options => options.UseSqlite($"Data Source=decks.db"));
                    services.AddScoped<IReaderWriter, ReaderWriter>();
                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("rmuser");
                                h.Password("rmpassword");
                            });
                            
                            cfg.ConfigureEndpoints(ctx);
                        });
                    });
                });
        }
    }

    /*public class Config
    {
        public string Type { get; set; }
        public 
    }*/
}
