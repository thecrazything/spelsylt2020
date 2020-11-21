using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestHub : MonoBehaviour
{
    public void LoadExpedition()
    {
        SceneManager.LoadScene("Expedition_Test");
    }
}
