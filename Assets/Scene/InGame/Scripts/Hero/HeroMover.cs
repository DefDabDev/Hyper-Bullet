using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMover : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 1f;

    Rigidbody2D _rigidBody;

    Vector3 _moveVector = Vector3.zero;
    Vector3 _rotateVector = Vector3.zero;
    const float correction = 90f * Mathf.Deg2Rad;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        getKey();
    }

    void FixedUpdate()
    {
        movement();
        rotation();
    }

    void getKey()
    {
        _moveVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        _rotateVector = new Vector3(CnInputManager.GetAxis("RotateX"), CnInputManager.GetAxis("RotateY"));
    }

    void movement()
    {
        _rigidBody.velocity = _moveVector * _moveSpeed;
    }

    void rotation()
    {
        if (_rotateVector.Equals(Vector3.zero))
            return;

        float value = (Mathf.Atan2(_rotateVector.y, _rotateVector.x) - correction) * Mathf.Rad2Deg;
        _rigidBody.rotation = value;
    }
}
