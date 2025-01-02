using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=tXDgSGOEatk
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    [SerializeField] float gravity = -30f;
    [SerializeField] float jumpHeight = 3.5f;
    [SerializeField] LayerMask ground;

    Vector2 moveInput;
    Vector3 verticalVelocity = Vector3.zero;
    bool isGrounded;
    bool canJump;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        canJump = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.1f, ground); // So it checks from bottom of player
        if (isGrounded)
        {
            verticalVelocity.y = 0; // Stops player from speeding up as it falls down
        }

        Vector3 horizontal = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;
        controller.Move(horizontal * Time.deltaTime);

        if (canJump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
            canJump = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
