using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBehaviour : MonoBehaviour
{
    public Text titleText;
    public Text infoText;
    public Text startButtonText;
    
    public void SetInfoText(string text)
    {
        infoText.text = text;
    }

    public void SetTitleText(string text)
    {
        titleText.text = text;
    }

    public void SetButtonText(string text)
    {
        startButtonText.text = text;
    }
}
