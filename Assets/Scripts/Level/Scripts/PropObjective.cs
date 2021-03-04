using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropObjective : MonoBehaviour, ILevelObjective
{
    //events
    public event Action<ILevelObjective> OnObjectiveDestroyed;

    //fields
    [SerializeField] private GameObject normalObject;
    [SerializeField] private GameObject coloredObject;
    [SerializeField] private GameObject destroyedObject;
    [SerializeField] private Rigidbody objectiveRigidBody;
    [SerializeField] private Collider objectiveTrigger;

    //properties
    public bool IsDestroyed { get; private set; }

    public void ObjectiveInSight()
    {
        normalObject.SetActive(false);
        coloredObject.SetActive(true);
    }

    public void ObjectiveOutOfSight()
    {
        normalObject.SetActive(true);
        coloredObject.SetActive(false);
    }

    public void DestroyObjective()
    {
        destroyedObject.SetActive(true);
        normalObject.SetActive(false);
        coloredObject.SetActive(false);
        objectiveRigidBody.isKinematic = false;
        objectiveTrigger.enabled = false;
        IsDestroyed = true;
        OnObjectiveDestroyed?.Invoke(this);
    }
}