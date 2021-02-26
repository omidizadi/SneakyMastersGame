using System;
using UnityEngine;

[Serializable]
public struct WayPoint
{
    public Vector3 position;
    public Vector3 rotation;
    public bool autoPass;
    public bool overrideRotation;

}