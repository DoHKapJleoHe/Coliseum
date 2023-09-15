using CardLib;
using Strategy;

namespace ColiseumTask
{
    class ColiseumTask
    {
        private const int IterCount = 1_000_000;
        public static void Main()
        {
            var deck = new Deck();
            
            //Console.WriteLine(deck.ToString());
            var s = new PickStrategy();
            Card[] elonDeck;
            Card[] markDeck;
            var winCount = 0;
            
            for (var i = 0; i < IterCount; i++)
            {
                elonDeck = deck.GetCardsArray().Take(deck.GetCardsArray().Length / 2).ToArray();
                markDeck = deck.GetCardsArray().Skip(deck.GetCardsArray().Length / 2).ToArray();

                if (elonDeck[s.Pick(markDeck)].GetColor() == markDeck[s.Pick(elonDeck)].GetColor())
                    winCount++;

                deck.ShuffleDeck();
            }

            float res = ((float)winCount / IterCount) * 100;
            Console.WriteLine(res);
        }
    }    
}
