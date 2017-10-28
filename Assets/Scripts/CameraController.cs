using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField, Range(0.01f, 1.0f)]
    private float _panSpeed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var vectorToPlayer = _player.transform.position - transform.position;

        var positionDelta = vectorToPlayer * _panSpeed * Time.deltaTime;
        positionDelta.Set(positionDelta.x, 
                          positionDelta.y, 
                          0);

        transform.position += positionDelta;
    }
}
