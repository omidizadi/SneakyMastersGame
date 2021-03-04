using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private LevelCommand[] commands;

    public LevelCommand GetCommand(int index)
    {
        return index > commands.Length - 1 ? commands[commands.Length - 1] : commands[index];
    }

    public int CommandsLength()
    {
        return commands.Length;
    }
}