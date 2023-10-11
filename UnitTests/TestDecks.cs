using CardLib;

namespace UnitTests;

public class TestDecks
{
    private static readonly Card[] Deck1 = {
        new Card(1, Color.Black), new Card(2, Color.Black), new Card(3, Color.Black),
        new Card(4, Color.Black), new Card(5, Color.Black), new Card(6, Color.Black),
        new Card(7, Color.Black), new Card(8, Color.Black), new Card(9, Color.Black),
        new Card(10, Color.Red), new Card(11, Color.Red), new Card(12, Color.Red),
        new Card(13, Color.Red), new Card(14, Color.Red), new Card(15, Color.Red),
        new Card(16, Color.Red), new Card(17, Color.Red), new Card(18, Color.Red),
        new Card(19, Color.Black), new Card(20, Color.Black), new Card(21, Color.Black),
        new Card(22, Color.Black), new Card(23, Color.Black), new Card(24, Color.Black),
        new Card(25, Color.Black), new Card(26, Color.Black), new Card(27, Color.Black),
        new Card(28, Color.Red), new Card(29, Color.Red), new Card(30, Color.Red),
        new Card(31, Color.Red), new Card(32, Color.Red), new Card(33, Color.Red),
        new Card(34, Color.Red), new Card(35, Color.Red), new Card(36, Color.Red)
    };
    
    private static readonly Card[] HalfDeck1 = {
        new Card(1, Color.Black), new Card(2, Color.Black), new Card(3, Color.Black),
        new Card(4, Color.Black), new Card(5, Color.Black), new Card(6, Color.Black),
        new Card(7, Color.Black), new Card(8, Color.Black), new Card(9, Color.Black),
        new Card(10, Color.Red), new Card(11, Color.Red), new Card(12, Color.Red),
        new Card(13, Color.Red), new Card(14, Color.Red), new Card(15, Color.Red),
        new Card(16, Color.Red), new Card(17, Color.Red), new Card(18, Color.Red)
    };
    
    private static readonly Card[] HalfDeck2 = {
        new Card(13, Color.Red), new Card(14, Color.Red), new Card(15, Color.Red),
        new Card(16, Color.Red), new Card(17, Color.Red), new Card(18, Color.Red),
        new Card(10, Color.Red), new Card(11, Color.Red), new Card(12, Color.Red),
        new Card(1, Color.Black), new Card(2, Color.Black), new Card(3, Color.Black),
        new Card(4, Color.Black), new Card(5, Color.Black), new Card(6, Color.Black),
        new Card(7, Color.Black), new Card(8, Color.Black), new Card(9, Color.Black)
    };
    
    private static readonly Card[] HalfDeck3 = {
        new Card(10, Color.Red), new Card(3, Color.Black), new Card(14, Color.Red),
        new Card(7, Color.Black), new Card(16, Color.Red), new Card(2, Color.Black),
        new Card(12, Color.Red), new Card(5, Color.Black), new Card(17, Color.Red),
        new Card(9, Color.Black), new Card(1, Color.Black), new Card(18, Color.Red),
        new Card(13, Color.Red), new Card(6, Color.Black), new Card(11, Color.Red),
        new Card(4, Color.Black), new Card(8, Color.Black), new Card(15, Color.Red)
    };

    private static readonly List<Card[]> HalfDecks = new() { HalfDeck1, HalfDeck2, HalfDeck3};
    private static readonly List<Card[]> Decks = new() { Deck1 };

    public static Card[] GetDeck(int num)
    {
        return Decks[num];
    }

    public static Card[] GetHalfDeck(int num)
    {
        return HalfDecks[num];
    }
}