using CardLib;

namespace Strategy;

public interface ICardPickStrategy
{ 
    public int Pick(Card[] deck);
}

public class PickStrategy : ICardPickStrategy
{
    private string _who = "Elon";
    public int Pick(Card[] deck)
    {
        int max = 0;
        if (_who.Equals("Elon"))
        {
            foreach (var card in deck)
            {
                if (card.Color == Color.Black)
                {
                    max++;
                }
            }

            _who = "Mark";
        }
        else
        {
            foreach (var card in deck)
            {
                if (card.Color == Color.Red)
                {
                    max++;
                }
            }

            _who = "Elon";
        }

        return max;
    }
}
