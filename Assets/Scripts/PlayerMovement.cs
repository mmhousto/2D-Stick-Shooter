using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // Adjust this value to control the jump force
    public float jumpDuration = 1f; // Adjust this value to control how long the jump lasts
    private bool isJumping = false;
    private float jumpStartTime;
    private float initialZPosition;
    private GetPlayerInput _playerInput;
    private Rigidbody2D _rb;
    private float moveSpeed = 5f;
    private float MAX_VELOCITY = 15f;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<GetPlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        initialZPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Stop();

        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_playerInput.isAutoMoving == false)
        {
            Time.timeScale = 1.0f;
            _rb.velocity = _playerInput.move * moveSpeed;

            if (_rb.velocity.x > MAX_VELOCITY) _rb.velocity = new Vector2(MAX_VELOCITY, _rb.velocity.y);
            if (_rb.velocity.x < -MAX_VELOCITY) _rb.velocity = new Vector2(-MAX_VELOCITY, _rb.velocity.y);
            if (_rb.velocity.y > MAX_VELOCITY) _rb.velocity = new Vector2(_rb.velocity.x, MAX_VELOCITY);
            if (_rb.velocity.y < -MAX_VELOCITY) _rb.velocity = new Vector2(_rb.velocity.x, -MAX_VELOCITY);

            if (_playerInput.move == Vector2.zero) _rb.velocity = Vector2.zero;
            if (_playerInput.move.x == 0) _rb.velocity = new Vector2(0, _rb.velocity.y);
            if (_playerInput.move.y == 0) _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
        else
        {
            Time.timeScale = 0.5f;
        }
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

    private void Jump()
    {
        if(_playerInput.isJumping && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            UpdateJump();
        }
    }

    void StartJump()
    {
        isJumping = true;
        jumpStartTime = Time.time;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void UpdateJump()
    {
        float elapsedTime = Time.time - jumpStartTime;
        if (elapsedTime < jumpDuration)
        {
            // Calculate the z position based on the elapsed time for the jump ascent
            float jumpProgress = elapsedTime / jumpDuration;
            float zPosition = Mathf.Lerp(initialZPosition, -jumpForce, jumpProgress);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
        }
        else if (elapsedTime < jumpDuration * 2)
        {
            // Calculate the z position based on the elapsed time for the jump descent
            float fallProgress = (elapsedTime - jumpDuration) / jumpDuration;
            float zPosition = Mathf.Lerp(-jumpForce, initialZPosition, fallProgress);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
        }
        else
        {
            // End the jump
            transform.position = new Vector3(transform.position.x, transform.position.y, initialZPosition);
            isJumping = false;
        }
    }

}
