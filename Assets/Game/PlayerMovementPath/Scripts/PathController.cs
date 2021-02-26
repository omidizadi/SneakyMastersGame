using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerController playerController;

    private LevelData levelData;
    private int currentWayPointIndex;

    private void Awake()
    {
        playerController.onWayPointReached += point =>
        {
            if (point.autoPass)
                MoveToWayPoint().ActivateNextWayPoint();
        };
    }

    public void SetLevelData(LevelData levelData)
    {
        this.levelData = levelData;
    }

    public PathController SetToWayPoint()
    {
        playerController.SetToWayPoint(GetCurrentWayPoint());
        return this;
    }

    public PathController MoveToWayPoint()
    {
        playerController.MoveToWayPoint(GetCurrentWayPoint(), moveSpeed);
        return this;
    }

    public PathController ActivateNextWayPoint()
    {
        currentWayPointIndex++;
        return this;
    }

    private WayPoint GetCurrentWayPoint()
    {
        return levelData.GetWayPoint(currentWayPointIndex);
    }
}