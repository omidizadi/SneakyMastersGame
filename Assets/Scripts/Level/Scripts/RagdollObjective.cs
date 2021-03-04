using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollObjective : MonoBehaviour,ILevelObjective
{
    //events
    public event Action<ILevelObjective> OnObjectiveDestroyed;
    
    //fields
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Material aliveMaterial;
    [SerializeField] private Material onSightMaterial;
    [SerializeField] private Material deadMaterial;
    
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Collider objectiveTrigger;

    //properties
    public bool IsDestroyed { get; private set; }
    
    public void ObjectiveInSight()
    {
        meshRenderer.material = onSightMaterial;
    }

    public void ObjectiveOutOfSight()
    {
        meshRenderer.material = aliveMaterial;

    }

    public void DestroyObjective()
    {
        enemyAnimator.enabled = false;
        objectiveTrigger.enabled = false;
        IsDestroyed = true;
        meshRenderer.material = deadMaterial;
        OnObjectiveDestroyed?.Invoke(this);

    }
}
