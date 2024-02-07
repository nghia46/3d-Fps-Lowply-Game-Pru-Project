using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private InputReader input;
        [SerializeField] private PlayerValue playerValue;
        [SerializeField] private Transform playerOrientation;
        private Vector2 rotation;
        private Vector2 mouseDir;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Start()
        {
            input.MouseEvent += HandleMouse;
        }

        private void HandleMouse(Vector2 obj)
        {
            mouseDir = obj;
        }

        private void Update()
        {
            var mousePos = mouseDir * (Time.deltaTime * playerValue.sensitivity);
            rotation.y += mousePos.x;
            rotation.x -= mousePos.y;
            rotation.x = Mathf.Clamp(rotation.x, -90, 90);

            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
            playerOrientation.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }
}