using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public ExpeditionManager manager;

    ExpeditionManager _manager;

    void Start() {
        _manager = manager.GetComponent<ExpeditionManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player;
        if (collider.TryGetComponent<Player>(out player)) {
            Debug.Log("Player entered exit zone");

            _manager.FinishExpedition();
        }
    }
}
