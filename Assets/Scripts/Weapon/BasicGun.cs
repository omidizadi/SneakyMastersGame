using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour, IWeapon
{
    //events
    public event Action OnShootingDone;

    //fields
    [SerializeField] private LineRenderer bulletPathRenderer;
    [SerializeField] private Transform gunHead;
    [SerializeField] private float cameraShakeStrength;
    [SerializeField] private int numberOfReflections;
    [SerializeField] private float maxStepDistance;

    //dependencies
    private GameManager gameManager;
    private CameraController cameraController;
    private IShootController shootController;
    
    //variables
    private bool aiming;
    private bool objectiveInSightFlag;
    private int hittingRayIndex;
    private List<RaycastHit> raycastHits = new List<RaycastHit>();
    private ILevelObjective currentObjective;

    private void Start()
    {
        gameManager = GameContainer.ResolveSingleton<GameManager>();
        cameraController = GameContainer.ResolveSingleton<CameraController>();
        shootController = GetComponent<IShootController>();
        shootController.OnShootingDone += OnShootingDone;
    }

    public void StartAiming()
    {
        bulletPathRenderer.enabled = true;
        hittingRayIndex = -1;
        aiming = true;
    }

    public void FinishAiming()
    {
        aiming = false;
        bulletPathRenderer.enabled = false;
        ObjectiveOutOfSight(-1);
        shootController.OnBulletReachedPoint += () => cameraController.ShakeCamera(cameraShakeStrength, 0.2f);
        shootController.InitBullet(gunHead.position);
        shootController.ShootBullet(DetectedHitPoints(), raycastHits.ToArray());
    }

    private void FixedUpdate()
    {
        if (aiming) PredictReflectionPattern();
    }


    private void PredictReflectionPattern()
    {
        var hitPoints = DetectedHitPoints(ObjectiveTargetCheck);
        bulletPathRenderer.SetPosition(0, gunHead.position);
        for (var i = 0; i < hitPoints.Length; i++)
        {
            bulletPathRenderer.SetPosition(i + 1, hitPoints[i]);
        }
    }

    private Vector3[] DetectedHitPoints(Action<RaycastHit, int> OnRayHit = null)
    {
        raycastHits.Clear();
        var hitPoints = new Vector3[numberOfReflections];
        var position = gunHead.position;
        var direction = gunHead.forward;
        for (var i = 0; i < numberOfReflections; i++)
        {
            var rayHit = Physics.Raycast(position, direction, out var hitPoint, maxStepDistance);
            if (rayHit)
            {
                OnRayHit?.Invoke(hitPoint, i);
                raycastHits.Add(hitPoint);
                direction = Vector3.Reflect(direction, hitPoint.normal);
                position = hitPoint.point;
            }
            else
            {
                position += direction * maxStepDistance;
            }

            hitPoints[i] = position;
        }

        return hitPoints;
    }

    private void ObjectiveTargetCheck(RaycastHit hitPoint, int rayIndex)
    {
        if (hitPoint.collider.CompareTag($"Objective"))
        {
            ObjectiveInSight(hitPoint, rayIndex);
        }
        else
        {
            ObjectiveOutOfSight(rayIndex);
        }
    }

    private void ObjectiveInSight(RaycastHit hitPoint, int rayIndex)
    {
        hittingRayIndex = rayIndex;
        if (objectiveInSightFlag) return;
        currentObjective = gameManager.GetObjective(hitPoint.collider.gameObject);
        currentObjective.ObjectiveInSight();
        objectiveInSightFlag = true;
    }

    private void ObjectiveOutOfSight(int rayIndex)
    {
        if (!objectiveInSightFlag) return;
        if (rayIndex > hittingRayIndex) return;
        hittingRayIndex = -1;
        currentObjective.ObjectiveOutOfSight();
        objectiveInSightFlag = false;
    }
}