using CardLib;
using ElonLib;
using MarkLib;

namespace UnitTests;

[TestFixture]
public class StrategyTest
{
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void TestElonStrategy(int num)
    {
        var elon = new Elon();
        var deck = TestDecks.GetHalfDeck(num);
        
        Assert.That(elon.PickCard(deck), Is.EqualTo(0));
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void TestMarkStrategy(int num)
    {
        var mark = new Mark();
        var deck = TestDecks.GetHalfDeck(num);
        
        Assert.That(mark.PickCard(deck), Is.EqualTo(0));
    }
}