using CardLib;

namespace ColiseumWebApp;

public class PickCard
{
    public List<CardDto> deckDto { get; set; }
    public int experimentId { get; set; }

    public Card[] ToHalfDeck()
    {
        Card[] deck = new Card[16];
        for (int i = 0; i < deckDto.Count; i++)
        {
            var color = deckDto[i].Color switch
            {
                Color.Red => Color.Red,
                Color.Black => Color.Black,
                _ => throw new ArgumentOutOfRangeException()
            };

            deck[i] = new Card(deckDto[i].Number, color);
        }

        return deck;
    }
}