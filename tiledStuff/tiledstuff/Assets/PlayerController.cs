using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public event Action<Vector3> OnPlayerDied;

    void Awake()
    {
        instance = this;
    }

    public void _PlayerDied()
    {
        OnPlayerDied(GameObject.FindWithTag("Player").transform.position);
    }
}
