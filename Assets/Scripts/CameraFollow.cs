using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed = 1f;
    [SerializeField]
    private bool _instant = true;
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _fallbackTarget;
    [SerializeField]
    private Rect _map;

    private Camera _cameraComponent;

    [SerializeField]
    private Vector2 _targetOffset;
    
    void Start()
    {
        _cameraComponent = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update() {
        Bounds cameraBounds = CameraExtensions.OrthographicBounds(_cameraComponent);
        Transform target = _target ? _target : _fallbackTarget;
        if (target == null) {
            return;
        }
        float newX = Mathf.Clamp(
            target.position.x - _targetOffset.x + cameraBounds.extents.x, 
            _map.xMin + cameraBounds.size.x, 
            _map.xMax
        ) - cameraBounds.extents.x;

        float newY = Mathf.Clamp(
            target.position.y - _targetOffset.y + cameraBounds.extents.y, 
            _map.yMin + cameraBounds.size.y, 
            _map.yMax
        ) - cameraBounds.extents.y;

        if (_instant) {
            transform.position = new Vector3(newX, newY, transform.position.z);
        } else {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector3(newX, newY, transform.position.z),
                _cameraSpeed * Time.fixedDeltaTime
            );
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_map.center, _map.size);
        Gizmos.DrawSphere(_map.position, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _targetOffset);
    }
}
