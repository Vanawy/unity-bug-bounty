using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollector : MonoBehaviour
{
    public delegate void ObjectiveCollectedAction(ObjectiveController objective);

    public event ObjectiveCollectedAction OnCollect;

    [SerializeField]
    private LayerMask _objectiveLayer;

    private void OnTriggerEnter2D(Collider2D other) {
        if (_objectiveLayer != (_objectiveLayer | 1 << other.gameObject.layer))
            return;
        OnCollect(other.GetComponent<ObjectiveController>());
    }
}
