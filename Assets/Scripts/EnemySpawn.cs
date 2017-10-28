using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    public Tilemap tilemap;
    public float spawnProbability = 0.2f;
    public int seed;
    public Enemy[] enemies;

    [System.Serializable]
    public struct Enemy
    {
        public GameObject prefab;
        public float spawnProbability;
    }

    public void InitialSpawn ()
	{
	    Random.seed = seed;
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
                    var random = Random.Range(0, 1);
                    if (random < spawnProbability)
                    {
                        var pos = new Vector3(x+0.5f,y-0.5f,0);
                        var enemy = GetSpawnEnemy();
                        var enemyGameObject = Instantiate(enemy.prefab, pos, Quaternion.identity);
                        enemyGameObject.transform.SetParent(transform);
                    }
                }
            }
        }
    }

    private Enemy GetSpawnEnemy()
    {
        var weights = new float[enemies.Length];
        var weightSum = 0f;
        foreach (Enemy enemy in enemies)
        {
            weightSum += enemy.spawnProbability;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            weights[i] = enemies[i].spawnProbability / weightSum;
        }

        float rand = Random.Range(0, 1f);
        var result = new Enemy();
        double cumulative = 0.0;
        for (int i = 0; i < enemies.Length; i++)
        {
            cumulative += weights[i];
            if (rand < cumulative)
            {
                result = enemies[i];
            }
        }

        return result;
    }
}
