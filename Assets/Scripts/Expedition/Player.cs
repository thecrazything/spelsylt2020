using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Test-value, 1 minute of oxygen
    float oxygen = 10;

    Character character;
    public Inventory inventory = new Inventory(5);
    public GameObject inventoryCanvas;

    InventoryUI inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        character = GameStatsService.Instance.selectedCharacter;

        Debug.Log(character.name);
        inventoryUI = Instantiate(inventoryCanvas, transform).GetComponent<InventoryUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("End expedition");
            GameStatsService.Instance.CompleteExpedition();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryUI.Toggle();
        }

        //oxygen -= Time.deltaTime;

        if (oxygen < 0) {
            character.subtractHealth(1);
        }

        checkIfDead();
    }

    void checkIfDead() 
    {
        if (character.health <= 0) {
            Debug.Log("You dieded");

            character.dead = true;
            GameStatsService.Instance.CompleteExpedition();
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
