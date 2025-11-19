using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private InputAction moveAction;

    [SerializeField] private float movementSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }


    void FixedUpdate()
    {
        rb.linearVelocity = moveAction.ReadValue<Vector2>() * movementSpeed;
    }
}
