using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Range(0, 1)]
    public float spawnProbability = 0.2f;
    public Enemy[] enemies;
    public Vector2 houseTopLeft;
    public Vector2 houseBottomRight;
    public int maxSpawnRate = 50;

    private float[] spawnWeights;
    private float spawnedLastMinute;

    [System.Serializable]
    public struct Enemy
    {
        public GameObject prefab;
        [Range(0, 1)]
        public float probability;
    }

    void Awake()
    {
        spawnWeights = NormalizedSpawnWeight();
    }

    void Update()
    {
        if (spawnedLastMinute > 0)
        {
            spawnedLastMinute -= Time.deltaTime / 60;
        }
    }

    public void InitialSpawn()
    {
        var mapController = GameManager.Instance.mapController;

        for (int y = 20; y < 50; y++)
        {
            for (int x = -20; x < 20; x++)
            {

                var tilePos = new Vector3Int(x, y, 0);

                if (mapController.ObstacleTilemap.GetTile(tilePos))
                    continue;

                //is tile in house?
                if (x >= houseTopLeft.x && x <= houseBottomRight.x
                && y >= houseBottomRight.y && y <= houseTopLeft.y)
                    continue;

                var tile = mapController.BackgroundTilemap.GetTile(tilePos);
                if (tile == null) continue;
                if (tile.name == "Floor")
                {
                    float random = Random.Range(0.0f, 1.0f);
                    if (random < spawnProbability)
                    {
                        var pos = new Vector3(x + 0.5f, y + 0.5f, -4.0f);
                        Spawn(pos);
                    }
                }
            }
        }
    }

    private void RandomSpawn()
    {
        var mapController = GameManager.Instance.mapController;
        for (int i = 0; i < 100; i++)
        {
            int randX = Random.Range(20, 49);
            int randY = Random.Range(-20, 19);
            var pos = new Vector3Int(randX, randY, 0);
            var tile = mapController.ObstacleTilemap.GetTile(pos);
            if (tile == null)
            {
                Spawn(new Vector3(randX, randY, -4f));
                break;
            }
        }
    }

    private void Spawn(Vector3 pos)
    {
        if (spawnedLastMinute >= maxSpawnRate) return;

        var item = GetSpawnEnemy(spawnWeights);
        var itemGameObject = Instantiate(item.prefab, pos, Quaternion.identity);
        itemGameObject.transform.SetParent(transform);
        spawnedLastMinute++;
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
