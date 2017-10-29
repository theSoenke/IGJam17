using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
    public MapController mapController;
    public PlayerController player;
    public EnemySpawn enemySpawn;
    public ItemSpawn itemSpawn;

    private List<EnemyController> _enemies = new List<EnemyController>();

	void Awake() {
		if (Instance != null) {
			throw new MissingReferenceException ();
		}
		Instance = this;
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    void Start ()
	{
        enemySpawn.InitialSpawn();
        itemSpawn.InitialSpawn();
    }

    public void RegisterEnemySpawn(EnemyController enemy)
    {
        _enemies.Add(enemy);
    }

    public void RegisterEnemyDeath(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }

    public List<EnemyController> GetEnemiesAt(Vector3 pos, float radius)
    {
        var res = new List<EnemyController>();
        foreach(var enemy in _enemies)
        {
            if(Vector2.Distance(pos, enemy.transform.position) < radius)
            {
                res.Add(enemy);
            }
        }
        return res;
    }
}
