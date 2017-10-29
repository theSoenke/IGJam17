using Boo.Lang;
using UnityEngine;

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
    private Vector3 prevInputVector;


    // Use this for initialization
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inventory = GetComponent<PlayerInventory>();
		prevInputVector = new Vector3 (1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
        CheckInputs();

        if (Vector3Int.FloorToInt(transform.position) != nextWallPos)
        {
            GameManager.Instance.mapController.BuildWall(nextWallPos);
        }
    }

    private void CheckInputs()
    {
		//TODO: implement button - item bindings
		for(var i = 0; i < 4; i++)
		{
			var inputName = _inputUseItem[i];
			if (Input.GetButtonDown(inputName))
			{
			    if (i == 0 && _inventory.Bombs > 0)
			    {
			        var pos = new Vector3(
			            Mathf.Floor(transform.position.x) + 0.5f,
			            Mathf.Floor(transform.position.y) + 0.5f, -7.0f);
                    Instantiate(_prefabBomb, pos, Quaternion.identity);
			        _inventory.Bombs--;
			    }

				if (i == 1 && _inventory.Candy > 0) {
					var pos = new Vector3 (
						Mathf.Floor(transform.position.x) + 0.5f,
						Mathf.Floor(transform.position.y) + 0.5f, -7.0f);
					var candy = Instantiate (_prefabCandy, pos, Quaternion.identity);
					candy.GetComponent<Rigidbody2D> ().AddForce (500.0f * prevInputVector);
				    _inventory.Candy--;
				}

			    if (i == 2 && _inventory.Walls > 0)
			    {
			        nextWallPos = Vector3Int.FloorToInt(transform.position);
			        _inventory.Walls--;
			    }
			}
		}
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