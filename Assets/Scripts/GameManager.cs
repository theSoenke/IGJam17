using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;
	
    public MapController mapController;
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

        enemySpawn.Spawn(mapController);
        itemSpawn.Spawn(mapController);
    }
}
