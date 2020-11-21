using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootManager : MonoBehaviour
{
    Dictionary<string, LootContainer> containerTable;

    void Awake()
    {
        containerTable = new Dictionary<string, LootContainer>();

        GameObject.FindGameObjectsWithTag("LootContainer")
            .Select(g => g.GetComponent<LootContainer>())
            .ToList()
            .ForEach(c => containerTable.Add(c.GetId(), c));
    }

    public void InitializeState(ExpeditionLevelState state)
    {
        if (state.randomItems.Count > 0)
        {
            SpawnRandomContent(state.randomItems);
        }

        if (state.containerContents.Count > 0)
        {
            SpawnContainerContent(state.containerContents);
        }

        if (state.additionalContainers.Count > 0)
        {
            state.additionalContainers.ForEach(c => SpawnAdditionalContainer(c));
        }
    }

    void SpawnRandomContent(List<Item> items)
    {
        items.ForEach(i => SpawnInRandomContainer(i));
    }

    void SpawnContainerContent(Dictionary<string, List<Item>> containers)
    {
        containers.Keys
            .ToList()
            .ForEach(k => {
                List<Item> items;
                containers.TryGetValue(k, out items);
                SpawnMultipleInContainerById(k, items);
            });
    }

    public void SpawnAdditionalContainer(AdditionalContainer container)
    {
        GameObject newGameObject = Instantiate(container.gameObject, container.position, Quaternion.identity);
        PrefabHolder prefabHolder = newGameObject.GetComponent<PrefabHolder>();

        if (!prefabHolder)
        {
            prefabHolder = newGameObject.AddComponent<PrefabHolder>();
        }

        prefabHolder.prefab = container.gameObject;

        LootContainer newContainer = newGameObject.GetComponent<LootContainer>();

        newContainer.inventory.AddRange(container.inventory);

        containerTable.Add(newContainer.GetId(), newContainer);
    }

    public void SpawnInRandomContainer(Item item)
    {
        GetRandomContainer().inventory.Add(item);
    }

    public bool SpawnMultipleInContainerById(string id, List<Item> items)
    {
        LootContainer container;
        if (containerTable.TryGetValue(id, out container)) {
            container.inventory.AddRange(items);

            return true;
        }

        return false;
    }

    public bool SpawnInContainerById(string id, Item item)
    {
        LootContainer container;
        if (containerTable.TryGetValue(id, out container))
        {
            container.inventory.Add(item);

            return true;
        }

        return false;
    }

    LootContainer GetRandomContainer()
    {
        var containers = containerTable.Values.ToArray();
        int randomNumber = Random.Range(0, containers.Length);
        return containers[randomNumber];
    }
}
