using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    private Transform _clouds;
    [SerializeField]
    private float _multiplier = 0.1f;
    private Rigidbody2D _player;

    void Awake()
    {
        _player = GetComponentInChildren<PlayerController>().gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player) {
            this.enabled = false;
            return;
        }
        _clouds.transform.position += (Vector3) _player.velocity * Time.deltaTime * _multiplier;
    }
}
