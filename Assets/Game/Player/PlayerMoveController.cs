using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public event Action<WayPoint> onWayPointReached;
    [SerializeField] private Transform playerTransform;
    private float moveSpeed;
    private float moveProgress;
    private Vector3 currentPosition;
    private WayPoint targetWayPoint;


    public void MoveTo(WayPoint wayPoint, float speed)
    {
        moveProgress = 0;
        targetWayPoint = wayPoint;
        currentPosition = playerTransform.position;
        playerTransform.LookAt(targetWayPoint.position);
        moveSpeed = speed;
    }

    public void SetTo(WayPoint wayPoint)
    {
        moveProgress = 1;
        playerTransform.position = wayPoint.position;
        if (wayPoint.overrideRotation)
            playerTransform.rotation = Quaternion.Euler(wayPoint.rotation);
    }

    private void Update()
    {
        if (moveProgress < 1)
        {
            playerTransform.position = Vector3.Lerp(currentPosition, targetWayPoint.position, moveProgress);
            moveProgress += Time.deltaTime * moveSpeed;

            if (moveProgress >= 1)
            {
                onWayPointReached?.Invoke(targetWayPoint);
                if (targetWayPoint.overrideRotation)
                    playerTransform.rotation = Quaternion.Euler(targetWayPoint.rotation);
            }
        }
    }
}