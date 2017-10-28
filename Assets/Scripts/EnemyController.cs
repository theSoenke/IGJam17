using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
	public float health = 100.0f;
    public float switchDirectionProbability;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    private const string ANIM_MOVE_LEFT = "MoveLeft";
    private const string ANIM_MOVE_RIGHT = "MoveRight";
    private const string ANIM_MOVE_UP = "MoveTop";
    private const string ANIM_MOVE_DOWN = "MoveDown";
    private const string ANIM_MOVEMENT_SPEED = "MovementSpeed";

    // Use this for initialization
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        var random = Random.Range(0.0f, 1.0f);
        if (random > switchDirectionProbability)
        {
            int randomHeading = Random.Range(0, 4);
            float vert = 0.0f, horiz = 0.0f;
            switch (randomHeading)
            {
                case 0:
                    vert = -1.0f;
                    _animator.SetTrigger(ANIM_MOVE_DOWN);
                    break;
                case 1:
                    vert = 1.0f;
                    _animator.SetTrigger(ANIM_MOVE_UP);
                    break;
                case 2:
                    horiz = -1.0f;
                    _animator.SetTrigger(ANIM_MOVE_LEFT);
                    break;
                case 3:
                    horiz = 1.0f;
                    _animator.SetTrigger(ANIM_MOVE_RIGHT);
                    break;
            }
            var randomDirection = new Vector3(horiz, vert);
            velocity = randomDirection;
            var player = GameObject.FindGameObjectWithTag("Player");
            var positionPlayer = player.GetComponent<Transform>().position;
            var enemyToPlayer = positionPlayer - GetComponent<Transform>().position;
            var enemyToPlayerDistance = enemyToPlayer.magnitude;

        }
        _rigidbody.velocity = 0.5f * velocity;
    }

//    private void OnDrawGizmos()
//    {
//        var dir = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0);
//        Gizmos.DrawLine(transform.position, transform.position + dir);
//    }

	public void Die() {
		Destroy (gameObject);
	}

	public void InflictDamage() {
		health -= 50.0f;

		if (health < 0.0f) {
			Die ();
		}
	}
}
