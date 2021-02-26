using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    [SerializeField] private Transform gunHead;
    [SerializeField] private bool aiming;

    public void StartAiming()
    {
        Debug.Log("true");

        aiming = true;
    }

    public void FinishAiming()
    {
        Debug.Log("VAR");
        aiming = false;
    }

    private void Update()
    {
        if (aiming) DrawRay();
    }

    private void DrawRay()
    {

        if (Physics.Raycast(gunHead.position, gunHead.forward, out var hitInfo))
        {
            Debug.DrawLine(gunHead.position, hitInfo.point, Color.yellow);
        }
    }
}