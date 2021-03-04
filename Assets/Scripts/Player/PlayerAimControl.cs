using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAimControl : MonoBehaviour, IPlayerAimControl
{
    //fields
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aimPower;
    [SerializeField] private Vector2 verticalAimConstraint;
    [SerializeField] private Vector2 horizontalAimConstraint;

    //variables
    private Vector2 initialTouchPos;

    public void SetVerticalAimConstraints(Vector2 angle)
    {
        verticalAimConstraint = angle;
    }

    public void SetHorizontalAimConstraints(Vector2 angle)
    {
        horizontalAimConstraint = angle;
    }

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
        
        var rotationY = ClampAngle(yDir, horizontalAimConstraint.x, horizontalAimConstraint.y);
        var rotationX = ClampAngle(xDir, verticalAimConstraint.x, verticalAimConstraint.y);
        rotation = Quaternion.Euler(rotationX, rotationY, 0);
        playerTransform.rotation = rotation;

        initialTouchPos = touchPos;
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


    private Vector2 PoweredDirection(Vector2 normalizedDirection)
    {
        return normalizedDirection * aimPower;
    }
}