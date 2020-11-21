using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Containerns that exists on in the level but has not been
/// placed by the editor. Eg. a dead player.
/// </summary>
public class AdditionalContainer
{
    public GameObject gameObject;
    public Vector3 position;
    public List<Item> inventory;

    public AdditionalContainer(GameObject gameObject, Vector3 position, List<Item> inventory)
    {
        this.gameObject = gameObject;
        this.position = position;
        this.inventory = inventory;
    }
}

/// <summary>
/// A representation of the level to keep it the same between scene-loads
/// </summary>
public class ExpeditionLevelState
{
    // Items to be spawned in a random container
    public List<Item> randomItems;

    // Containers to sync items to
    public Dictionary<string, List<Item>> containerContents;

    // Additional containers to spawn, like a dead player
    public List<AdditionalContainer> additionalContainers;

    public ExpeditionLevelState()
    {
        randomItems = new List<Item>();
        containerContents = new Dictionary<string, List<Item>>();
        additionalContainers = new List<AdditionalContainer>();
    }
}

/// <summary>
/// Static class to handle the different scenes and their state
/// </summary>
public class ExpeditionStateManager
{
    private Dictionary<string, ExpeditionLevelState> sceneStates;

    public ExpeditionStateManager()
    {
        sceneStates = new Dictionary<string, ExpeditionLevelState>();
    }

    public ExpeditionLevelState GetSceneState(string scene)
    {
        ExpeditionLevelState state;

        if (!sceneStates.TryGetValue(scene, out state)) {
            throw new System.Exception("Scene not initialized");
        }

        return state;
    }

    public void SaveSceneState(string scene, ExpeditionLevelState state)
    {
        sceneStates.Remove(scene);
        sceneStates.Add(scene, state);
    }

    public bool SceneIsInitialized(string scene)
    {
        return sceneStates.ContainsKey(scene);
    }
}
