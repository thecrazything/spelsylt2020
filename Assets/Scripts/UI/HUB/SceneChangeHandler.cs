using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public int scene;
    public HubBehaviour hub;
    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        GameStatsService.Instance.gameStats.hubTasks = hub.avalibleTasks;
        SceneManager.LoadScene(scene);
    }
}
