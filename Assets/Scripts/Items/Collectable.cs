using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ItemType type;

    protected Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.PickUpItem(this);
            Destroy(gameObject);
        }
    }
}

public enum ItemType
{
    Bomb,
    Wall,
    Gun,
    Mine
}