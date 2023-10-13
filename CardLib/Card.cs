namespace CardLib;

public class Card
{
    private int _number;
    private Color _color;
    
    public Card(int num, Color col)
    {
        _color = col;
        _number = num;
    }

    public Card() {}
    
    public Color GetColor()
    {
        return _color;
    }

    public int GetNumber()
    {
        return _number;
    }

    public override string ToString()
    {
        var s = "(" + _number + " " + _color + ")";
        return s;
    }
}