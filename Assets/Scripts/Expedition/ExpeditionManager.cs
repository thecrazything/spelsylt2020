using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    GameObject _player;

    void Start()
    {
        GameObject _player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().player = _player;
    }
}
