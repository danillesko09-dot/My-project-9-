using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float moveSpeed = 8f;      
    [SerializeField] private float maxSpeed = 10f;      

    [Header("Прыжок")]
    [SerializeField] private float jumpForce = 12f;     
    [SerializeField] private float jumpCooldown = 0.2f; 

    [Header("Вращение")]
    [SerializeField] private float rotateSpeed = 300f; // <--- НОВАЯ НАСТРОЙКА

    private Rigidbody2D rb;
    private float moveInput;
    private float jumpTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Ввод
        moveInput = 0f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveInput = -1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveInput = 1f;

        // Таймер прыжка
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        // Прыжок
        bool jumpPressed = keyboard.wKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame;

        if (jumpPressed && jumpTimer <= 0f)
        {
            if (Mathf.Abs(rb.linearVelocity.y) < 0.1f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimer = jumpCooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        // Качение (движение)
        Vector2 velocity = rb.linearVelocity;
        velocity.x = Mathf.Clamp(velocity.x + moveInput * moveSpeed * Time.fixedDeltaTime * 10f, -maxSpeed, maxSpeed);

        if (moveInput == 0f)
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, moveSpeed * Time.fixedDeltaTime * 2f);

        rb.linearVelocity = velocity;

        // Вращение шарика с использованием новой настройки rotateSpeed
        if (Mathf.Abs(velocity.x) > 0.1f)
        {
            float rotation = -velocity.x * rotateSpeed * Time.fixedDeltaTime; 
            rb.MoveRotation(rb.rotation + rotation);
        }
    }
}