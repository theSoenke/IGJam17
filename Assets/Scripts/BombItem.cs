using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BombItem : Item {
	public GameObject bombPrefab;
	public bool CanBePickedUp() {
		
	}

	public override void Use()
	{
		Instantiate(bombPrefab, _player.GetPosition(), Quaternion.identity);
	}
}