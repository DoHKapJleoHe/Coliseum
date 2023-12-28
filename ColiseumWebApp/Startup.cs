using AbstractPlayer;
using ElonLib;
using MarkLib;
using MassTransit;

namespace ColiseumWebApp;

public class Startup
{
    private Player _player;
    private string _queue;

    public Startup(IConfiguration config)
    {
        _player = config["PLAYER"] switch
        {
            "Elon" => new Elon(),
            "Mark" => new Mark(),
            _ => throw new ArgumentException("Bad player name")
        };

        _queue = config["PLAYER"] switch
        {
            "Elon" => "elon-queue",
            "Mark" => "mark-queue",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_player);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h => {
                    h.Username("rmuser");
                    h.Password("rmpassword");
                });

                cfg.ConfigureEndpoints(ctx);
                cfg.ReceiveEndpoint(_queue, rep =>
                {
                    rep.ConfigureConsumer<PickCardConsumer>(ctx);
                    rep.ConfigureConsumer<CardPickedConsumer>(ctx);
                });
            });
        });
        services.AddSingleton<IStorage, Storage>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}