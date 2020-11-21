using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileTaskBehaviour : MonoBehaviour
{
    public Image taskDropdown;
    public Button task1Button;
    public Button task2Button;
    public Button task3Button;
    public Button task4Button;
    public Button expeditionButton;
    public Button restButton;
    public HubBehaviour hub;
    public ProfileBehaviour pfB;
    public Text taskButtonText;

    private Text task1Text;
    private Text task2Text;
    private Text task3Text;
    private Text task4Text;

    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
        task1Text = task1Button.GetComponentInChildren<Text>();
        task2Text = task2Button.GetComponentInChildren<Text>();
        task3Text = task3Button.GetComponentInChildren<Text>();
        task4Text = task4Button.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (GameStatsService.Instance.selectedCharacter != null)
        {
            expeditionButton.interactable = false;
        }
        else
        {
            expeditionButton.interactable = true;
        }

        task1Button.interactable = !CheckIfRelatedToSelf(hub.avalibleTasks[0]) && hub.avalibleTasks[0].doer == null;
        task2Button.interactable = !CheckIfRelatedToSelf(hub.avalibleTasks[1]) && hub.avalibleTasks[1].doer == null;
        task3Button.interactable = !CheckIfRelatedToSelf(hub.avalibleTasks[2]) && hub.avalibleTasks[2].doer == null;
        task1Text.text = hub.avalibleTasks[0].skillTest.name;
        task2Text.text = hub.avalibleTasks[1].skillTest.name;
        task3Text.text = hub.avalibleTasks[2].skillTest.name;
        if (hub.avalibleTasks.Length > 3)
        {
            task4Button.interactable = !CheckIfRelatedToSelf(hub.avalibleTasks[3]) && hub.avalibleTasks[3].doer == null;
            task4Text.text = hub.avalibleTasks[3].skillTest.name;
        }
        else
        {
            task4Button.gameObject.SetActive(false);
        }
    }

    bool CheckIfRelatedToSelf(HubTask task)
    {
        if (task.skillTest is HealCharacterSkillTest)
        {
            return (task.skillTest as HealCharacterSkillTest).GetCharacter() == pfB.character;
        }
        return false;
    }

    public void onClickDropdown()
    {
        taskDropdown.gameObject.SetActive(true);
        if (GameStatsService.Instance.selectedCharacter == pfB.character)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
        unsetFromTasks();
        taskButtonText.text = "A S S I G N   T A S K";
    }

    public void onClickTask1Button()
    {
        unsetFromTasks();
        taskDropdown.gameObject.SetActive(false);
        if (GameStatsService.Instance.selectedCharacter == pfB.character)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
        taskButtonText.text = hub.avalibleTasks[0].skillTest.name;
        hub.avalibleTasks[0].doer = pfB.character;
    }

    public void onClickTask2Button()
    {
        unsetFromTasks();
        taskDropdown.gameObject.SetActive(false);
        if (GameStatsService.Instance.selectedCharacter == pfB.character)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
        taskButtonText.text = hub.avalibleTasks[1].skillTest.name;
        hub.avalibleTasks[1].doer = pfB.character;
    }

    public void onClickTask3Button()
    {
        unsetFromTasks();
        taskDropdown.gameObject.SetActive(false);
        if (GameStatsService.Instance.selectedCharacter == pfB.character)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
        taskButtonText.text = hub.avalibleTasks[2].skillTest.name;
        hub.avalibleTasks[2].doer = pfB.character;
    }

    public void onClickTask4Button()
    {
        unsetFromTasks();
        taskDropdown.gameObject.SetActive(false);
        if (GameStatsService.Instance.selectedCharacter == pfB.character)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
        taskButtonText.text = hub.avalibleTasks[3].skillTest.name;
        hub.avalibleTasks[3].doer = pfB.character;
    }

    public void onClickExpeditionButton()
    {
        unsetFromTasks();
        GameStatsService.Instance.selectedCharacter = pfB.character;
        taskDropdown.gameObject.SetActive(false);
        taskButtonText.text = "E X P E D I T I O N";
    }

    public void onClickRest()
    {
        unsetFromTasks();
        taskDropdown.gameObject.SetActive(false);
        taskButtonText.text = "R E S T";
        if (!pfB.hub.restingCharacters.Contains(pfB.character))
        {
            pfB.hub.restingCharacters.Add(pfB.character);
        }
    }

    private void unsetFromTasks()
    {
        for (var i = 0; i < hub.avalibleTasks.Length; i++)
        {
            if (hub.avalibleTasks[i].doer == pfB.character)
            {
                hub.avalibleTasks[i].doer = null;
            }
        }

        if(pfB.hub.restingCharacters.Contains(pfB.character))
        {
            pfB.hub.restingCharacters.Remove(pfB.character);
        }
    }
}
