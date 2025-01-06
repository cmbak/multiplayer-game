using Unity.Netcode;
using UnityEngine;

// All code from https://www.youtube.com/watch?v=g9z-JHiQDxE
// For some reason I couldn't get the client to move their player
// When using the new input system
// For the sake of time I decided to use the old input system :)
public class OldPlayerMovement : NetworkBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseSens = 2f;
    public Transform groundCheck;
    public Transform playerCamera;
    public LayerMask ground;

    private Rigidbody rb;
    public bool isGrounded;
    private float cameraVeritcalAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Delete this instance's player if not the owner (i.e. not controlling it)
        if (!IsOwner) Destroy(playerCamera.gameObject);
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 velocity = moveDir * currentSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity; 
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        transform.Rotate(Vector3.up * mouseX);

        cameraVeritcalAngle -= mouseY;
        cameraVeritcalAngle = Mathf.Clamp(cameraVeritcalAngle, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(cameraVeritcalAngle, 0f, 0f);
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, ground);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        MovePlayer();
        Jump();
        RotateCamera();
    }
}
