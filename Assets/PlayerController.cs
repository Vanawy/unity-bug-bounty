using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _inY = 0, _inX = 0;

    void Start()
    {
        
    }
    void Update()
    {
        _inX = Input.GetAxis("horizontal");
        _inY = Input.GetAxis("vertical");
    }

    void FixedUpdate() {
        
    }
}
