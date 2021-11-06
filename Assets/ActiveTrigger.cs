using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActiveTrigger : MonoBehaviour
{
    private bool _isTriggerActive = false;

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
}
