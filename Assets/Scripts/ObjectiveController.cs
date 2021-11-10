using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectiveType
{
    YELLOW,
    BLUE
}

[ExecuteInEditMode]
public class ObjectiveController : MonoBehaviour
{   

    public ObjectiveType type;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _sr;

    private void Awake() {
        switch (type)
        {
            case ObjectiveType.YELLOW:
                _animator.SetTrigger("yellow");
                break;
            case ObjectiveType.BLUE:
                _animator.SetTrigger("blue");
                break;
        }
        _sr.flipX = Random.value > 0.5f;
    }

    public void SetType(string name)
    {
        type = NameToType(name);
    }

    static ObjectiveType NameToType(string name)
    {
        switch (name)
        {
            case "yellow":
                return ObjectiveType.YELLOW;
            case "blue":
                return ObjectiveType.BLUE;
            default:
                return ObjectiveType.YELLOW;
        }
    }
}
