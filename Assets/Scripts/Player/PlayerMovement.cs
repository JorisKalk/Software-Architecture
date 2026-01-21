using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Layers")]
    [SerializeField]
    private LayerMask groundedGround;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;

    private InputAction walkInput;
    private InputAction jumpInput;

    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpForce = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        walkInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
    }

    public void SetMovementValues(PlayerData playerStats)
    {
        movementSpeed = playerStats.moveSpeed;
        jumpForce = playerStats.jumpForce;
    }


    void FixedUpdate()
    {
        //rb.linearVelocity = moveAction.ReadValue<Vector2>() * movementSpeed;
        rb.linearVelocityX = walkInput.ReadValue<float>() * movementSpeed;

        if (rb.linearVelocityX > 0) sprite.flipX = false; else if (rb.linearVelocityX < 0) sprite.flipX = true;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocityY = jumpForce;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, UnityEngine.Vector2.down, .1f, groundedGround);
    }
}
