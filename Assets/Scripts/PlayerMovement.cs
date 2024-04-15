using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private float moveSpeed = 5f;
    private float MAX_VELOCITY = 15f;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Stop();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.velocity = _playerInput.move * moveSpeed;

        if(_rb.velocity.x > MAX_VELOCITY) _rb.velocity = new Vector2(MAX_VELOCITY, _rb.velocity.y);
        if(_rb.velocity.x < -MAX_VELOCITY) _rb.velocity = new Vector2(-MAX_VELOCITY, _rb.velocity.y);
        if(_rb.velocity.y > MAX_VELOCITY) _rb.velocity = new Vector2(_rb.velocity.x, MAX_VELOCITY);
        if(_rb.velocity.y < -MAX_VELOCITY) _rb.velocity = new Vector2(_rb.velocity.x, -MAX_VELOCITY);

        if (_playerInput.move == Vector2.zero) _rb.velocity = Vector2.zero;
        if (_playerInput.move.x == 0) _rb.velocity = new Vector2(0, _rb.velocity.y);
        if (_playerInput.move.y == 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }

    private void Stop()
    {
        if (_playerInput.isStopping && moveSpeed != 0)
        {
            moveSpeed = 0;
            _rb.velocity = Vector2.zero;
        }
        else if (_playerInput.isStopping == false && moveSpeed != 1f)
        {
            moveSpeed = 5f;
        }
    }

}
