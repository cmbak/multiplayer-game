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
    public LayerMask ground;

    private Rigidbody rb;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
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
    }
}