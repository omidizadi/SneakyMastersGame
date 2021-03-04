using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    //fields
    [SerializeField] private float moveSpeed;
    
    //dependencies
    private PlayerController playerController;

    //variables
    private LevelData levelData;
    private int currentWayPointIndex;
    private bool pathFinished;
    private void Awake()
    {
        playerController = GameContainer.ResolveSingleton<PlayerController>();
        playerController.OnWayPointReached += OnWayPointReached;
    }

    private void OnWayPointReached(LevelCommand command)
    {
        if (command.autoPass)
            MoveToWayPoint().ActivateNextWayPoint();

    }

    public void SetLevelData(LevelData levelData)
    {
        this.levelData = levelData;
    }

    public PathController SetToWayPoint()
    {
        playerController.SetToWayPoint(GetCurrentCommand());
        OnWayPointReached(GetCurrentCommand());
        return this;
    }

    public PathController MoveToWayPoint()
    {
        playerController.MoveToWayPoint(GetCurrentCommand(), moveSpeed);
        return this;
    }

    public PathController ActivateNextWayPoint()
    {
        currentWayPointIndex++;
        if (currentWayPointIndex > levelData.CommandsLength()-1)
            pathFinished = true;
        return this;
    }

    public bool AnyWayPointAvailable()
    {
        return !pathFinished;
    }

    private LevelCommand GetCurrentCommand()
    {
        return levelData.GetCommand(currentWayPointIndex);
    }
}