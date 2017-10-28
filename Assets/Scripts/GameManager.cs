using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    public EnemySpawn enemySpawn;
    public ItemSpawn itemSpawn;

    void Start ()
    {
        enemySpawn.Spawn(tilemap);
        itemSpawn.Spawn(tilemap);
    }
}
