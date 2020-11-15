using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float oxygen = 60;
    public float noMovingOxygenMod = 0.5f;
    public float sprintOxygenMod = 2.0f;

    public GameObject inventoryCanvas;
    public Interactor interactor;

    ContainerInventoryUI inventoryUI;
    public PlayerInventory inventory = new PlayerInventory();

    public Character character { get; private set; }

    PlayerMovement _playerMovement;

    void Start()
    {
        interactor = GetComponentInChildren<Interactor>();
        _playerMovement = GetComponent<PlayerMovement>();
        character = GameStatsService.Instance.selectedCharacter;

        inventoryUI = Instantiate(inventoryCanvas, transform).GetComponent<ContainerInventoryUI>();
        inventoryUI.SetTitle("Inventory");
        inventoryUI.source = inventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("End expedition");

            GameStatsService.Instance.CompleteExpedition(inventory.items.ToArray());
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryUI.Toggle();
        }

        if (_playerMovement.isMoving)
        {
            oxygen -= Time.deltaTime * (_playerMovement.iSprinting ? sprintOxygenMod : 1);
        }
        else
        {
            oxygen -= Time.deltaTime * noMovingOxygenMod;
        }

        if (oxygen < 0) {
            character.subtractHealth(1);
        }

        checkIfDead();
    }

    void checkIfDead() 
    {
        if (character.health <= 0) {
            character.dead = true;
            GameStatsService.Instance.CompleteExpedition(null); // TODO add items here
            SceneManager.LoadScene(1);
        }
    }

    void OnGUI()
    {
        float o = Mathf.RoundToInt(oxygen);
        float h = Mathf.RoundToInt(character.health);
        GUI.Label(new Rect(10, 10, 100, 20), "Oxygen: " + o);
        GUI.Label(new Rect(10, 50, 100, 20), "Health: " + h);
    }
}
