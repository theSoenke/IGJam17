using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
    public MapController mapController;
    public PlayerController player;
    public EnemySpawn enemySpawn;
    public ItemSpawn itemSpawn;

	void Awake() {
		if (Instance != null) {
			throw new MissingReferenceException ();
		}
		Instance = this;
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    void Start ()
	{
        enemySpawn.Spawn(mapController);
        itemSpawn.Spawn(mapController);
    }
}
