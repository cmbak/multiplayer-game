using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    // https://www.youtube.com/watch?v=tXDgSGOEatk
    [SerializeField] new Transform camera;
    [SerializeField] float xSensitivity = 10f;
    [SerializeField] float ySensitivity = 0.5f;
    float xClamp = 65f; // Allow player to look behind
    float xRotation = 0f;
    float mouseX;
    float mouseY;

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 mouseInput = ctx.ReadValue<Vector2>();
        mouseX = mouseInput.x * xSensitivity;
        mouseY = mouseInput.y * ySensitivity;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime); // Changing y-axis = horizontal

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        camera.eulerAngles = targetRotation;
    }
}
