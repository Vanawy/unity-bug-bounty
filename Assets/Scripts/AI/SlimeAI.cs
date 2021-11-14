using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class SlimeAI : MonoBehaviour {
    private Animator _animator;
    private Rigidbody2D _rb;
    [SerializeField]
    private SpriteRenderer _sr;
    [SerializeField]
    private Collider2D _collider;

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private LayerMask _groundlayer;
    [SerializeField]
    private LayerMask _obstaclelayer;
    [SerializeField]
    private float _groundCheckDistance = 0.5f;
    [SerializeField]
    private float _maxHeight = 0.2f;

    private bool _isMovingLeft = false;
    

    public void Awake () {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void FixedUpdate () {
        if (!CanMoveForward()) {
            _isMovingLeft = !_isMovingLeft;
            return;
        }
        Vector2 dir = _isMovingLeft ? Vector2.left : Vector2.right;
        Vector2 velocity = dir * speed;
        if (velocity.x > 0) {
            _sr.flipX = false;
        } else if (velocity.x < 0) {
            _sr.flipX = true;
        }
        _animator.SetBool("is_moving", true);

        _rb.velocity = velocity;
    }

    public bool CanMoveForward()
    {
        Vector2 dir = _isMovingLeft ? Vector2.left : Vector2.right;

        RaycastHit2D obstacleHit = Physics2D.BoxCast(
            (Vector2) _collider.bounds.center + _groundCheckDistance * dir, 
            Vector3.one * 0.1f, 0, dir, _maxHeight, _obstaclelayer
        );
        if (obstacleHit && obstacleHit.collider != _collider) {
            return false;
        }

        RaycastHit2D groundHit = Physics2D.BoxCast(
            (Vector2) _collider.bounds.center + _groundCheckDistance * dir, 
            _collider.bounds.size, 0, Vector2.down, _maxHeight, _groundlayer
        );
        return groundHit;
    }

    private void OnDrawGizmosSelected() {
        Vector3 dir = _isMovingLeft ? Vector3.left : Vector3.right;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center + _groundCheckDistance * dir, _collider.bounds.size);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_collider.bounds.center + _groundCheckDistance * dir, Vector2.down * _maxHeight);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_collider.bounds.center + _groundCheckDistance * dir, Vector3.one * 0.1f);
    }
}