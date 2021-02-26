using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private WayPoint[] wayPoints;

    public WayPoint GetWayPoint(int index)
    {
        return index > wayPoints.Length - 1 ? wayPoints[wayPoints.Length - 1] : wayPoints[index];
    }
}