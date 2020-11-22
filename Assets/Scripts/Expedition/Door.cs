using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour, IInteractable
{
    bool _isOpen;
    Animator animator;
    Collider2D _collider;
    
    public GameObject shadowObject;
    public KeycardColor keycardColor;
    public bool requiresKeycard = false;

    public bool pickable = false;
    public float picktime = 8f;
    public int pickDifficulty = 2;

    void Start() {
        animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
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
                    return;
                }
            }
            else if(keycard == null)
            {
                return;
            }
        }

        _isOpen = true;
        shadowObject.SetActive(false);
        animator.SetBool("IsOpen", true);
        _collider.enabled = false;
    }

    private Item GetKeycard(Player source)
    {
        return source.inventory.items.Find(i => (i is Keycard) && (i as Keycard).color == keycardColor); 
    }

    private class PickSkillTest : SkillTest
    {
        public PickSkillTest(int difficulty) : base(SkillsEnum.UTILITY, difficulty, null, null, null, null)
        {
        }
    }
}
