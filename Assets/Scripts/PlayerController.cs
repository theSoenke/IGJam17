using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region exposed fields
    [SerializeField]
    private List<Item> _inventory = new List<Item>();
    [SerializeField]
    private float _movementSpeed = 1.0f;
    [SerializeField]
    private string _inputAxisX = "Horizontal";
    [SerializeField]
    private string _inputAxisY = "Vertical";
    
    #endregion

    private Rigidbody2D _rigidbody;

    

    // Use this for initialization
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
        CheckInputs();
    }

    

    private void CheckInputs()
    {
        //TODO: implement button - item bindings
    }

    private void UpdatePosition()
    {
        var inputVector = new Vector3(Input.GetAxis(_inputAxisX), Input.GetAxis(_inputAxisY));
		if (inputVector.sqrMagnitude >= 1.0f) {
			inputVector = inputVector.normalized;
		}

        var speedVector = inputVector * _movementSpeed;

        var positionDelta = speedVector * Time.deltaTime;

        _rigidbody.velocity = speedVector;
    }

    public void PickUpItem(Item item)
    {
        _inventory.Add(item);
        item.IsPickedUp = true;
    }
}
