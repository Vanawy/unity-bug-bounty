using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLayer : MonoBehaviour
{
    public void SetLayer(string name)
    {
        gameObject.layer = LayerMask.NameToLayer(name);
    }
}
