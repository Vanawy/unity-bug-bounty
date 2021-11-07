using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private SuperObjectLayer _objects;

    [SerializeField]
    private CameraFollow _camera;

    void Awake()
    {
        PlayerController player = _objects.GetComponentInChildren<PlayerController>();
        _camera.SetTarget(player.transform);
    }

    void Update()
    {
    }
}
