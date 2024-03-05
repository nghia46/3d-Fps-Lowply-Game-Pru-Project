using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Properties")]

        [SerializeField] private PlayerValue playerValue;
        [SerializeField] private InputReader input;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform orientation;

        private CharacterController controller;
        private bool isGrounded;
        private bool isJumping;
        private Vector2 moveDir;
        private Vector3 velocity;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            input.MoveEvent += HandleMove;
            input.JumpEvent += HandleJump;
            input.JumpCancelEvent += HandleJumpCancel;
        }

        private void HandleJumpCancel()
        {
            isJumping = false;
        }

        private void HandleJump()
        {
            isJumping = true;
        }

        private void HandleMove(Vector2 dir)
        {
            moveDir = dir;
        }

        private void Update()
        {

            UpdateGroundedStatus();

            if (isGrounded && IsFalling())
            {
                ResetVerticalVelocity();
            }

            MovePlayer(playerValue.speed);
            JumpPlayer(playerValue.jumpHeight);
            ApplyGravity(playerValue.Gravity);

            RotatePlayerOrientation(orientation);

            controller.Move(velocity * Time.deltaTime);
        }
        private bool IsFalling()
        {
            return velocity.y < 0;
        }

        private void ResetVerticalVelocity()
        {
            velocity.y = -2f;
        }
        private void UpdateGroundedStatus()
        {
            if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hitInfo, playerValue.groundDistance))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    isGrounded = true;
                }
            }
            else
            {
                isGrounded = false;
            }
        }

        private void MovePlayer(float speed)
        {
            var move = (transform.right * moveDir.x) + (transform.forward * moveDir.y);
            controller.Move(move * (speed * Time.deltaTime));
        }
        private void JumpPlayer(float jumpHeight)
        {
            if (isJumping && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerValue.Gravity);
            }
        }
        private void ApplyGravity(float gravity)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        private void RotatePlayerOrientation(Transform orientation)
        {
            transform.rotation = Quaternion.Euler(0, orientation.rotation.eulerAngles.y, 0);
        }
    }
}
