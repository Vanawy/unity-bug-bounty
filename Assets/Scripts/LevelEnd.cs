using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;

    void NextScene()
    {
        _gameController.NextScene();
    }
}
