using System.Numerics;
using AbstractPlayer;
using CardLib;
using ColiseumWebApp;
using DbLib;
using DeckShuffler;
using ExperimentWorker;
using MassTransit;
using MassTransit.Internals.Caching;
using Microsoft.Extensions.Logging;

namespace SandboxLib;

public class Sandbox
{
    private readonly IReaderWriter _readerWriter;
    private readonly IDeckShuffler _deckShuffler;
    private readonly IPlayerAsker _playerAsker;
    private readonly IRabbitPlayerAsker _rabbitPlayerAsker;
    private readonly Player _elon;
    private readonly Player _mark;
    
    public Sandbox(
        IDeckShuffler deckShuffler, 
        IEnumerable<Player> players, 
        IReaderWriter readerWriter, 
        IPlayerAsker playerAsker,
        IRabbitPlayerAsker rabbitPlayerAsker,
        ILogger<Sandbox> logger)
    {
        _readerWriter = readerWriter;
        _deckShuffler = deckShuffler;
        _playerAsker = playerAsker;
        _rabbitPlayerAsker = rabbitPlayerAsker;

        var enumerable = players as Player[] ?? players.ToArray();
        _mark = enumerable.ToArray()[0];
        _elon = enumerable.ToArray()[1];
        
        logger.LogInformation($"Sandbox participants: {_mark.Name}, {_elon.Name}");
    }
    
    public async Task<bool> DoOneExperiment(Deck deck, int id)
    {
        Card[] cardsArray = deck.GetCardsArray();
        _deckShuffler.Shuffle(ref cardsArray);
        
        //_readerWriter.WriteToDatabase(new Deck(cardsArray));
        
        Card[] elonDeck = cardsArray.Take(cardsArray.Length / 2).ToArray();
        Card[] markDeck = cardsArray.Skip(cardsArray.Length / 2).ToArray();

        List<CardDto> elonDto = new List<CardDto>();
        List<CardDto> markDto = new List<CardDto>();
        for (int i = 0; i < 16; i++)
        {
            elonDto[i] = new CardDto(elonDeck[i]);
            markDto[i] = new CardDto(markDeck[i]);
        }

        _rabbitPlayerAsker.SendElonDeckAsync(new PickCard()
        {
            deckDto = elonDto,
            experimentId = id
        });
        _rabbitPlayerAsker.SendMarkDeckAsync(new PickCard()
        {
            deckDto = markDto,
            experimentId = id
        });

        Color elonColor = await _rabbitPlayerAsker.GetElonColorAsync();
        Color markColor = await _rabbitPlayerAsker.GetMarkColorAsync();

        return elonColor == markColor;
        //return elonDeck[_playerAsker.AskMark(markDeck).Result].Color == markDeck[_playerAsker.AskElon(elonDeck).Result].Color;
        //return elonDeck[_mark.PickCard(markDeck)].Color == markDeck[_elon.PickCard(elonDeck)].Color;
    }
    
    public bool DoOneExperimentNoShuffle(Deck deck)
    {
        Card[] cardsArray = deck.GetCardsArray();
        
        Card[] elonDeck = cardsArray.Take(cardsArray.Length / 2).ToArray();
        Card[] markDeck = cardsArray.Skip(cardsArray.Length / 2).ToArray();

        return elonDeck[_mark.PickCard(markDeck)].Color == markDeck[_elon.PickCard(elonDeck)].Color;
    }
}