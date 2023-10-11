using AbstractPlayer;
using CardLib;
using DeckShuffler;
using Microsoft.Extensions.Logging;

namespace SandboxLib;

public class Sandbox
{
    private readonly IDeckShuffler _deckShuffler;
    private readonly Player _elon;
    private readonly Player _mark;
    
    public Sandbox(IDeckShuffler deckShuffler, IEnumerable<Player> players, ILogger<Sandbox> logger)
    {
        _deckShuffler = deckShuffler;
        var enumerable = players as Player[] ?? players.ToArray();
        _mark = enumerable.ToArray()[0];
        _elon = enumerable.ToArray()[1];
        
        logger.LogInformation($"Sandbox participants: {_mark.Name}, {_elon.Name}");
    }

    public bool DoOneExperiment(Deck deck)
    {
        Card[] cardsArray = deck.GetCardsArray();
        _deckShuffler.Shuffle(ref cardsArray);
        
        Card[] elonDeck = cardsArray.Take(cardsArray.Length / 2).ToArray();
        Card[] markDeck = cardsArray.Skip(cardsArray.Length / 2).ToArray();

        return elonDeck[_mark.PickCard(markDeck)].GetColor() == markDeck[_elon.PickCard(elonDeck)].GetColor();
    }
    
    public bool DoOneExperimentNoShuffle(Deck deck)
    {
        Card[] cardsArray = deck.GetCardsArray();
        
        Card[] elonDeck = cardsArray.Take(cardsArray.Length / 2).ToArray();
        Card[] markDeck = cardsArray.Skip(cardsArray.Length / 2).ToArray();

        return elonDeck[_mark.PickCard(markDeck)].GetColor() == markDeck[_elon.PickCard(elonDeck)].GetColor();
    }
}