using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System;

public class AnalyticsTest : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneUnloaded += OnLevelUnloaded;
        
        PlayerController.instance.OnPlayerDied += PlayerController_OnPlayerDied;
    }

    void OnLevelUnloaded(Scene scene)
    {
        PlayerController.instance.OnPlayerDied -= PlayerController_OnPlayerDied;
    }

    void LevelController_OnLevelComplete(int level)
    {
        Analytics.CustomEvent("LEVEL COMPLETE: " + level);
    }

    void PlayerController_OnPlayerDied(Vector3 deathPos)
    {
        //to save complex data, we have to find a different way of storing it!
        Dictionary<string, object> data = new Dictionary<string, object>();

        data.Add("Position", deathPos);

        Analytics.CustomEvent("PLAYER DIED", data);
    }

    private void OnDestroy()
    {
        Analytics.FlushEvents();
    }
}
