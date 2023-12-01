using CardLib;

namespace UnitTests;

[TestFixture]
public class CardsTest
{
    [Test]
    public void TestColorsNumber()
    {
        var deck = new Deck();
        var cards = deck.GetCardsArray();
        
        int blackNum = 0, 
            redNum = 0;

        foreach (var card in cards)
        {
            if (card.Color == Color.Red)
                redNum++;
            if (card.Color == Color.Black)
                blackNum++;
        }

        Assert.Multiple(() =>
        {
            Assert.That(blackNum, Is.EqualTo(18));
            Assert.That(redNum, Is.EqualTo(18));
        });
    }
}