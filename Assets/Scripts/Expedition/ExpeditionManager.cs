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
            ExpeditionLevelState state = GameStatsService.SceneStateManager.GetSceneState(sceneName);
            _lootManager.InitializeState(state);

            // Open doors
            GameObject.FindGameObjectsWithTag("Door")
                .Select(g => g.GetComponent<Door>())
                .ToList()
                .ForEach(d => {
                    if (state.openDoors.IndexOf(d.GetId()) > -1)
                    {
                        d.SetOpen();
                    }
                });
        }
        else {
            ExpeditionLevelState startState;
            if (!ExpeditionDefaultLoot.state.TryGetValue(sceneName, out startState)) {
                throw new System.Exception("No default state for scene " + sceneName);
            }
            _lootManager.InitializeState(startState, true);
        }

        Camera.main.GetComponent<CameraFollow>().player = _player;
    }

    public void FinishExpedition()
    {
        AddInventoryToHub();

        SaveSceneState();

        SceneManager.LoadScene("Hub");
    }

    public void OnPlayerDeath()
    {
        SaveSceneState(true);
        Destroy(_player);

        GameStatsService.Instance.CompleteExpedition(null);

        SceneManager.LoadScene("Hub");
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

        // Find and save any spawned containers
        LootContainer[] additonals = GameObject.FindGameObjectsWithTag("AdditionalLootContainer")
                                               .Select(g => g.GetComponent<LootContainer>())
                                               .ToArray();
        foreach (LootContainer additional in additonals)
        {
            state.additionalContainers.Add(new AdditionalContainer(
                additional.GetComponent<PrefabHolder>().prefab,
                additional.transform.position,
                additional.inventory
            ));
        }

        // Save open doors
        List<Door> openDoors = GameObject.FindGameObjectsWithTag("Door")
                            .Select(g => g.GetComponent<Door>())
                            .Where(d => d.isOpen)
                            .ToList();
        state.openDoors = openDoors.Select(o => o.GetId()).ToList();

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

        GameStatsService.Instance.CompleteExpedition(player.inventory.items.ToArray());
    }
}
