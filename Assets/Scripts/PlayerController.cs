using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region exposed fields
    [SerializeField]
    private Item[] _inventory;
    public string[] _inputUseItem;
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
        _inventory = new Item[4];
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
        for(var i = 0; i < 4; i++)
        {
            var inputName = _inputUseItem[i];
            var item = _inventory[i];
            if(Input.GetButtonDown(inputName) && item != null)
            {
                item.Use();
            }
        }
    }

    private void UpdatePosition()
    {
        var inputVector = new Vector3(Input.GetAxis(_inputAxisX), Input.GetAxis(_inputAxisY));
        if (inputVector.sqrMagnitude >= 1.0f) {
            inputVector = inputVector.normalized;
        }

        var speedVector = inputVector * _movementSpeed;
        _rigidbody.velocity = speedVector;
    }

    public void PickUpItem(Item item)
    {
        _inventory[(int)item.Type] = item;
        item.IsPickedUp = true;
    }
}
