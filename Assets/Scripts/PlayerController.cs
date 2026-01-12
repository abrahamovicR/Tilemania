using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector2 moveInput;
    Rigidbody2D _rb;
    Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, _rb.linearVelocity.y);
        _rb.linearVelocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;
        _animator.SetBool("isRunning", hasHorizontalSpeed);

    }

    void onJump(InputValue value)
    {
        if (value.isPressed )
        {
            _rb.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(_rb.linearVelocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.linearVelocity.x), 1f);
        }
    }
}
