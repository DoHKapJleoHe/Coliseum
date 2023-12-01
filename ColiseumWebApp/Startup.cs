using AbstractPlayer;
using ElonLib;
using MarkLib;

namespace ColiseumWebApp;

public class Startup
{
    private Player _player;

    public Startup(string player)
    {
        _player = player switch
        {
            "Elon" => new Elon(),
            "Mark" => new Mark(),
            _ => throw new ArgumentException("Bad player name")
        };
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_player);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
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