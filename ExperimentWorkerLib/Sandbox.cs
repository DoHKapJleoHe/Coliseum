using AbstractPlayer;
using CardLib;
using DbLib;
using DeckShuffler;
using ExperimentWorker;
using Microsoft.Extensions.Logging;

namespace SandboxLib;

public class Sandbox
{
    private readonly IReaderWriter _readerWriter;
    private readonly IDeckShuffler _deckShuffler;
    private readonly IPlayerAsker _playerAsker;
    private readonly Player _elon;
    private readonly Player _mark;
    
    public Sandbox(
        IDeckShuffler deckShuffler, 
        IEnumerable<Player> players, 
        IReaderWriter readerWriter, 
        IPlayerAsker playerAsker, 
        ILogger<Sandbox> logger
        )
    {
        _readerWriter = readerWriter;
        _deckShuffler = deckShuffler;
        _playerAsker = playerAsker;
        
        var enumerable = players as Player[] ?? players.ToArray();
        _mark = enumerable.ToArray()[0];
        _elon = enumerable.ToArray()[1];
        
        logger.LogInformation($"Sandbox participants: {_mark.Name}, {_elon.Name}");
    }
    

    public bool DoOneExperiment(Deck deck)
    {
        Card[] cardsArray = deck.GetCardsArray();
        _deckShuffler.Shuffle(ref cardsArray);
        
        _readerWriter.WriteToDatabase(new Deck(cardsArray));
        
        Card[] elonDeck = cardsArray.Take(cardsArray.Length / 2).ToArray();
        Card[] markDeck = cardsArray.Skip(cardsArray.Length / 2).ToArray();

        return elonDeck[_playerAsker.AskMark(markDeck).Result].Color == markDeck[_playerAsker.AskElon(elonDeck).Result].Color;
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