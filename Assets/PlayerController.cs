using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animation))]
public class PlayerController : MonoBehaviour
{
    private float _inY = 0, _inX = 0;
    private bool _inJump, _inJumpDown;

    private Rigidbody2D _rb;
    private Animation _animation;

    [Header("Settings")]
    [SerializeField]
    [Range(1, 15)]
    private float _jumpVelocity = 5f;
    [Header("Settings")]
    [SerializeField]
    [Range(0, 5)]
    private float _fallMultiplier = 2.5f;
    [Header("Settings")]
    [SerializeField]
    [Range(0, 5)]
    private float _lowJumpMultiplier = 2f;
    private bool _isJumping = false; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animation>();
    }
    void Update()
    {
        _inX = Input.GetAxis("Horizontal");
        _inY = Input.GetAxis("Vertical");
        _inJumpDown = Input.GetButtonDown("Jump") || _inJumpDown;
        _inJump = Input.GetButton("Jump");
    }

    void FixedUpdate() {
        if (_rb.velocity.y < 0) {
            _rb.AddForce(Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1));
        } else if (_rb.velocity.y > 0 && !_inJump) {
            _rb.AddForce(Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1));
        }
        if (CanJump()) {
            Jump();
        }
        _inJumpDown = false;
    }

    private bool CanJump()
    {
        if (_isJumping) return false;
        if (_inJumpDown) return true;
        // TODO: Check for floor
        return false;
    }

    private void Jump()
    {
        _rb.velocity += Vector2.up * _jumpVelocity;
        // _isJumping = true;
    }
}
