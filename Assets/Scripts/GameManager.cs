using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;
	
    public Tilemap tilemap;
    public EnemySpawn enemySpawn;
    public ItemSpawn itemSpawn;

	void Awake() {
		if (Instance != null) {
			throw new MissingReferenceException ();
		}
		Instance = this;
	}

    void Start ()
	{
        enemySpawn.Spawn(tilemap);
        itemSpawn.Spawn(tilemap);
    }
}
