using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image bombBackground;
    public Image candyBackground;
    public Image wallsBackground;

    public Color empty;
    public Color filled;

    private int bombs = 10;
    private int candy =  10;
    private int walls = 10;
    private int mines = 2;

    public int Bombs
    {
        get { return bombs; }
        set { bombs = value; }
    }

    public int Candy
    {
        get { return candy; }
        set { candy = value; }
    }

    public int Walls
    {
        get { return walls; }
        set { walls = value; }
    }

    void Update ()
	{
	    bombBackground.color = bombs <= 0 ? empty : filled;
	    candyBackground.color = candy <= 0 ? empty : filled;
	    wallsBackground.color = walls <= 0 ? empty : filled;
    }

    public void Add(Collectable collectable)
    {
        switch (collectable.type)
        {
            case ItemType.Bomb:
                bombs++;
                break;
            case ItemType.Gun:
                candy++;
                break;
            case ItemType.Wall:
                walls++;
                break;
            case ItemType.Mine:
                mines++;
                break;
        }
    }
}
