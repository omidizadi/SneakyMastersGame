using System;
using UnityEngine;

public interface IShootController
{
    event Action OnShootingDone;
    event Action OnBulletReachedPoint;
    void InitBullet(Vector3 position);
    void ShootBullet(Vector3[] hitPoints,RaycastHit[] rayCastHits);
}