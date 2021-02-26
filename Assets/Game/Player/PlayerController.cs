using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<WayPoint> onWayPointReached;

    [SerializeField] private PlayerLocomotion playerLocomotion;
    [SerializeField] private PlayerMoveController playerMoveController;

    private void Start()
    {
        playerMoveController.onWayPointReached += onWayPointReached;
        playerMoveController.onWayPointReached += point =>
        {
            if (!point.autoPass) playerLocomotion.DoIdle();
        };
    }

    public void MoveTo(WayPoint wayPoint, float speed)
    {
        playerMoveController.MoveTo(wayPoint, speed);
        playerLocomotion.DoSprint();
    }

    public void SetTo(WayPoint wayPoint)
    {
        playerMoveController.SetTo(wayPoint);
    }
}