using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PathController pathController;

    [SerializeField] private LevelData levelData;

    [SerializeField] private LevelObjective[] levelObjectives;

    [SerializeField]
    private Dictionary<GameObject, LevelObjective> objectivesStore = new Dictionary<GameObject, LevelObjective>();

    private void Awake()
    {
        pathController.SetLevelData(levelData);
        InitObjectives();
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

    private void InitObjectives()
    {
        foreach (var t in levelObjectives)
        {
            objectivesStore.Add(t.gameObject, t);
        }
    }

    public LevelObjective GetObjective(GameObject objectiveGameObject)
    {
        return objectivesStore[objectiveGameObject];
    }

}