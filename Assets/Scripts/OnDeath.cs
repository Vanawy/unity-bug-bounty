using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public GameController gameController;
    private void OnDestroy() {
        if (!gameController) return;
        gameController.ReloadScene();
    }
}
