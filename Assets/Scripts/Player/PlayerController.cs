using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;


    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _jumpPressed;

    private bool _isGrounded;

    [SerializeField]
    private Sprite _bunnyUp;
    [SerializeField]
    private Sprite _bunnyDown;
    [SerializeField]
    private Sprite _bunnySide;
    [SerializeField]
    private Sprite _bunnySideL;
    [SerializeField]
    private Sprite _bunnyBlink;

    private Vector2 lastMoveDir = Vector2.down;

    private SpriteRenderer _sr;


    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();

        StartCoroutine(BlinkAnimation());
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    void OnInteract(InputValue value)
    {
        GetComponent<SpinningAttack>().Upgrade();
    }
    void OnRestart(InputValue value)
    {
        GameManager.Instance.RestartGame();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsGameOver)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        _rb.linearVelocity = _moveInput * moveSpeed;
        if (_moveInput != Vector2.zero)
        {
            lastMoveDir = _moveInput;
            UpdateSprite(_moveInput);
        }
    }

    private void UpdateSprite(Vector2 direction)
    {
        if (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x)) 
        {
            _sr.sprite = direction.y > 0 ? _bunnyUp : _bunnyDown;
        }
        else
        {
            _sr.sprite = direction.x > 0 ? _bunnySide : _bunnySideL;
        }
    }

    IEnumerator BlinkAnimation()
    {
        float randomDelay = Random.Range(2f, 5f);
        while (true) 
        {
            yield return new WaitForSeconds(randomDelay);
            if (_sr.sprite != _bunnyDown) continue;

            _sr.sprite = _bunnyBlink;
            yield return new WaitForSeconds(0.5f);
            _sr.sprite = _bunnyDown;
            randomDelay = Random.Range(2f, 5f);
        }
    }
}