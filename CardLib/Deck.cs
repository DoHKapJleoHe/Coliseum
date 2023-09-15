using System.Text;

namespace CardLib;

public class Deck
{
    private Card[] _deck;

    public Deck()
    {
        _deck = new Card[36];
        CreateDeck();
    }

    private void CreateDeck()
    {
        for (var i = 0; i < 36; i++)
        {
            if (i < 18)
                _deck[i] = new Card(i + 1, Color.Red);
            else
                _deck[i] = new Card(i + 1, Color.Black);
        }
        
        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        var rnd = new Random();
        var num = _deck.Length;

        while (num > 1)
        {
            var k = rnd.Next(num--);
            (_deck[num], _deck[k]) = (_deck[k], _deck[num]);
        }
    }

    public Card[] GetCardsArray()
    {
        return _deck;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
    
        foreach (var card in _deck)
        {
            sb.AppendLine(card.ToString());
        }
    
        return sb.ToString(); 
    }
}