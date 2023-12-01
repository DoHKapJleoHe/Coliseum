namespace CardLib;

public class Card
{
    public int Number { get; set; }
    public Color Color { get; set; }

    public Card(int num, Color col)
    {
        Color = col;
        Number = num;
    }

    public Card() {}

    public override string ToString()
    {
        var s = "(" + Number + " " + Color + ")";
        return s;
    }
}