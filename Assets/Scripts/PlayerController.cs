using System;
using Boo.Lang;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region exposed fields
    public string[] _inputUseItem;
    [SerializeField]
    private float _movementSpeed = 1.0f;
    [SerializeField]
    private string _inputAxisX = "Horizontal";
    [SerializeField]
    private string _inputAxisY = "Vertical";
    [SerializeField]
    private GameObject _prefabCandy;
    [SerializeField]
    private GameObject _prefabBomb;
    #endregion

    private Rigidbody2D _rigidbody;
    private PlayerInventory _inventory;
    private Vector3 nextWallPos;
    private bool buildWall;
    private Vector3 prevInputVector;
    private Animator animator;

    private const string ANIM_MOVE_LEFT = "WalkLeft";
    private const string ANIM_MOVE_RIGHT = "WalkRight";
    private const string ANIM_MOVE_UP = "WalkUp";
    private const string ANIM_MOVE_DOWN = "WalkDown";


    // Use this for initialization
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inventory = GetComponent<PlayerInventory>();
		prevInputVector = new Vector3 (1.0f, 0.0f, 0.0f);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
        CheckInputs();

        if (buildWall && Vector3Int.FloorToInt(transform.position) != nextWallPos)
        {
            GameManager.Instance.mapController.BuildWall(nextWallPos);
            buildWall = false;
        }
    }

    private void SpawnBomb()
    {
        var pos = new Vector3(
                        Mathf.Floor(transform.position.x) + 0.5f,
                        Mathf.Floor(transform.position.y) + 0.5f, -7.0f);
        Instantiate(_prefabBomb, pos, Quaternion.identity);
        _inventory.Bombs--;
    }

    private void CheckInputs()
    {
		for(var i = 0; i < 4; i++)
		{
			var inputName = _inputUseItem[i];
			if (Input.GetButtonDown(inputName))
			{
			    if (i == 0 && _inventory.Bombs > 0)
			    {
                    SpawnBomb();
			    }

				if (i == 1 && _inventory.Candy > 0) {
                    FireCandyGun();
				}

			    if (i == 2 && _inventory.Walls > 0)
			    {
			        nextWallPos = Vector3Int.FloorToInt(transform.position);
                    buildWall = true;
			        _inventory.Walls--;
			    }
			}
		}
    }

    private void FireCandyGun()
    {
        var pos = new Vector3(
                        Mathf.Floor(transform.position.x) + 0.5f,
                        Mathf.Floor(transform.position.y) + 0.5f, -7.0f);
        var candy = Instantiate(_prefabCandy, pos, Quaternion.identity);
        candy.GetComponent<Rigidbody2D>().AddForce(500.0f * prevInputVector);
        _inventory.Candy--;
    }

    private void UpdatePosition()
    {
        var inputVector = new Vector3(Input.GetAxis(_inputAxisX), Input.GetAxis(_inputAxisY));
        if (inputVector.sqrMagnitude >= 1.0f) {
            inputVector = inputVector.normalized;
        }
		if (inputVector.sqrMagnitude >= 0.0001) {
			prevInputVector = inputVector.normalized;
		}

        var speedVector = inputVector * _movementSpeed;
        _rigidbody.velocity = speedVector;

        UpdateAnimationStateMachine(inputVector);
    }

    private void UpdateAnimationStateMachine(Vector3 movementDirection)
    {
        if (movementDirection.magnitude <= 0.01f)
            return;

        //check if movement is horizontal ov vertical

        if(Mathf.Abs(movementDirection.x) >= Mathf.Abs(movementDirection.y))
        {
            //horizontal
            if (movementDirection.x <= 0)
                animator.SetTrigger(ANIM_MOVE_LEFT);
            else
                animator.SetTrigger(ANIM_MOVE_RIGHT);
        }
        else
        {
            //vertical
            if (movementDirection.y <= 0)
                animator.SetTrigger(ANIM_MOVE_DOWN);
            else
                animator.SetTrigger(ANIM_MOVE_UP);
        }
    }

    public void PickUpItem(Collectable collectable)
    {
        _inventory.Add(collectable);
	}

    public void Die()
    {
        print("I DIED!!!");
    }
}