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