using CardLib;

namespace DeckShuffler;

public interface IDeckShuffler
{
    public void Shuffle(ref Card[] deck);
}

public class Shuffler : IDeckShuffler
{
    public void Shuffle(ref Card[] deck)
    {
        var rnd = new Random();
        var num = deck.Length;

        while (num > 1)
        {
            var k = rnd.Next(num--);
            (deck[num], deck[k]) = (deck[k], deck[num]);
        }
    }
}