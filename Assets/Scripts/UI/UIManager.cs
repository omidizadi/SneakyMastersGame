using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //fields
    [SerializeField] private GameObject touchPad;
    [SerializeField] private GameObject gameStartUi;
    [SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject gameLoseUi;
    [SerializeField] private GameObject gameWinUi;

    [SerializeField] private GameObject[] ammoIcons;
    [SerializeField] private GameObject[] enemyRagdollIcons;
    [SerializeField] private GameObject[] securityCameraIcons;

    //dependencies
    private PlayerController playerController;
    private GameManager gameManager;

    private void Awake()
    {
        playerController = GameContainer.ResolveSingleton<PlayerController>();
        gameManager = GameContainer.ResolveSingleton<GameManager>();
    }

    private void Start()
    {
        playerController.OnShootStart += DisableAiming;
        playerController.OnShootStart += DecreaseAmmo;
        playerController.OnShootFinished += EnableAiming;
        gameManager.OnStartMoving += DisableAiming;
        gameManager.OnWinEvent += ShowWinUI;
        gameManager.OnLoseEvent += ShowLoseUI;
        gameManager.OnObjectiveDestroyedEvent += DestroyObjective;
        playerController.OnWayPointReached += HandleCommandUIAction;
    }

    private void HandleCommandUIAction(LevelCommand command)
    {
        if (command.enableAiming)
            EnableAiming();
        else
            DisableAiming();
    }


    public void StartGame()
    {
        gameManager.StartGame();
        gameStartUi.SetActive(false);
        gameUi.SetActive(true);
        DisableAiming();
    }

    public void RestartTheGame()
    {
        GameContainer.FlushContainer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowWinUI()
    {
        gameWinUi.SetActive(true);
        gameUi.SetActive(false);
        DisableAiming();
    }

    private void ShowLoseUI()
    {
        gameUi.SetActive(false);
        gameLoseUi.SetActive(true);
        DisableAiming();
    }

    private void EnableAiming()
    {
        touchPad.SetActive(true);
    }

    private void DisableAiming()
    {
        touchPad.SetActive(false);
    }

    private void DestroyObjective(ILevelObjective objective)
    {
        if (objective is PropObjective)
            DeactiveFirstActiveIcon(securityCameraIcons);
        if (objective is RagdollObjective)
            DeactiveFirstActiveIcon(enemyRagdollIcons);
    }

    private void DecreaseAmmo()
    {
        DeactiveFirstActiveIcon(ammoIcons);
    }

    private void DeactiveFirstActiveIcon(GameObject[] icons)
    {
        for (var i = 0; i < icons.Length; i++)
        {
            if (icons[i].activeSelf)
            {
                icons[i].SetActive(false);
                break;
            }
        }
    }
}