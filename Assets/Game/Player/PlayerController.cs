using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public event Action<WayPoint> onWayPointReached;

    private IPlayerLocomotion playerLocomotion;
    private IPlayerMoveController playerMoveController;
    private IPlayerAimControl playerAimControl;
    [SerializeField] private BasicGun weapon;

    private void Awake()
    {
        playerLocomotion = GetComponent<IPlayerLocomotion>();
        playerAimControl = GetComponent<IPlayerAimControl>();
        playerMoveController = GetComponent<IPlayerMoveController>();
    }

    private void Start()
    {
        playerMoveController.onWayPointReached += onWayPointReached;
        playerMoveController.onWayPointReached += point =>
        {
            if (!point.autoPass) playerLocomotion.DoIdle();
        };
    }

    public void MoveToWayPoint(WayPoint wayPoint, float speed)
    {
        playerMoveController.MoveToWayPoint(wayPoint, speed);
        playerLocomotion.DoSprint();
    }

    public void SetToWayPoint(WayPoint wayPoint)
    {
        playerMoveController.SetToWayPoint(wayPoint);
    }

    public void OnAimStarted(BaseEventData e)
    {
        weapon.StartAiming();
        playerAimControl.OnAimStarted(e.currentInputModule.input.mousePosition);
    }

    public void OnAimDrag(BaseEventData e)
    {
        playerAimControl.OnDrag(e.currentInputModule.input.mousePosition);
    }

    public void OnShoot(BaseEventData e)
    {
        weapon.FinishAiming();
        playerLocomotion.DoShoot();
        playerAimControl.OnShoot();
    }
}