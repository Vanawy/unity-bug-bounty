using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField]
    private WizardController _wizard;

    private void StartWizardFly()
    {
        _wizard.StartFlying();
    }
}
