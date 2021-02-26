using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PathController pathController;

    [SerializeField] private LevelData levelData;

    private void Awake()
    {
        pathController.SetLevelData(levelData);
    }

    void Start()
    {
        pathController.SetToWayPoint().ActivateNextWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            pathController.MoveToWayPoint().ActivateNextWayPoint();
        }
    }
}