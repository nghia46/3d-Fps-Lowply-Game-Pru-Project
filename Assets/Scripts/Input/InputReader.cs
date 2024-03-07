using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, GameInput.IGamePlayActions
{
    private GameInput gameInput;
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
    //Single Fire Event for shooting... using left mouse, trigger(controller) .etc
    public event Action SingleFireEvent;
    public event Action SingleFireCancelEvent;
    // Burst Fire Event
    public event Action BurstFireEvent;
    public event Action BurstFireCancelEvent;
    // Gun aim Event
    public event Action GunAimEvent;
    public event Action GunAimCancelEvent;
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

    public void OnFireSingle(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                SingleFireEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                SingleFireCancelEvent?.Invoke();
                break;
        }
    }
    public void OnFireBrust(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                BurstFireEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                BurstFireCancelEvent?.Invoke();
                break;
        }
    }
    public void OnGunAim(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                GunAimEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                GunAimCancelEvent?.Invoke();
                break;
        }
    }
}
