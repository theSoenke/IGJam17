using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image bombBackground;
    public Image candyBackground;

    public Color empty;
    public Color filled;

    private int bombs;
    private int candys;

	void Update ()
	{
	    bombBackground.color = bombs <= 0 ? empty : filled;
	    candyBackground.color = candys <= 0 ? empty : filled;
    }
}
