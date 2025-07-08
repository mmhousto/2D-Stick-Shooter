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
    [SerializeField]
    private float moveSpeed = 5f;
    private float MAX_VELOCITY = 15f;
    private static float playerMovementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<GetPlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        initialZPosition = transform.position.z;
        playerMovementSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementSpeed();
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
            _rb.linearVelocity = _playerInput.move * moveSpeed;

            if (_rb.linearVelocity.x > MAX_VELOCITY) _rb.linearVelocity = new Vector2(MAX_VELOCITY, _rb.linearVelocity.y);
            if (_rb.linearVelocity.x < -MAX_VELOCITY) _rb.linearVelocity = new Vector2(-MAX_VELOCITY, _rb.linearVelocity.y);
            if (_rb.linearVelocity.y > MAX_VELOCITY) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, MAX_VELOCITY);
            if (_rb.linearVelocity.y < -MAX_VELOCITY) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -MAX_VELOCITY);

            if (_playerInput.move == Vector2.zero) _rb.linearVelocity = Vector2.zero;
            if (_playerInput.move.x == 0) _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            if (_playerInput.move.y == 0) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
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
            _rb.linearVelocity = Vector2.zero;
        }
        else if (_playerInput.isStopping == false && moveSpeed != playerMovementSpeed)
        {
            moveSpeed = playerMovementSpeed;
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
            Vector3 start = new Vector3(transform.position.x + _playerInput.move.x / 750, transform.position.y + _playerInput.move.y / 750, zPosition);
            Vector3 end = new Vector3(transform.position.x + _playerInput.move.x/750, transform.position.y + _playerInput.move.y/750, zPosition);
            transform.position = Vector3.Lerp(start, end, jumpProgress);
        }
        else if (elapsedTime < jumpDuration * 2)
        {
            
            // Calculate the z position based on the elapsed time for the jump descent
            float fallProgress = (elapsedTime - jumpDuration) / jumpDuration;
            float zPosition = Mathf.Lerp(-jumpForce, initialZPosition, fallProgress);
            Vector3 start = new Vector3(transform.position.x + _playerInput.move.x / 750, transform.position.y + _playerInput.move.y / 750, zPosition);
            Vector3 end = new Vector3(transform.position.x + _playerInput.move.x / 750, transform.position.y + _playerInput.move.y / 750, zPosition);
            transform.position = Vector3.Lerp(start, end, fallProgress);
        }
        else
        {
            // End the jump
            transform.position = new Vector3(transform.position.x, transform.position.y, initialZPosition);
            isJumping = false;
        }
    }

    private void UpdateMovementSpeed()
    {
        if(moveSpeed != 0 && moveSpeed != playerMovementSpeed)
            moveSpeed = playerMovementSpeed;
    }

    public static void IncreaseMovementSpeed()
    {
        playerMovementSpeed = playerMovementSpeed + playerMovementSpeed * 0.1f;
        Time.timeScale = 1f;
    }

}
