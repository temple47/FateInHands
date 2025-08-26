using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4.5f;
    public float sprintSpeed = 7.5f;
    public float jumpHeight = 1.2f;      // meters
    public float gravity = -9.81f * 3f;  // stronger for snappy feel

    [Header("Grounding")]
    public Transform groundCheck;        // empty child at feet
    public float groundRadius = 0.25f;
    public LayerMask groundMask = ~0;    // everything by default

    [Header("Look")]
    public Transform playerCamera;       // assign Camera (child)
    public float mouseSensitivity = 120f; // degrees/second at 1.0 mouse input
    public float minPitch = -85f;
    public float maxPitch = 85f;

    [Header("Options")]
    public bool lockCursor = true;
    public KeyCode sprintKey = KeyCode.LeftShift;

    CharacterController controller;
    float pitch;               // camera pitch
    Vector3 velocity;          // y-velocity for gravity/jump
    bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        // Mouse axes (Input Manager)
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Yaw (rotate the body around Y)
        float yawDelta = mouseX * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, yawDelta, Space.Self);

        // Pitch (rotate the camera around local X)
        pitch -= mouseY * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        if (playerCamera) playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        // Ground check
        if (groundCheck)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask, QueryTriggerInteraction.Ignore);
        }
        else
        {
            // Fallback: rely on CharacterController’s built-in flag
            isGrounded = controller.isGrounded;
        }

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f; // small downward force to keep grounded

        // Input axes
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        float speed = (Input.GetKey(sprintKey) && z > 0f) ? sprintSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // v = sqrt(2gh)
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Optional: draw the ground check gizmo
    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}

