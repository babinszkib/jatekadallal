using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Egér sens
    public float moveSpeed = 5f;          // Mozgasi sebesseg
    public float gravity = -9.81f;       // gravity
    public float jumpHeight = 1.5f;      // ugras gravita(magassag)

    public CharacterController controller; 

    private Transform cameraTransform;
    private float xRotation = 0f;
    private Vector3 velocity;             
    private bool isGrounded;            

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;

        if (controller == null)
        {
            controller = GetComponent<CharacterController>(); /
        }
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        transform.Rotate(Vector3.up * mouseX);                               
    }

    void HandleMovement()
    {
      
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

    
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
