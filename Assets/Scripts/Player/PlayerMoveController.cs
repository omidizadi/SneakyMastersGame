using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour, IPlayerMoveController
{
    //events
    public event Action<LevelCommand> onWayPointReached;
    
    //fields
    [SerializeField] private Transform playerTransform;
    
    //variables
    private float moveSpeed;
    private float moveProgress;
    private Vector3 currentPosition;
    private LevelCommand targetLevelCommand;


    public void MoveToWayPoint(LevelCommand levelCommand, float speed)
    {
        moveProgress = 0;
        targetLevelCommand = levelCommand;
        currentPosition = playerTransform.position;
        playerTransform.LookAt(targetLevelCommand.position);
        moveSpeed = speed;
    }

    public void SetToWayPoint(LevelCommand levelCommand)
    {
        moveProgress = 1;
        playerTransform.position = levelCommand.position;
        if (levelCommand.overrideRotation)
            playerTransform.rotation = Quaternion.Euler(levelCommand.rotation);
    }

    private void Update()
    {
        if (moveProgress < 1)
        {
            playerTransform.position = Vector3.Lerp(currentPosition, targetLevelCommand.position, moveProgress);
            moveProgress += Time.deltaTime * moveSpeed;

            if (moveProgress >= 1)
            {
                onWayPointReached?.Invoke(targetLevelCommand);
                if (targetLevelCommand.overrideRotation)
                    playerTransform.rotation = Quaternion.Euler(targetLevelCommand.rotation);
            }
        }
    }
}