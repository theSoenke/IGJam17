using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    public Tilemap tilemap;
    public float spawnProbability;
    public GameObject[] enemies;

    public void InitialSpawn ()
	{
        var gridSize = tilemap.size;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                var tilePos = new Vector3Int(x, y, 0);
                var tile = tilemap.GetTile(tilePos);
                if (tile == null) continue;
                if (tile.name == "Floor")
                {
					float random = Random.Range(0.0f, 1.0f);
                    if (random < spawnProbability)
                    {
                        var pos = new Vector3(x+0.5f,y-0.5f, -4.0f);
                        var enemy = Instantiate(enemies[0], pos, Quaternion.identity);
                        enemy.transform.SetParent(transform);
                    }
                }
            }
        }
    }
}
