using System.Text;

namespace ColiseumTask;

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

    private void ShuffleDeck()
    {
        var rnd = new Random();
        var num = _deck.Length;

        while (num > 1)
        {
            var k = rnd.Next(num--);
            (_deck[num], _deck[k]) = (_deck[k], _deck[num]);
        }
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