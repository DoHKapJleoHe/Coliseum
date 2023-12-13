using AbstractPlayer;
using CardLib;

namespace MarkLib;

public class Mark : Player
{
    public override string Name => "Mark";

    public override int PickCard(Card[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            if (deck[i].Color == Color.Red)
            {
                return i;
            }
        }
        
        //default
        return 0;
    }

}