using System;
using System.Collections;

public class Square
{
    public bool visited {get; set;}
    public bool hasNorth {get; set;}
    public bool hasSouth {get; set;}
    public bool hasWest {get; set;}
    public bool hasEast { get; set; }
    public int row { get; }
    public int col { get; }
	public Square(int r, int c)
	{
        row = r;
        col = c;
        hasNorth = true;
        hasSouth = true;
        hasWest = true;
        hasEast = true;
	}
}
