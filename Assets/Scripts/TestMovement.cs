using UnityEngine;
using UnityEngine.InputSystem;

/////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D and a CapsuleCollider2D component in the inspector.
// The following components are needed: Player Input

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class TopDownMovement : MonoBehaviour
{
    public float maxSpeed = 7;

    public bool controlEnabled { get; set; } = true;

    [Header("Vertical Movement")]
    public bool isMovingUp;
    public bool isMovingDown;
    [Tooltip("Minimum speed required to switch the bools to true")]
    public float movementThreshold = 0.1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // --- 1. Handle Horizontal Animation (isRunning) ---
        float currentvelocityX = Mathf.Abs(rb.linearVelocity.x);

        if (currentvelocityX > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // --- 2. Handle Vertical Animation (isMovingUp / isMovingDown) ---
        float currentvelocityY = rb.linearVelocity.y;

        // Calculate the bools
        isMovingUp = currentvelocityY > movementThreshold;
        isMovingDown = currentvelocityY < -movementThreshold;

        // Send the bools to the Animator
        animator.SetBool("isMovingUp", isMovingUp);
        animator.SetBool("isMovingDown", isMovingDown);
    }

    private void FixedUpdate()
    {
        // Set velocity based on direction of input and maxSpeed
        if (controlEnabled)
        {
            rb.linearVelocity = moveInput.normalized * maxSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Sprite Flipping Logic
        if (rb.linearVelocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            sprite.flipX = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }
}