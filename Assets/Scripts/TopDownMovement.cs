using UnityEngine;
using UnityEngine.InputSystem;

            /////////////// INFORMATION ///////////////
// This script automatically adds a Rigidbody2D and a CapsuleCollider2D component in the inspector.
// The following components are needed: Player Input

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class TopDownMovement : MonoBehaviour
{
    public float maxSpeed = 7;
    
    public bool controlEnabled { get; set; } = true;    // You can edit this variable from Unity Events
    
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    
    private Animator animator;

    void Update()
    {
        float currentvelocityX = Mathf.Abs(rb.linearVelocity.x);

        if (currentvelocityX > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // Set gravity scale to 0 so player won't "fall" 
        rb.gravityScale = 0;
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

        if (rb.linearVelocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rb.linearVelocity.x < 0)
        {
            sprite.flipX = true;
        }
        
        // Write code for walking animation here. (Suggestion: send your current velocity into the Animator for both the x- and y-axis.)
    }
    
    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
        
    }
}
