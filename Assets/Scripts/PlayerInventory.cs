using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image bombBackground;
    public Image candyBackground;
    public Image wallsBackground;

    public Color empty;
    public Color filled;

    public int bombs;
    private int candys;
    private int walls;

    void Update ()
	{
	    bombBackground.color = bombs <= 0 ? empty : filled;
	    candyBackground.color = candys <= 0 ? empty : filled;
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
                candys++;
                break;
        }
    }
}
