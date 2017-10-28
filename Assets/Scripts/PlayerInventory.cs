using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image bombBackground;
    public Image candyBackground;

    public Color empty;
    public Color filled;

    public Item bomb;
    public Item candy;

    private int bombs;
    private int candys;

	void Update ()
	{
	    bombBackground.color = bombs <= 0 ? empty : filled;
	    candyBackground.color = candys <= 0 ? empty : filled;
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
