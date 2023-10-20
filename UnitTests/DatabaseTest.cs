using CardLib;
using DbLib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests;

[TestFixture]
public class DatabaseTest
{
    private DeckDbContext _testDbContext;
    private SqliteConnection _connection;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        
        var options = new DbContextOptionsBuilder<DeckDbContext>()
            .UseSqlite(_connection)
            .Options;
        
        _testDbContext = new DeckDbContext(options);
        _testDbContext.Database.EnsureCreated();

        //_readerWriterMock = new Mock<ReaderWriter>();
    }
    
    [TearDown]
    public void TearDown()
    {
        _connection.Close();
    }
    
    [Test]
    public void WriteToDbTest()
    {
        Deck testDeck = new Deck(TestDecks.GetDeck(0));

        Mock<IReaderWriter> readerWriterMock = new Mock<IReaderWriter>();
        readerWriterMock.Setup(rw => rw.WriteToDatabase(It.IsAny<Deck>())).Verifiable();
        
        readerWriterMock.Object.WriteToDatabase(testDeck);
        readerWriterMock.Verify(rw => rw.WriteToDatabase(testDeck), Times.Once);
        
        List<Deck> expDecks = new List<Deck> {
            new Deck(TestDecks.GetDeck(0))
        };
        readerWriterMock.Setup(rw => rw.ReadFromDatabase()).Returns(expDecks);

        List<Deck> actDecks = readerWriterMock.Object.ReadFromDatabase();
        
        CollectionAssert.AreEqual(expDecks, actDecks);
    }
}