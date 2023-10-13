using AbstractPlayer;
using CardLib;
using Strategy;

namespace ElonLib;

public class Elon : Player
{
    public override string Name => "Elon";

    public override int PickCard(Card[] deck)
    {
        return 0;
    }

}