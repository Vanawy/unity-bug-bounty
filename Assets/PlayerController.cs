using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private float _inY = 0, _inX = 0;
    private bool _inJump, _inJumpDown;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sr;

    [Header("Jump")]
    [SerializeField]
    [Range(1, 15)]
    private float _jumpVelocity = 5f;
    [SerializeField]
    [Range(0, 5)]
    private float _fallMultiplier = 2.5f;
    [SerializeField]
    [Range(0, 5)]
    private float _lowJumpMultiplier = 2f;
    [SerializeField]
    private ActiveTrigger _jumpTrigger;

    [Header("Move")]
    [SerializeField]
    private float _maxSpeed = 2f;
    private bool _isJumping = false; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        _inX = Input.GetAxis("Horizontal");
        _inY = Input.GetAxis("Vertical");
        _inJumpDown = Input.GetButtonDown("Jump") || _inJumpDown;
        _inJump = Input.GetButton("Jump");
    }

    void FixedUpdate() {
        _isJumping = !_jumpTrigger.IsActive();
        if (CanJump()) {
            Jump();
        }
        BetterJump();
        Move();
        _inJumpDown = false;
        UpdateAnimator();
    }

    void LateUpdate() {
        UpdateSprite();
    }

    private bool CanJump()
    {
        if (_isJumping) return false;
        if (_inJumpDown) return true;
        return false;
    }

    private void Jump()
    {
        _rb.velocity += Vector2.up * _jumpVelocity;
    }

    private void BetterJump()
    {
        if (_rb.velocity.y < 0) {
            _rb.AddForce(Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1));
        } else if (_rb.velocity.y > 0 && !_inJump) {
            _rb.AddForce(Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1));
        }
    }

    private void UpdateSprite()
    {
        if (_rb.velocity.x < 0) { 
            _sr.flipX = true;
        } else if (_rb.velocity.x > 0) { 
            _sr.flipX = false;
        }
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_inX * _maxSpeed, _rb.velocity.y);
    }

    private void UpdateAnimator()
    {
        _animator.SetBool("is_jumping", _isJumping);
        _animator.SetFloat("speed", _rb.velocity.magnitude);
    }
}
