using AbstractPlayer;
using CardLib;
using Strategy;

namespace ElonLib;

public class Elon : Player
{
    public override string Name => "Elon";

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