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

    public Color GetColor()
    {
        return _color;
    }

    public override string ToString()
    {
        var s = "(" + _number + " " + _color + ")";
        return s;
    }
}