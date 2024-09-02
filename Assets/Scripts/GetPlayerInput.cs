using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPlayerInput : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public bool isJumping;
    public bool isStopping;
    public bool isFiring;
    public bool isLeftFiring;
    public bool isAutoMoving;
    public bool isInteracting;
    public bool isPausing;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnStop(InputValue value)
    {
        StopInput(value.isPressed);
    }

    public void OnAutoMove(InputValue value)
    {
        AutoMoveInput();
    }

    public void OnFire(InputValue value)
    {
        FireInput(value.isPressed);
    }

    public void OnLeftFire(InputValue value)
    {
        LeftFireInput(value.isPressed);
    }

    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }

    public void OnPause(InputValue value)
    {
        PauseInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMove)
    {
        move = newMove;
    }

    public void LookInput(Vector2 newLook)
    {
        look = newLook;
    }

    public void JumpInput(bool isPressed)
    {
        isJumping = isPressed;
    }

    public void StopInput(bool newState)
    {
        isStopping = newState;
    }

    public void AutoMoveInput()
    {
        isAutoMoving = !isAutoMoving;
    }

    public void FireInput(bool newState)
    {
        isFiring = newState;
    }

    public void LeftFireInput(bool newState)
    {
        isLeftFiring = newState;
    }

    public void InteractInput(bool newState)
    {
        isInteracting = newState;
    }

    public void PauseInput(bool newState)
    {
        isPausing = newState;
    }
}
