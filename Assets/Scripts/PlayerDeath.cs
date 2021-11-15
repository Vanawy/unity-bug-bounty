using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private LayerMask _cantTouchThis;

    public delegate void RestartAction();
    public event RestartAction OnRestart;

    private void Update() {
        if (Input.GetButtonDown("Restart")) {
            FireRestartLevelEvent();
        }
    }       

    private void OnCollisionStay2D(Collision2D other) {
        // idk how to do it properly rn
        if (_cantTouchThis != (_cantTouchThis | 1 << other.collider.gameObject.layer))
            return;
        FireRestartLevelEvent();
    }

    private void FireRestartLevelEvent()
    {
        OnRestart();
    }
}
