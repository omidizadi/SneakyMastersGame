using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //fields
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip runningClip;
    [SerializeField] private AudioClip bulletHitClip;
    [SerializeField] private AudioClip bulletFlyClip;
    [SerializeField] private float sfxVolume;

    //dependencies
    private PlayerController playerController;
    private IShootController shootController;
    private GameManager gameManager;

    private void Awake()
    {
        shootController = GameContainer.ResolveSingleton<ShootController>();
        playerController = GameContainer.ResolveSingleton<PlayerController>();
        gameManager = GameContainer.ResolveSingleton<GameManager>();

        playerController.OnWayPointReached += command =>
        {
            if (!command.autoPass)
                audioSource.Stop();
        };
        playerController.OnShootStart += () => audioSource.PlayOneShot(shootClip, sfxVolume);
        gameManager.OnStartMoving += () =>
        {
            audioSource.clip = runningClip;
            audioSource.loop = true;
            audioSource.volume = sfxVolume;
            audioSource.Play();
        };
        shootController.OnBulletReachedPoint += () =>
        {
            audioSource.PlayOneShot(bulletHitClip, sfxVolume);
            audioSource.PlayOneShot(bulletFlyClip, sfxVolume);
        };
    }
}