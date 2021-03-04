using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //events
    public event Action OnWinEvent;
    public event Action OnLoseEvent;
    public event Action OnStartMoving;
    public event Action<ILevelObjective> OnObjectiveDestroyedEvent;

    //fields
    [SerializeField] private LevelData levelData;
    [Header("Objectives")] [SerializeField]
    private PropObjective[] propObjectives;
    [Header("Gameplay")] [SerializeField] private int ammo;
    [SerializeField] private RagdollObjective[] ragdollObjectives;

    //dependencies
    private PathController pathController;
    private PlayerController playerController;
    
    //variables
    private Dictionary<GameObject, ILevelObjective> objectivesStore = new Dictionary<GameObject, ILevelObjective>();
    private bool loseFlag;
    private bool winFlag;
    private bool objectiveDestroyed;


    private void Awake()
    {
        pathController = GameContainer.ResolveSingleton<PathController>();
        playerController = GameContainer.ResolveSingleton<PlayerController>();
        pathController.SetLevelData(levelData);
        InitObjectives();
    }

    void Start()
    {
        pathController.SetToWayPoint().ActivateNextWayPoint();
        playerController.OnShootStart += () => objectiveDestroyed = false;
        playerController.OnShootStart += DecreaseAmmo;
        playerController.OnShootFinished += CheckDefeat;
        playerController.OnShootFinished += CheckGameState;
    }

    public void StartGame()
    {
        GoToNextPosition();
    }

    private void InitObjectives()
    {
        foreach (var t in propObjectives)
        {
            t.OnObjectiveDestroyed += OnObjectiveDestroyed;
            objectivesStore.Add(t.gameObject, t);
        }

        foreach (var t in ragdollObjectives)
        {
            t.OnObjectiveDestroyed += OnObjectiveDestroyed;
            objectivesStore.Add(t.gameObject, t);
        }
    }

    private void OnObjectiveDestroyed(ILevelObjective objective)
    {
        winFlag = true;
        foreach (var obj in objectivesStore)
        {
            if (!obj.Value.IsDestroyed)
            {
                winFlag = false;
                break;
            }
        }

        objectiveDestroyed = true;
        OnObjectiveDestroyedEvent?.Invoke(objective);
    }

    private void DecreaseAmmo()
    {
        ammo--;
        if (ammo < 0) ammo = 0;
    }

    private void CheckDefeat()
    {
        if (ammo <= 0) loseFlag = true;
    }

    private void CheckGameState()
    {
        
        if (winFlag)
        {
            OnWinEvent?.Invoke();
            return;
        }

        if (loseFlag)
        {
            OnLoseEvent?.Invoke();
            return;
        }


        if (objectiveDestroyed && pathController.AnyWayPointAvailable())
        {
            GoToNextPosition();
        }
    }

    private void GoToNextPosition()
    {
        OnStartMoving?.Invoke();
        pathController.MoveToWayPoint().ActivateNextWayPoint();
    }

    public ILevelObjective GetObjective(GameObject objectiveGameObject)
    {
        return objectivesStore.ContainsKey(objectiveGameObject) ? objectivesStore[objectiveGameObject] : null;
    }
}