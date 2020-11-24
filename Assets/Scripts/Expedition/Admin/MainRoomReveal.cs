using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MainRoomReveal : MonoBehaviour, OnDoorOpen
{
    List<Light2D> _lights = new List<Light2D>();

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("AdminMainLight");
        foreach (GameObject gObj in gameObjects)
        {
            _lights.Add(gObj.GetComponent<Light2D>());
        }
        _lights.ForEach(light => { light.enabled = false; });
    }

    public void Opened()
    {
        _lights.ForEach(light => { light.enabled = true; });
    }
}
