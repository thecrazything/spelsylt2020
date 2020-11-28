using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour, IInteractable
{
    public AudioClip lockpickSound;
    bool _isOpen;
    public bool isOpen
    {
        get
        {
            return _isOpen;
        }
    }

    Animator animator;
    Collider2D _collider;

    public GameObject Light;
    Light2D light2d;

    public GameObject shadowObject;
    public KeycardColor keycardColor;
    public bool requiresKeycard = false;

    public bool pickable = false;
    public float picktime = 8f;
    public int pickDifficulty = 2;

    bool isFailed = false;
    float failedTimer = 0;
    float failedTimerCooldown = 3;
    AudioSource _audioSource;

    SpriteRenderer _renderer;

    OnDoorOpen _OnOpen;

    void Start() {
        if (Light != null)
        {
            light2d = Light.GetComponentInChildren<Light2D>();
            ResetLight();
        }

        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _OnOpen = GetComponent<OnDoorOpen>();
    }

    void Update()
    {
        if (failedTimer > 0) {
            failedTimer -= Time.deltaTime;
        }

        if (failedTimer <= 0 && isFailed)
        {
            isFailed = false;
            ResetLight();
        }
    }

    public string GetId()
    {
        return (transform.position + name).ToString().Replace(" ", "");
    }

    private void _open()
    {
        _audioSource.Play();
        animator.SetBool("IsOpen", true);
        StartCoroutine(FixRenderOrder());

        _setComponentsOpen();
    }

    public void SetOpen()
    {
        animator.Play("Door_Open");
        _renderer.sortingOrder = 5;
        _setComponentsOpen();

        if (_OnOpen != null)
        {
            _OnOpen.SetStateOpen();
        }
    }

    private void _setComponentsOpen()
    {
        shadowObject.SetActive(false);
        _collider.enabled = false;
        _isOpen = true;
    }

    public float? GetActionTime(GameObject source)
    {
        Player player = ComponentUtil.RequireComponent<Player>(source);
        return (GetKeycard(player) == null && pickable) ? picktime as float? : null;
    }

    public string GetActionTitle(GameObject source)
    {
        Player player = ComponentUtil.RequireComponent<Player>(source);
        return (GetKeycard(player) == null && pickable) ? "Picking the lock..." : null;
    }

    public void Interact(GameObject source)
    {
        Player player = ComponentUtil.RequireComponent<Player>(source);
        if (requiresKeycard) {
            Item keycard = GetKeycard(player);

            if (keycard == null && !pickable)
            {
                OpenDoorFailed();
                player.console.ShowMessage("Keycard Required. The lock cannot be picked.");
            }

            if (keycard == null && pickable)
            {
                SkillCheck.Result result = SkillCheck.DoCheck(player.character, new PickSkillTest(pickDifficulty));
                if (!result.success)
                {
                    if (result.complication)
                    {
                        pickable = false;
                        player.console.ShowMessage("Attempting to pick the lock has damaged the lock system. Keycard Required.");
                    }
                    else
                    {
                        player.console.ShowMessage("Failed to pick the lock.");
                    }
                    OpenDoorFailed();
                    return;
                }
            }
            else if(keycard == null)
            {
                OpenDoorFailed();
                return;
            }
        }

        _open();

        if (_OnOpen != null)
        {
            _OnOpen.Opened();
        }
    }

    private IEnumerator FixRenderOrder()
    {
        yield return new WaitForSeconds(1);
        _renderer.sortingOrder = 5;
    }

    private Item GetKeycard(Player source)
    {
        return source.inventory.items.Find(i => (i is Keycard) && (i as Keycard).color == keycardColor); 
    }

    private void ResetLight()
    {
        if (!requiresKeycard)
        {
            light2d.color = Color.white;
        }
        else {
            switch (keycardColor)
            {
                case KeycardColor.Blue:
                    light2d.color = Color.blue;
                    break;
                case KeycardColor.Green:
                    light2d.color = Color.green;
                    break;
                default:
                    light2d.color = Color.white;
                    break;
            }
        }
    }

    private void OpenDoorFailed()
    {
        light2d.color = Color.red;
        isFailed = true;
        failedTimer = failedTimerCooldown;
    }

    public AudioClip GetActionSound(GameObject source)
    {
        Player player = ComponentUtil.RequireComponent<Player>(source);
        return (GetKeycard(player) == null && pickable) ? sound : null;
    }

    private class PickSkillTest : SkillTest
    {
        public PickSkillTest(int difficulty) : base(SkillsEnum.UTILITY, difficulty, null, null, null, null)
        {
        }
    }
}
