using System;
using System.Collections;

public class Square
{
    public bool visited {get; set;}
    public bool start;
    public bool exit;
    public bool hasNorth {get; set;}
    public bool hasSouth {get; set;}
    public bool hasWest {get; set;}
    public bool hasEast { get; set; }
    private int row;
    private int col;
    private int wallToDestroy;
	public Square(int r, int c)
	{
        row = r;
        col = c;
        hasNorth = true;
        hasSouth = true;
        hasWest = true;
        hasEast = true;
	}
    public int getRow() { return row; }
    public int getCol() { return col; }
    public int getWallToDestroy() { return wallToDestroy;  }
    public void setWallToDestroy(int w2d) { wallToDestroy = w2d; }
}
