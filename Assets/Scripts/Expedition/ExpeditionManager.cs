using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ExpeditionManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    GameObject _player;
    LootManager _lootManager;

    void Start()
    {
        _lootManager = GetComponent<LootManager>();
        _player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);

        string sceneName = SceneManager.GetActiveScene().name;
        // Check if this is the first time the scene is loaded
        bool isInitialized = GameStatsService.SceneStateManager.SceneIsInitialized(sceneName);

        if (isInitialized)
        {
            Debug.Log("Found old state");
            _lootManager.InitializeState(GameStatsService.SceneStateManager.GetSceneState(sceneName));
        }
        else {
            Debug.Log("Scene not initialized before");
            _lootManager.InitializeState(ExpeditionDefaultLoot.state);
        }

        Camera.main.GetComponent<CameraFollow>().player = _player;
    }

    public void FinishExpedition()
    {
        AddInventoryToHub();

        SaveSceneState();

        SceneManager.LoadScene("New Scene");
    }

    void SaveSceneState()
    {
        ExpeditionLevelState state = new ExpeditionLevelState();
        LootContainer[] containers = GameObject.FindGameObjectsWithTag("LootContainer")
                                               .Select(g => g.GetComponent<LootContainer>())
                                               .ToArray();

        foreach (LootContainer container in containers)
        {
            state.containerContents.Add(container.GetId(), container.inventory);
        }

        string sceneName = SceneManager.GetActiveScene().name;
        GameStatsService.SceneStateManager.SaveSceneState(sceneName, state);
    }

    void AddInventoryToHub()
    {
        Player player = _player.GetComponent<Player>();

        GameStatsService.Instance.gameStats.AddItems(player.inventory.items.ToArray());
    }
}
