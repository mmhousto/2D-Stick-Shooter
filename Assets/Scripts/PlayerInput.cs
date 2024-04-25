using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public bool isStopping;
    public bool isFiring;
    public bool isLeftFiring;
    public bool isAutoMoving;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
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

    public void MoveInput(Vector2 newMove)
    {
        move = newMove;
    }

    public void LookInput(Vector2 newLook)
    {
        look = newLook;
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
}
