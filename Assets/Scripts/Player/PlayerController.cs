using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //events
    public event Action<LevelCommand> OnWayPointReached;
    public event Action OnShootStart;
    public event Action OnShootFinished;
    
    //fields
    [SerializeField] private GameObject gunObject;

    //dependencies
    private IWeapon weapon;
    private IPlayerLocomotion playerLocomotion;
    private IPlayerMoveController playerMoveController;
    private IPlayerAimControl playerAimControl;
    private GameManager gameManager;


    private void Awake()
    {
        playerLocomotion = GetComponent<IPlayerLocomotion>();
        playerAimControl = GetComponent<IPlayerAimControl>();
        playerMoveController = GetComponent<IPlayerMoveController>();
        weapon = GetComponent<IWeapon>();
        gameManager = GameContainer.ResolveSingleton<GameManager>();
    }

    private void Start()
    {
        playerMoveController.onWayPointReached += point =>
        {
            OnWayPointReached?.Invoke(point);

            if (!point.autoPass) playerLocomotion.DoIdle();

            playerAimControl.SetHorizontalAimConstraints(point.horizontalAimConstraints);
            playerAimControl.SetVerticalAimConstraints(point.verticalAimConstraints);
        };

        gameManager.OnWinEvent += OnWin;
        weapon.OnShootingDone += OnShootFinished;

    }

    public void MoveToWayPoint(LevelCommand levelCommand, float speed)
    {
        playerMoveController.MoveToWayPoint(levelCommand, speed);
        playerLocomotion.DoSprint();
    }

    public void SetToWayPoint(LevelCommand levelCommand)
    {
        playerMoveController.SetToWayPoint(levelCommand);
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
        OnShootStart?.Invoke();
        weapon.FinishAiming();
        playerLocomotion.DoShoot();
    }

    private void OnWin()
    {
        playerLocomotion.DoDance();
        gunObject.SetActive(false);
    }
}