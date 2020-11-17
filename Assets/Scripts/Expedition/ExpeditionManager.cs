using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpeditionManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    GameObject _player;

    void Start()
    {
        _player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().player = _player;
    }

    public void FinishExpedition()
    {
        Debug.Log(_player.name);
        Player player = _player.GetComponent<Player>();

        GameStatsService.Instance.gameStats.AddItems(player.inventory.items.ToArray());
        
        SceneManager.LoadScene("Hub");
    }
}
