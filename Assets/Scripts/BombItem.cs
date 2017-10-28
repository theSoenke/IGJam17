using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : Item {
	public GameObject bombPrefab;

	public bool isDeployed = false;
	public bool CanBePickedUp() {
		return !isDeployed;
	}

	public override void Use()
	{
		var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		Instantiate(bombPrefab, player.GetPosition(), Quaternion.identity);
	}
}