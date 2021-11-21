using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    private WizardController _wizard;
    [SerializeField]
    private string _firstLevelScene;

    private void StartWizardFly()
    {
        _wizard.StartFlying();
    }

    private void NextScene()
    {
        SceneManager.LoadScene(_firstLevelScene);
    }
}
