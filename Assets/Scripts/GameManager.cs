using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawn enemySpawn;

    void Start ()
    {
        enemySpawn.InitialSpawn();
    }
}
