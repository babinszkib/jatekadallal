using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Egér érzékenység
    public float moveSpeed = 5f;          // Mozgási sebesség
    public float gravity = -9.81f;       // Gravitáció
    public float jumpHeight = 1.5f;      // Ugrás magassága

    public CharacterController controller; // A játékos mozgásának kezelése

    private Transform cameraTransform;
    private float xRotation = 0f;
    private Vector3 velocity;             // Sebesség a gravitációhoz
    private bool isGrounded;              // Ellenőrzi, hogy a játékos a talajon van-e

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Az egér zárolása
        cameraTransform = Camera.main.transform;

        if (controller == null)
        {
            controller = GetComponent<CharacterController>(); // Automatikusan megtalálja a Character Controller-t
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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Függőleges nézés korlátozása

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera forgatása
        transform.Rotate(Vector3.up * mouseX);                               // Játékos forgatása
    }

    void HandleMovement()
    {
        // Ellenőrizzük, hogy a játékos a talajon van-e
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Talajon tartás
        }

        // WASD billentyűk mozgása
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravitáció alkalmazása
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Ugrás kezelése
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
