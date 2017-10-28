using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region exposed fields
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

        //transform.position += positionDelta;

    }
}
