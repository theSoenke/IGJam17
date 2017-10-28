using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawn : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject[] enemies;
    public float spawnProbability;
    public BoundsInt area;

    void Start ()
	{
	    //var gridSize = tilemap.size;

	    TileBase[] tileArray = tilemap.GetTilesBlock(area);
	    for (int index = 0; index < tileArray.Length; index++)
	    {
	        print(tileArray[index]);
	    }

	    return;

     //   for (int y = 0; y < gridSize.y; y++)
	    //{
	    //    for (int x = 0; x < gridSize.x; x++)
	    //    {
     //           var tilePos = new Vector3Int(x, y, 0);
     //           var tile = tilemap.GetTile(tilePos);
     //           if(tile != null)
	    //        {
	    //            print(tilePos);
     //               if (tile.name == "Floor")
	    //            {
	    //                Random.seed = 0;
	    //                var random = Random.Range(0, 1);
	    //                if (random < spawnProbability)
	    //                {
                           
	    //                    var enemy = Instantiate(enemies[0], tilePos, Quaternion.identity);
     //                       enemy.transform.SetParent(transform);
	    //                }
	    //            }
     //           }
     //       }
     //   }

    }

    void Update () {
		
	}
}
