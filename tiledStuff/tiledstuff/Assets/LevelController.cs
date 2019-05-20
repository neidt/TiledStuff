using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public event Action<int> OnLevelComplete;

    void Start()
    {
        instance = this;
    }

    public void _CompletedLevel(int level)
    {
        OnLevelComplete(level);
    }
}
