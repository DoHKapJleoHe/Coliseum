using AbstractPlayer;
using CardLib;

namespace ElonLib;

public class Elon : Player
{
    public override string Name => "Elon";

    public override int PickCard(Card[] deck)
    {
        return 0;
    }

}