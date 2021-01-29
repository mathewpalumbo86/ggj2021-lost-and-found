using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolHolder : MonoBehaviour
{
    Dictionary<Type, PlayerTool> toolDictionary = new Dictionary<Type, PlayerTool>();

    public void AddTool(PlayerTool tool)
    {
        if (toolDictionary.ContainsKey(tool.GetType()))
            Debug.LogError("Dictioary already holds a " + tool.GetType());
        toolDictionary[tool.GetType()] = tool;
    }

    public T GetTool<T> () where T : PlayerTool
    {
        PlayerTool tool;
        toolDictionary.TryGetValue(typeof(T), out tool);
        return (T)tool;
    }
}
