using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ActiveTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _collider;
    [SerializeField]
    private LayerMask _jumpableObjectsMask;

    [SerializeField]
    private Vector2 _direction = Vector2.down;

    [SerializeField]
    [Range(0, 1)]
    private float _distance = 1f;


    private bool _isTriggerActive = false;
    public bool IsActive()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, _direction, _distance, _jumpableObjectsMask);
        _isTriggerActive = hit.collider;
        return _isTriggerActive;
    }

    private void OnDrawGizmosSelected() {
        if (!_collider) return;
        Gizmos.color = Color.white;
        Gizmos.DrawRay(_collider.transform.position, _direction * _distance);
        Gizmos.color = _isTriggerActive ? Color.green : Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center + (Vector3) _direction * _distance, _collider.bounds.size);
    }
}
