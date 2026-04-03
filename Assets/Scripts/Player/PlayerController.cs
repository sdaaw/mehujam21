using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;


    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _jumpPressed;

    private bool _isGrounded;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    void OnInteract(InputValue value)
    {
        GetComponent<SpinningAttack>().Upgrade();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * moveSpeed;
    }
}