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


        if (spawnedLastMinute < maxSpawnRate)
        {
            RandomSpawn();
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
        for (int i = 0; i < maxSpawnRate; i++)
        {
            RandomSpawn();
        }
    }

    private bool IsPlaceablePos(Vector3Int pos)
    {
        var mapController = GameManager.Instance.mapController;
        if (mapController.ObstacleTilemap.GetTile(pos))
        {
            return false;
        }

        return true;
    }

    private void RandomSpawn()
    {
        var mapController = GameManager.Instance.mapController;
        for (int i = 0; i < 100; i++)
        {
            int x = Random.Range(-17, 15);
            int y = Random.Range(22, 49);
            var tilePos = new Vector3Int(x, y, 0);

            if (!IsPlaceablePos(tilePos)) return;
            var tile = mapController.ObstacleTilemap.GetTile(tilePos);
            if (tile == null)
            {
                Spawn(new Vector3(x + 0.5f, y + 0.5f, -1));
                break;
            }
        }
    }

    private Item GetSpawnItem(float[] weights)
    {
        float rand = Random.Range(0, 1f);
        if (rand < 0.5f)
        {
            return items[2];
        } else if(rand < 0.8f)
        {
            return items[0];
        }
        else
        {
            return items[1];
        }

        // FIXME broken with multiple items
        //float rand = Random.Range(0, 1f);
        //var item = new Item();
        //double cumulative = 0.0;
        //for (int i = 0; i < items.Length; i++)
        //{
        //    cumulative += weights[i];
        //    if (rand < cumulative)
        //    {
        //        item = items[i];
        //    }
        //}

        //return item;
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
