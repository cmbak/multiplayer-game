using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 10f;

    Vector2 moveInput;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {

    }

    public void OnLook(InputAction.CallbackContext ctx)
    {

    }

    private void Update()
    {
        Vector3 horizontal = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;
        controller.Move(horizontal * Time.deltaTime);
    }
}
