using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    [SerializeField] private GameObject normalObject;
    [SerializeField] private GameObject coloredObject;

    public void ObjectiveInSight()
    {
        normalObject.SetActive(false);
        coloredObject.SetActive(true);
    }   
    
    public void ObjectiveOutOfSight()
    {
        normalObject.SetActive(true);
        coloredObject.SetActive(false);
    }
}
