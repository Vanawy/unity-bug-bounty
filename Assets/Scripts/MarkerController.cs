using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    public Transform parent;

    public Transform target;
    [SerializeField]
    private Sprite _markerSprite;

    [SerializeField]
    private Transform _markerBase;
    [SerializeField]
    private SpriteRenderer _sr;

    void Update()
    {
        if (parent == null || target == null) {
            _sr.enabled = false;
            this.enabled = false;
            return;
        }
        _markerBase.position = parent.position;
        float angle = Vector2.SignedAngle(Vector2.right, target.position - parent.position);
        _markerBase.eulerAngles = new Vector3(0, 0, angle);
    }

    public void SetSprite(Sprite sprite)
    {
        _markerSprite = sprite;
        _sr.sprite = _markerSprite;
    }
}
