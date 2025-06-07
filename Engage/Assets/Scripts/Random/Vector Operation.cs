//Uz pomoc metoda iz ove klase mozes da invertujes Vector2

using UnityEngine;

public class VectorInverter
{
    static public Vector2 InvertUpDown( Vector2 value)
    {
        Vector2 output = new Vector2(value.x, -value.y);
        return output;
    }
    static public Vector2 InvertLeftRight( Vector2 value)
    {
        Vector2 output = new Vector2(-value.x, value.y);
        return output;
    }
    static public Vector2 InvertAll( Vector2 value)
    {
        Vector2 output = InvertUpDown(value);
        output = InvertUpDown(output);
        return output;
    }
}
