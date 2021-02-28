using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LineRenderer bulletPathRenderer;
    [SerializeField] private Transform gunHead;
    [SerializeField] private int numberOfReflections;
    [SerializeField] private float maxStepDistance;

    private bool aiming;
    [SerializeField]private bool objectiveInSightFlag;
    private LevelObjective currentObjective;

    public void StartAiming()
    {
        bulletPathRenderer.enabled = true;

        aiming = true;
    }

    public void FinishAiming()
    {
        aiming = false;
        bulletPathRenderer.enabled = false;
        ObjectiveOutOfSight();

    }

    private void FixedUpdate()
    {
        if (aiming) PredictReflectionPattern();
    }


    private void PredictReflectionPattern()
    {
        var position = gunHead.position;
        var direction = gunHead.forward;
        bulletPathRenderer.SetPosition(0, position);


        for (var i = 0; i < numberOfReflections; i++)
        {
            var rayHit = Physics.Raycast(position, direction, out var hitPoint, maxStepDistance);
            if (rayHit)
            {
                ObjectiveTargetCheck(hitPoint);
                direction = Vector3.Reflect(direction, hitPoint.normal);
                position = hitPoint.point;
            }
            else
            {
                position += direction * maxStepDistance;
            }

            bulletPathRenderer.SetPosition(i + 1, position);
        }
    }

    private void ObjectiveTargetCheck(RaycastHit hitPoint)
    {
        if (hitPoint.collider.CompareTag($"Objective"))
        {
            ObjectiveInSight(hitPoint);
        }
        else
        {
            ObjectiveOutOfSight();
        }
    }

    private void ObjectiveInSight(RaycastHit hitPoint)
    {
        if (objectiveInSightFlag) return;
        currentObjective = gameManager.GetObjective(hitPoint.collider.gameObject);
        currentObjective.ObjectiveInSight();
        objectiveInSightFlag = true;
    }

    private void ObjectiveOutOfSight()
    {
        if (!objectiveInSightFlag) return;
        currentObjective.ObjectiveOutOfSight();
        objectiveInSightFlag = false;
    }
}