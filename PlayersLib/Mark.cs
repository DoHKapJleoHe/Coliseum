using AbstractPlayer;
using CardLib;

namespace MarkLib;

public class Mark : Player
{
    public override string Name => "Mark";

    public override int PickCard(Card[] deck)
    {
        return 0;
    }

}