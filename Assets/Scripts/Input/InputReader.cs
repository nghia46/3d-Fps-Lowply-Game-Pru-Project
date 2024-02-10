using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions
{
    private GameInput gameInput;
    private bool fireButtonDown;

    private void OnEnable()
    {
        gameInput = new();
        gameInput.GamePlay.SetCallbacks(this);
        gameInput.GamePlay.Enable();
    }
    private void OnDisable()
    {
        gameInput?.Disable();
    }
    //Move Event 
    public event Action<Vector2> MoveEvent;
    // Look event
    public event Action<Vector2> MouseEvent;
    //Fire Event for shooting... using left mouse, trigger(controller) .etc
    public event Action FireEvent;
    public event Action FireCancelEvent;
    //Jump event
    public event Action JumpEvent;
    public event Action JumpCancelEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                JumpEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                JumpCancelEvent?.Invoke();
                break;
        }
    }
    public void OnCameraMoverment(InputAction.CallbackContext context)
    {
        MouseEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                FireEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                FireCancelEvent?.Invoke();
                break;
        }
    }
}
