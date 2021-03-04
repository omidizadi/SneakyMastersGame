using System;
using UnityEngine;

[Serializable]
public struct LevelCommand
{
    [Header("Position & Rotation")]
    public Vector3 position;
    public Vector3 rotation;
    public bool overrideRotation;
    
    [Header("Aiming")]
    public Vector2 horizontalAimConstraints;
    public Vector2 verticalAimConstraints;
    
    [Header("Actions")]
    public bool autoPass;

    public bool enableAiming;
   
    [Header("Camera")]
    public bool activateCamera;
    public CameraType cameraType;
    public int cameraIndex;
}