using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCandy : MonoBehaviour {
	private int zombieHits = 0;
	public int maxZombieHits = 3;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var player = collision.gameObject.GetComponent<PlayerController>();
		if (player != null)
		{
			return;
		}
		var enemyController = collision.gameObject.GetComponent<EnemyController>();
		if (enemyController != null) {
			enemyController.InflictDamage();
			zombieHits ++;
			if (zombieHits >= maxZombieHits) {
				Destroy (gameObject);
			}
		}
//		var mapController = collision.gameObject.GetComponent<MapController>();
//		if (mapController != null) {
//
//		}
	}
}
