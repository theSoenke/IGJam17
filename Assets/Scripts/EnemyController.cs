using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float switchDirectionProbability;
	private Rigidbody2D _rigidbody;
	Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		var random = Random.Range(0.0f, 1.0f);
		if (random > switchDirectionProbability) {
			int randomHeading = Random.Range (0, 4);
			float vert = 0.0f, horiz = 0.0f;
			switch (randomHeading) {
			case 0:
				vert = -1.0f;
				break;
			case 1:
				vert = 1.0f;
				break;
			case 2:
				horiz = -1.0f;
				break;
			case 3:
				horiz = 1.0f;
				break;
			}
			var randomDirection = new Vector3 (horiz, vert);
			velocity = randomDirection;
			var player = GameObject.FindGameObjectWithTag ("Player");
			var positionPlayer = player.GetComponent<Transform> ().position;
			var enemyToPlayer = positionPlayer - GetComponent<Transform> ().position;
			var enemyToPlayerDistance = enemyToPlayer.magnitude;

		}
		_rigidbody.velocity = 2.0f * velocity;
	}
}
