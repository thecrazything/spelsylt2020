using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContiniueButtonBehaviour : MonoBehaviour
{
    private Text _text;
    private Button _button;
    private MapBehaviour _map;
    public HubBehaviour hub;
    public BlackoutBehaviour blackout;
    public ConsoleBehaviour uiConsole;

    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
        _text = ComponentUtil.RequireComponent<Text>(transform.Find("Text").gameObject);
        _map = ComponentUtil.RequireComponent<MapBehaviour>(GameObject.Find("MapArea"));
        _button = ComponentUtil.RequireComponent<Button>(gameObject);
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
                _text.text = TextConstants.START_EXPIDITION;
                _button.interactable = true;
            }
        }
    }

    public void onClick()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
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
