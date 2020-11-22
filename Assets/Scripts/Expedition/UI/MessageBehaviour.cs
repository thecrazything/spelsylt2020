using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaviour : MonoBehaviour
{
    public float messageDelayTime = 3f;
    public ConsoleBehaviour console;
    public CanvasGroup consoleWrapper;

    void Start()
    {
        Hide();
    }

    public void ShowMessage(string msg)
    {
        StartCoroutine(Fade(true));
        console.WriteText(msg, (x) => {
            StartCoroutine(DelayThenHide());
            return false;
        });
    }

    private void Hide()
    {
        consoleWrapper.alpha = 0f;
    }

    private IEnumerator DelayThenHide()
    {
        yield return new WaitForSeconds(messageDelayTime);
        StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool fadeIn)
    {
        var startAlpha = consoleWrapper.alpha;
        if (fadeIn)
        {
            while(consoleWrapper.alpha < 1)
            {
                consoleWrapper.alpha += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (consoleWrapper.alpha > 0)
            {
                consoleWrapper.alpha -= Time.deltaTime;
                yield return null;
            }
        }

        consoleWrapper.alpha = fadeIn ? 1f : 0f;
    }
}
