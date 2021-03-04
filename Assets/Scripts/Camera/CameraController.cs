using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    //fields
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private CinemachineVirtualCamera winCamera;
    [SerializeField] private CinemachineVirtualCamera[] objectiveCameras;
    
    //dependencies
    private PlayerController playerController;
    private GameManager gameManager;
    
    //variables
    private CinemachineVirtualCamera activeCamera;


    private void Awake()
    {
        playerController = GameContainer.ResolveSingleton<PlayerController>();
        gameManager = GameContainer.ResolveSingleton<GameManager>();
        gameManager.OnWinEvent += () => ActivateCamera(CameraType.Win);
        playerController.OnWayPointReached += HandleCommandCamera;
    }

    public void ShakeCamera(float shakeAmount, float shakeDuration)
    {
        var multiChannelPerlin = activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = shakeAmount;
        DOTween.To(() => multiChannelPerlin.m_AmplitudeGain, x => multiChannelPerlin.m_AmplitudeGain = x, 0f,
            shakeDuration).SetEase(Ease.Linear);
    }
    
    private void HandleCommandCamera(LevelCommand command)
    {
        if (command.activateCamera)
            ActivateCamera(command.cameraType, command.cameraIndex);
    }

    private void ActivateCamera(CameraType cameraType, int index = 0)
    {
        playerFollowCamera.gameObject.SetActive(false);
        winCamera.gameObject.SetActive(false);

        foreach (var t in objectiveCameras)
        {
            t.gameObject.SetActive(false);
        }

        switch (cameraType)
        {
            case CameraType.PlayerFollow:
                playerFollowCamera.gameObject.SetActive(true);
                activeCamera = playerFollowCamera;
                break;
            case CameraType.Objective:
                objectiveCameras[index].gameObject.SetActive(true);
                activeCamera = objectiveCameras[index];
                break;
            case CameraType.Win:
                winCamera.gameObject.SetActive(true);
                activeCamera = winCamera;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, null);
        }
    }
}