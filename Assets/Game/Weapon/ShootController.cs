using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootController : MonoBehaviour, IShootController
{
    [SerializeField] private Transform bullet;
    [SerializeField] private float bulletSpeed;

    public void InitBullet(Vector3 position)
    {
        bullet.transform.position = position;
        bullet.gameObject.SetActive(true);

    }

    public void ShootBullet(Vector3[] hitPoints, Action OnBulletReachedPoint)
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
                .OnComplete(() =>
                {
                    OnBulletReachedPoint?.Invoke();
                    bullet.LookAt(point);
                    if (pointIndex == hitPoints.Length - 1)
                    {
                        bullet.gameObject.SetActive(false);
                    }
                });
            position = point;
            delay += duration;
        }
    }
}