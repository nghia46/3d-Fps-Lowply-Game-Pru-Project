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
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> MouseEvent;

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


}
