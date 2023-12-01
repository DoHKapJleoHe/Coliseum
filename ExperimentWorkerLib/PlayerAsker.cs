using System.Net;
using System.Net.Http.Json;
using System.Text;
using CardLib;
using ColiseumWebApp;

namespace ExperimentWorker;

public interface IPlayerAsker
{
    public Task<int> AskMark(Card[] deck);
    public Task<int> AskElon(Card[] deck);
}

public class PlayerAsker : IPlayerAsker
{
    private readonly HttpClient _client = new();
    private const string MarkApiUrl = "http://localhost:5002/player";
    private const string ElonApiUrl = "http://localhost:5001/player";

    public async Task<int> AskMark(Card[] deck)
    {
        var jsonDeck = ConvertDeck(deck);
        var content = JsonContent.Create(jsonDeck);
        var response = await _client.PostAsync(MarkApiUrl, content);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            if (int.TryParse(responseBody, out int cardPosition))
            {
                Console.WriteLine("Mark chose position: " + cardPosition);
                return cardPosition;
            }

            Console.WriteLine("Couldn't convert string response to number!");
            return -1;
        }

        Console.WriteLine("Error while receiving response: " + response.ToString());
        Console.WriteLine(jsonDeck);
        return -1;
    }

    public async Task<int> AskElon(Card[] deck)
    {
        var jsonDeck = ConvertDeck(deck);
        var content = JsonContent.Create(jsonDeck);
        var response = await _client.PostAsync(ElonApiUrl, content);

        Console.WriteLine(response.ToString());
        Console.WriteLine(content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            if (int.TryParse(responseBody, out int cardPosition))
            {
                Console.WriteLine("Elon chose position: " + cardPosition);
                return cardPosition;
            }

            Console.WriteLine("Couldn't convert string response to number!");
            return -1;
        }
        else
        {
            Console.WriteLine("Error while receiving response from server!");
            return -1;
        }
    }
    
    private static IEnumerable<CardDto> ConvertDeck(Card[] deck)
    {
        var dtos = new CardDto[deck.Length];
        for (var i = 0; i < deck.Length; i++)
        {
            dtos[i] = new CardDto(deck[i]);
        }
        return dtos;
    }
}