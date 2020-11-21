using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public ExpeditionManager manager;

    void Start() {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player;
        if (collider.TryGetComponent<Player>(out player)) {
            Debug.Log("Player entered exit zone");

            manager.FinishExpedition();
        }
    }
}
