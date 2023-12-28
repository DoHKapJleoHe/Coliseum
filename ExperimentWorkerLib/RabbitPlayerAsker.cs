using System.Drawing;
using ColiseumWebApp;
using MassTransit;
using Color = CardLib.Color;

namespace ExperimentWorker;

public interface IRabbitPlayerAsker
{
    public void SendElonDeckAsync(PickCard _pickCard);
    public void SendMarkDeckAsync(PickCard _pickCard);
    public Task<Color> GetElonColorAsync();
    public Task<Color> GetMarkColorAsync();
}

public class RabbitPlayerAsker : IRabbitPlayerAsker
{
    private const string MarkApiUrl = "http://localhost:5002/player/color";
    private const string ElonApiUrl = "http://localhost:5001/player/color";   
    private readonly IBus _bus;

    public RabbitPlayerAsker(IBus bus)
    {
        _bus = bus;
    }

    public async void SendElonDeckAsync(PickCard _pickCard)
    {
        var endpointElon = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/elon-queue"));
        await endpointElon.Send<PickCard>(_pickCard);
    }
    
    public async void SendMarkDeckAsync(PickCard _pickCard)
    {
        var endpointMark = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/mark-queue"));
    }

    public async Task<Color> GetElonColorAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            using HttpResponseMessage response = await client.GetAsync(ElonApiUrl);
            
            response.EnsureSuccessStatusCode();
            String responseBody = await response.Content.ReadAsStringAsync();
            Color color = Enum.Parse<Color>(responseBody);

            return color;
        }
    }

    public async Task<Color> GetMarkColorAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            using HttpResponseMessage response = await client.GetAsync(MarkApiUrl);
            
            response.EnsureSuccessStatusCode();
            String responseBody = await response.Content.ReadAsStringAsync();
            Color color = Enum.Parse<Color>(responseBody);

            return color;
        }
    }
}