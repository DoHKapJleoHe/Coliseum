using CardLib;
using Strategy;

namespace ElonLib;

public class Elon : AbstractPlayer.Player
{
    public override string Name => "Elon";

    public override int PickCard(Card[] deck)
    {
        return 0;
    }

}