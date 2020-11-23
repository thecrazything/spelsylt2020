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

    public ExpeditionManager manager;

    public Character character { get; private set; }

    PlayerMovement _playerMovement;

    public MessageBehaviour console;

    public AudioClip[] breathingSounds;
    public AudioClip noOxygenSound;
    public float breatingSoundDelay = 4f;
    private AudioSource _BreathingSource;
    private float breathTimeout = 0f;

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        character = GameStatsService.Instance.selectedCharacter;

        inventoryUI = Instantiate(inventoryCanvas, transform).GetComponent<ContainerInventoryUI>();
        inventoryUI.SetTitle("Inventory");
        inventoryUI.source = inventory;
        console = GetComponent<MessageBehaviour>();
        _BreathingSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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

        if (oxygen < 0)
        {
            character.subtractHealth(1);
        }

        breathSound();

        checkIfDead();
    }

    void breathSound()
    {
        if (oxygen > 12)
        {
            if (breathTimeout <= 0)
            {
                _BreathingSource.loop = false;
                breathTimeout = breatingSoundDelay;
                _BreathingSource.clip = breathingSounds[Random.Range(0, breathingSounds.Length)];
                _BreathingSource.Play();
            }
            else
            {
                breathTimeout -= Time.deltaTime;
            }
        }
        else if(!_BreathingSource.loop)
        {
            _BreathingSource.clip = noOxygenSound;
            _BreathingSource.loop = true;
            _BreathingSource.Play();
        }
    }

    void checkIfDead() 
    {
        if (character.health <= 0)
        {
            character.dead = true;
            manager.OnPlayerDeath();
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
