using System;
using UnityEngine;

public interface IShootController
{
    void InitBullet(Vector3 position);
    void ShootBullet(Vector3[] hitPoints, Action OnBulletReachedPoint);
}