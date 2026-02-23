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
    private Animator anim;

    private InputAction walkInput;
    private InputAction jumpInput;

    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float jumpForce = 7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        InitializeInputs();
    }

    private void InitializeInputs()
    {
        walkInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");
    }

    public void SetMovementValues(PlayerData playerStats)
    {
        movementSpeed = playerStats.moveSpeed;
        jumpForce = playerStats.jumpForce;
    }


    private void FixedUpdate()
    {
        rb.linearVelocityX = walkInput.ReadValue<float>() * movementSpeed;
        HorizontalAnimations();

        if (rb.linearVelocityX > 0) sprite.flipX = false; else if (rb.linearVelocityX < 0) sprite.flipX = true;
    }

    private void Update()
    {
        VerticalAnimations();
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

    private void HorizontalAnimations()
    {
        if (rb.linearVelocityX != 0f) anim.SetBool("Running", true);
        else anim.SetBool("Running", false);
    }

    private void VerticalAnimations()
    {
        if (rb.linearVelocityY > 0.1f)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Rising", true);
        }
        else if (rb.linearVelocityY < -0.1f)
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Rising", false);
        }
        else
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Rising", false);
        }
    }
}
