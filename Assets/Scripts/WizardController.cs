using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _cloud;

    public void StartFlying()
    {
        _cloud.Play();
        _animator.SetBool("is_fly", true);
    }
    public void StopFlying()
    {
        _cloud.Stop();
        _animator.SetBool("is_fly", false);
    }
}
