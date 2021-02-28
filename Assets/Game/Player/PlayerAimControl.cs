using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAimControl : MonoBehaviour, IPlayerAimControl
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aimPower;

    private Vector2 initialTouchPos;

    public void OnAimStarted(Vector2 touchPos)
    {
        initialTouchPos = touchPos;
    }

    public void OnDrag(Vector2 touchPos)
    {
        var normalDir = (touchPos - initialTouchPos).normalized;
        var dir = PoweredDirection(normalDir);
        var rotation = playerTransform.rotation;
        var xDir = rotation.eulerAngles.x + dir.y;
        var yDir = rotation.eulerAngles.y - dir.x;

        if (yDir > 45 && yDir < 180) yDir = 45;
        if (yDir > 180 && yDir < 315) yDir = 315;

        if (xDir > 40 && xDir < 180) xDir = 40;
        if (xDir > 180 && xDir < 350) xDir = 350;

        playerTransform.rotation = Quaternion.Euler(xDir, yDir, 0);
        initialTouchPos = touchPos;
    }

    public void OnShoot()
    {
    }


    private Vector2 PoweredDirection(Vector2 normalizedDirection)
    {
        return normalizedDirection * aimPower;
    }
}