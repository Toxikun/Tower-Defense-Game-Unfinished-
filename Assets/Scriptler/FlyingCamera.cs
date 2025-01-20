using UnityEngine;

public class FlyingCamera : MonoBehaviour
{
    public float movementSpeed = 10.0f;      // Kamera hareket hýzý
    public float mouseSensitivity = 100.0f;   // Fare hassasiyeti
    public float boostMultiplier = 2.0f;      // Hýz artýrma çarpaný (örn: Sol Shift ile)

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        // Ýmleci gizle ve kilitle
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        // Fare hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Y ekseninde yukarý-aþaðý bakmayý sýnýrlayýn
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kamerayý döndür
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        yRotation += mouseX;
    }

    void HandleMovement()
    {
        // Hýz artýrma
        float speed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= boostMultiplier;
        }

        // Hareket yönlerini belirle
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            move += transform.forward;
        if (Input.GetKey(KeyCode.S))
            move -= transform.forward;
        if (Input.GetKey(KeyCode.A))
            move -= transform.right;
        if (Input.GetKey(KeyCode.D))
            move += transform.right;
        if (Input.GetKey(KeyCode.Space))
            move += transform.up;
        if (Input.GetKey(KeyCode.LeftControl))
            move -= transform.up;

        // Normalize et ve hareket et
        if (move.magnitude > 0)
        {
            move = move.normalized * speed * Time.deltaTime;
            transform.position += move;
        }
    }
}
