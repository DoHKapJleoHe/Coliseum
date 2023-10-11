using AbstractPlayer;
using CardLib;

using DeckShuffler;
using Microsoft.Extensions.Logging;
using Moq;
using SandboxLib;

namespace UnitTests;

[TestFixture]
public class ExperimentsTest
{
    private Mock<IDeckShuffler> _deckShufflerMoq;
    private Mock<Player> _elonMock;
    private Mock<Player> _markMock;
    
    [SetUp]
    public void SetUp()
    {
        _deckShufflerMoq = new Mock<IDeckShuffler>();
        _elonMock = new Mock<Player>();
        _markMock = new Mock<Player>();

        var deck = new Deck().GetCardsArray();
        var elonHalf = deck.Take(deck.Length / 2).ToArray();
        var markHalf = deck.Skip(deck.Length / 2).ToArray();
        
        _deckShufflerMoq.Setup(shuffler => shuffler.Shuffle(ref deck));
        _elonMock.Setup(elon => elon.PickCard(elonHalf)).Returns(0);
        _markMock.Setup(mark => mark.PickCard(markHalf)).Returns(0);
    }

    [Test]
    public void TestShuffle()
    {
        var sandbox = new Sandbox(
            _deckShufflerMoq.Object,
            new [] {_markMock.Object, _elonMock.Object},
            Mock.Of<ILogger<Sandbox>>()
            );

        var deck = new Deck();
        sandbox.DoOneExperiment(deck);

        var cards = deck.GetCardsArray();
        _deckShufflerMoq.Verify(shuffler => shuffler.Shuffle(ref cards), Times.Once);
    }

    [TestCase(0)]
    public void TestExperimentResultCorrectness(int num)
    {
        var sandbox = new Sandbox(
            _deckShufflerMoq.Object,
            new [] {_markMock.Object, _elonMock.Object},
            Mock.Of<ILogger<Sandbox>>()
            );

        var deck = TestDecks.GetDeck(num);
        var result = sandbox.DoOneExperimentNoShuffle(new Deck(deck));
        
        
        Assert.That(result, Is.EqualTo(deck[0].GetColor() == deck[18].GetColor()));
    }
}