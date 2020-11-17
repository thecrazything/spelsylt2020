using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutTextBehaviour : MonoBehaviour
{

    ConsoleBehaviour _console;
    public BlackoutBehaviour blackout;
    // Start is called before the first frame update
    void Start()
    {
        if (GameStatsService.Instance.gameStats.intro)
        {
            _console = GetComponent<ConsoleBehaviour>();
        }
    }

    public void WriteText(string text)
    {
        if (_console == null)
        {
            _console = GetComponent<ConsoleBehaviour>();
        }
        blackout.SetBlack();
        _console.WriteText(text, (val) =>
        {
            blackout.onFadeFinished += BlackOutFinished;
            blackout.FadeIn();
            GameStatsService.Instance.gameStats.intro = false;
            return false;
        });
    }

    void BlackOutFinished(bool faded)
    {
        blackout.onFadeFinished -= BlackOutFinished;
        _console.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
