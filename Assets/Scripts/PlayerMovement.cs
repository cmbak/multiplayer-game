using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=tXDgSGOEatk
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;
    Vector2 moveInput;
    Vector2 jumpInput;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {

    }

    private void Update()
    {
        Vector3 horizontal = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;
        controller.Move(horizontal * Time.deltaTime); 
    }
}
