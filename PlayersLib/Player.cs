using CardLib;

namespace AbstractPlayer;

public abstract class Player
{
    public abstract string Name { get; }
    public abstract int PickCard(Card[] deck);
}