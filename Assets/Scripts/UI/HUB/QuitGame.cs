using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    Image btnImg;
    Button btn;

    void Start()
    {
        btnImg = GetComponent<Image>();
        btn = GetComponent<Button>();
        Hide();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Show()
    {
        btn.enabled = true;
        btnImg.enabled = true;
    }

    public void Hide()
    {
        btn.enabled = false;
        btnImg.enabled = false;
    }

}
