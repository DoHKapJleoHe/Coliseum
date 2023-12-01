using CardLib;
using Microsoft.EntityFrameworkCore;

namespace DbLib;

public interface IReaderWriter
{
    public void WriteToDatabase(Deck deck);
    public List<Deck> ReadFromDatabase();
}

public class ReaderWriter : IReaderWriter
{
    private readonly DeckDbContext _db;

    public ReaderWriter(IDbContextFactory<DeckDbContext> dbContextFactory)
    {
        _db = dbContextFactory.CreateDbContext();
        _db.Database.EnsureCreated();
    }

    public void WriteToDatabase(Deck deck)
    {
        List<CardEntity> cardsEntity = new List<CardEntity>();
        foreach (var card in deck.GetCardsArray())
        {
            cardsEntity.Add(new CardEntity()
            {
                Number = card.Number,
                Color = card.Color.ToString()
            });
        }

        DeckEntity deckEntity = new DeckEntity()
        {
            Deck = cardsEntity
        };
        
        _db.AddRange(cardsEntity);
        _db.Add(deckEntity);
        _db.SaveChanges();
    }

    public List<Deck> ReadFromDatabase()
    {
        List<Deck> decks = new List<Deck>();
        var deckEntities = _db.Decks.Include(d => d.Deck).ToList();

        foreach (var deck in deckEntities)
        {
            Card[] cardsArr = new Card[36];
            int i = 0;
            foreach (var card in deck.Deck)
            {
                cardsArr[i] = new Card(card.Number, Enum.Parse<Color>(card.Color));
                i++;
            }
            decks.Add(new Deck(cardsArr));
        }

        return decks;
    }
}