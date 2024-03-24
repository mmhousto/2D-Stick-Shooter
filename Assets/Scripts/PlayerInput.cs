using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public bool isFiring;
    public bool isLeftFiring;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
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

    public void FireInput(bool newState)
    {
        isFiring = newState;
    }

    public void LeftFireInput(bool newState)
    {
        isLeftFiring = newState;
    }
}
