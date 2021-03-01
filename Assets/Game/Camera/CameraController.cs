using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    [SerializeField] private CinemachineVirtualCamera activeCamera;

    public void ShakeCamera(float shakeAmount,float shakeDuration)
    {
        var multiChannelPerlin = activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = shakeAmount;
        DOTween.To(() => multiChannelPerlin.m_AmplitudeGain, x => multiChannelPerlin.m_AmplitudeGain = x, 0f,
            shakeDuration).SetEase(Ease.Linear);
    }
}