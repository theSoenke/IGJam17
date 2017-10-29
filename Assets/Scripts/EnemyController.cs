using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public float switchDirectionProbability;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
	private float destroyCountdown;
	public float destroyTime = 2.0f;
    private bool _isDying = false;

    private const string ANIM_MOVE_LEFT = "MoveLeft";
    private const string ANIM_MOVE_RIGHT = "MoveRight";
    private const string ANIM_MOVE_UP = "MoveTop";
    private const string ANIM_MOVE_DOWN = "MoveDown";
    private const string ANIM_MOVE_DIE = "Die";
    private const string ANIM_MOVEMENT_SPEED = "MovementSpeed";

	public TileBase targetBlock = null;

    // Use this for initialization
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void DoMovementStuff()
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
            var enemyToPlayer = positionPlayer - transform.position;
			if (Random.Range (0.0f, 1.0f) > 0.7) {
				if (Mathf.Abs (enemyToPlayer.x) < Mathf.Abs (enemyToPlayer.y)) {
					velocity.y = enemyToPlayer.y > 0.0f ? 1.0f : -1.0f;
					velocity.x = 0;
				} else {
					velocity.x = enemyToPlayer.x > 0.0f ? 1.0f : -1.0f;
					velocity.y = 0;
				}
			}
            var enemyToPlayerDistance = enemyToPlayer.magnitude;

        }
        _rigidbody.velocity = 1.5f * velocity;

		var mapController = GameManager.Instance.mapController;
		var targetPosition = new Vector3Int ((int)Mathf.Floor (transform.position.x), (int)Mathf.Floor (transform.position.y), 0);
		targetPosition.x += velocity.x > 0.1 ? 1 : velocity.x < -0.1 ? -1 : 0;
		targetPosition.y += velocity.y > 0.1 ? 1 : velocity.y < -0.1 ? -1 : 0;

		var block = mapController.ObstacleTilemap.GetTile (targetPosition);
		if (block == targetBlock && block != null && block.name == "Undestructible") {
			destroyCountdown += Time.deltaTime;
			print (destroyCountdown); 
			if (destroyCountdown > destroyTime) {
				mapController.DestroyTile (targetPosition);
				targetBlock = null;
			}
		}
		if (block != targetBlock) {
			targetBlock = block;
			destroyCountdown = 0.0f;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDying)
            DoMovementStuff();
        else
            _rigidbody.velocity = Vector2.zero;
    }

//    private void OnDrawGizmos()
//    {
//        var dir = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0);
//        Gizmos.DrawLine(transform.position, transform.position + dir);
//    }

	public void Die() {
        if (_isDying)
            return;
        _isDying = true;
	    GameManager.Instance.RegisterEnemyDeath(this);
        _animator.SetTrigger(ANIM_MOVE_DIE);
        Destroy (gameObject,2);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Die();
        }

		var wall = other.gameObject.GetComponent<MapController> ();
		if (wall != null) {
			Debug.Log ("hit wall");
		}
    }
}
