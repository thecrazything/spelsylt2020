using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartExpeditionBehaviour : MonoBehaviour
{
    private Text _text;
    private Button _button;
    private MapBehaviour _map;
    public HubBehaviour hub;
    public BlackoutBehaviour blackout;
    public ConsoleBehaviour uiConsole;
    public InvetoryWrapper inventory;
    public ConfirmBehaviour dialog;
    public AudioSource mainMusic;

    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
        _text = ComponentUtil.RequireComponent<Text>(transform.Find("Text").gameObject);
        _map = ComponentUtil.RequireComponent<MapBehaviour>(GameObject.Find("MapArea"));
        _button = ComponentUtil.RequireComponent<Button>(gameObject);

        inventory.Hide();
        dialog.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatsService.Instance.selectedCharacter == null)
        {
            _text.text = TextConstants.NEXT_DAY;
            _button.interactable = true;
        } 
        else
        {
            if (_map.selectedMap == null || _map.selectedMap == "")
            {
                _text.text = TextConstants.SELECT_EXPEDITION;
                _button.interactable = false;
            }
            else
            {
                _text.text = TextConstants.PREPARE_EXPIDITION;
                _button.interactable = true;
            }
        }
    }

    public void onClick()
    {
        if (GameStatsService.Instance.selectedCharacter == null)
        {
            ShowConfirmDialog();
        }
        else
        {
            ShowPrepareScreen();
        }
    }

    public void onPrepareDone()
    {
        ShowConfirmDialog();
    }

    public void onClickConfirm()
    {
        if (GameStatsService.Instance.selectedCharacter != null)
        {
            GameStatsService.Instance.SetPreparedInventory(inventory.plrInv.playerInventory);
        }
        inventory.Hide();
        dialog.gameObject.SetActive(false);
        ChangeScene();
    }

    public void onClickCancel()
    {
        inventory.Hide();
    }

    public void onDialogCancel()
    {
        dialog.gameObject.SetActive(false);
    }

    private void ShowConfirmDialog()
    {
        dialog.SetButtonText(GameStatsService.Instance.selectedCharacter == null ? TextConstants.NEXT_DAY : TextPrintAnimation.spaceLetters(TextConstants.START_EXPIDITION));
        dialog.SetTitleText(GameStatsService.Instance.selectedCharacter == null ? TextConstants.START_NEW_DAY_QUESTION : TextConstants.YES);
        dialog.gameObject.SetActive(true);
    }

    private void ShowPrepareScreen()
    {
        inventory.Show();
    }


    private void ChangeScene()
    {
        AudioFadeOut.FadeOut(mainMusic, 2f);
        uiConsole.ShutDown();
        blackout.FadeOut();
        blackout.onFadeFinished += doChangeScene;
    }

    private void doChangeScene(bool faded)
    {
        GameStatsService.Instance.gameStats.hubTasks = hub.avalibleTasks;
        blackout.onFadeFinished -= doChangeScene;
        if (_map.selectedMap != null && GameStatsService.Instance.selectedCharacter != null)
        {
            SceneManager.LoadScene(_map.selectedMap);
        }
        else
        {
            GameStatsService.Instance.gameStats.expeditionComplete = true;
            SceneManager.LoadScene("Hub");
        }
    }
}
