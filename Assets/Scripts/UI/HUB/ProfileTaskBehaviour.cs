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
    public Button expeditionButton;
    public HubBehaviour hub;
    public ProfileBehaviour pfB;
    public Text taskButtonText;

    private Text task1Text;
    private Text task2Text;
    private Text task3Text;

    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
        task1Text = task1Button.GetComponentInChildren<Text>();
        task2Text = task2Button.GetComponentInChildren<Text>();
        task3Text = task3Button.GetComponentInChildren<Text>();
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

        task1Button.interactable = hub.avalibleTasks[0].doer == null;
        task2Button.interactable = hub.avalibleTasks[1].doer == null;
        task3Button.interactable = hub.avalibleTasks[2].doer == null;
        task1Text.text = hub.avalibleTasks[0].skillTest.name;
        task2Text.text = hub.avalibleTasks[1].skillTest.name;
        task3Text.text = hub.avalibleTasks[2].skillTest.name;
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

    public void onClickExpeditionButton()
    {
        unsetFromTasks();
        GameStatsService.Instance.selectedCharacter = pfB.character;
        taskDropdown.gameObject.SetActive(false);
        taskButtonText.text = "E X P E D I T I O N";
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
    }
}
