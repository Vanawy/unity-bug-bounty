using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[ExecuteInEditMode]
public class ActiveTrigger : MonoBehaviour
{
    private bool _isTriggerActive = false;
    private Collider2D _collider;
    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _isTriggerActive = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        _isTriggerActive = false;
    }

    public bool IsActive()
    {
        return _isTriggerActive;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = _isTriggerActive ? Color.green : Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
