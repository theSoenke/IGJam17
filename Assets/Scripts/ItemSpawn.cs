using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [Range(0, 1)]
    public float spawnProbability = 0.2f;
    public Item[] items;
    [Range(0,100)]
    public int maxSpawnRate = 50;

    private float[] spawnWeights;
    private float spawnedLastMinute;

    [System.Serializable]
    public struct Item
    {
        public Collectable prefab;
        [Range(0,1)]
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

    public void Spawn(Vector3 pos)
    {
        if (spawnedLastMinute > maxSpawnRate) return;
        var item = GetSpawnItem(spawnWeights);
        var itemGameObject = Instantiate(item.prefab, pos, Quaternion.identity);
        itemGameObject.transform.SetParent(transform);
        spawnedLastMinute++;
    }

    public void InitialSpawn()
    {
        var mapController = GameManager.Instance.mapController;
        var gridSize = mapController.BackgroundTilemap.size;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = -20; x < gridSize.x; x++)
            {
                var tilePos = new Vector3Int(x, y, 0);

                if (mapController.ObstacleTilemap.GetTile(tilePos))
                    continue;

                var tile = mapController.BackgroundTilemap.GetTile(tilePos);
                if (tile == null) continue;
                if (tile.name == "Floor")
                {
                    float random = Random.Range(0, 1f);
                    if (random < spawnProbability)
                    {
                        var pos = new Vector3(x + 0.5f, y + 0.5f, -1);
                        Spawn(pos);
                    }
                }
            }
        }
    }

    private Item GetSpawnItem(float[] weights)
    {
        float rand = Random.Range(0, 1f);
        var item = new Item();
        double cumulative = 0.0;
        for (int i = 0; i < items.Length; i++)
        {
            cumulative += weights[i];
            if (rand < cumulative)
            {
                item = items[i];
            }
        }

        return item;
    }

    private float[] NormalizedSpawnWeight()
    {
        var weights = new float[items.Length];
        var weightSum = 0f;
        foreach (Item item in items)
        {
            weightSum += item.probability;
        }

        for (int i = 0; i < items.Length; i++)
        {
            weights[i] = items[i].probability / weightSum;
        }

        return weights;
    }
}
