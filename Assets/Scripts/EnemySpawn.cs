using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    [Range(0,1)]
    public float spawnProbability = 0.2f;
    public Enemy[] enemies;

    private float[] weights;

    [System.Serializable]
    public struct Enemy
    {
        public GameObject prefab;
        [Range(0, 1)]
        public float probability;
    }

    void Awake()
    {
        weights = NormalizedSpawnWeight();
    }

    public void Spawn (Tilemap tilemap)
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
                        var enemy = GetSpawnEnemy(weights);
                        var enemyGameObject = Instantiate(enemy.prefab, pos, Quaternion.identity);
                        enemyGameObject.transform.SetParent(transform);
                    }
                }
            }
        }
    }

    private Enemy GetSpawnEnemy(float[] weights)
    {
        float rand = Random.Range(0, 1f);
        var enemy = new Enemy();
        double cumulative = 0.0;
        for (int i = 0; i < enemies.Length; i++)
        {
            cumulative += weights[i];
            if (rand < cumulative)
            {
                enemy = enemies[i];
            }
        }

        return enemy;
    }

    private float[] NormalizedSpawnWeight()
    {
        var weights = new float[enemies.Length];
        var weightSum = 0f;
        foreach (Enemy enemy in enemies)
        {
            weightSum += enemy.probability;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            weights[i] = enemies[i].probability / weightSum;
        }

        return weights;
    }
}
