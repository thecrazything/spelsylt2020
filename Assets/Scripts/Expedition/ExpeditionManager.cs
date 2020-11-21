using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ExpeditionManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    public GameObject deadPlayerContainerPrefab;

    GameObject _player;
    LootManager _lootManager;

    void Start()
    {
        _lootManager = GetComponent<LootManager>();
        _player = Instantiate(PlayerPrefab, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<Player>().manager = this;

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

    public void OnPlayerDeath()
    {
        SaveSceneState(true);
        Destroy(_player);

        SceneManager.LoadScene("New Scene");
    }

    void SaveSceneState(bool playerDied = false)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        ExpeditionLevelState state = new ExpeditionLevelState();

        // Sync all loot container content
        LootContainer[] containers = GameObject.FindGameObjectsWithTag("LootContainer")
                                               .Select(g => g.GetComponent<LootContainer>())
                                               .ToArray();

        foreach (LootContainer container in containers)
        {
            state.containerContents.Add(container.GetId(), container.inventory);
        }

        // Save player body as an Additional container if dead
        if (playerDied)
        {
            state.additionalContainers.Add(new AdditionalContainer(
                deadPlayerContainerPrefab,
                _player.transform.position,
                _player.GetComponent<Player>().inventory.items
            ));
        }

        GameStatsService.SceneStateManager.SaveSceneState(sceneName, state);
    }

    void AddInventoryToHub()
    {
        Player player = _player.GetComponent<Player>();

        GameStatsService.Instance.gameStats.AddItems(player.inventory.items.ToArray());
    }
}
