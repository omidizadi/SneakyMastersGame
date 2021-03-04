using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootController : MonoBehaviour, IShootController
{
    //events
    public event Action OnBulletReachedPoint;
    public event Action OnShootingDone;

    //fields
    [SerializeField] private Transform bullet;
    [SerializeField] private float bulletSpeed;

    //dependencies
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameContainer.ResolveSingleton<GameManager>();
    }

    public void InitBullet(Vector3 position)
    {
        bullet.transform.position = position;
        bullet.gameObject.SetActive(true);
    }

    public void ShootBullet(Vector3[] hitPoints, RaycastHit[] rayCastHits)
    {
        var delay = 0f;
        var position = bullet.transform.position;
        for (var index = 0; index < hitPoints.Length; index++)
        {
            var point = hitPoints[index];
            var distance = (point - position).magnitude;
            var duration = distance / bulletSpeed;
            bullet.LookAt(point);
            var pointIndex = index;
            bullet.DOMove(point, duration).SetEase(Ease.Linear).SetDelay(delay)
                .OnComplete(() => HandleBulletPathCompleted(hitPoints, rayCastHits, pointIndex))
                .OnStart(() => { bullet.LookAt(point); });
            position = point;
            delay += duration;
        }
    }

    private void HandleBulletPathCompleted(Vector3[] hitPoints, RaycastHit[] rayCastHits, int pointIndex)
    {
        OnBulletReachedPoint?.Invoke();


        var isInsideHitPoints = pointIndex <= rayCastHits.Length - 1;
        if (isInsideHitPoints)
        {
            var bulletHittingNonWallObject = !rayCastHits[pointIndex].collider.CompareTag($"Wall");
            if (bulletHittingNonWallObject)
            {
                var objective = gameManager.GetObjective(rayCastHits[pointIndex].collider.gameObject);
                var hitPointIsObjective = objective != null;
                if (hitPointIsObjective)
                {
                    objective.DestroyObjective();
                }
                else
                {
                    HitTheObject(rayCastHits, pointIndex);
                }
            }
        }

        var isLastHitPoint = pointIndex == hitPoints.Length - 1;
        if (isLastHitPoint)
            ShootingDone();
    }

    private void HitTheObject(RaycastHit[] rayCastHits, int pointIndex)
    {
        rayCastHits[pointIndex].rigidbody.isKinematic = false;
        rayCastHits[pointIndex].rigidbody.AddForce(-rayCastHits[pointIndex].normal * 11000);
    }

    private void ShootingDone()
    {
        OnShootingDone?.Invoke();
        bullet.gameObject.SetActive(false);
    }
}