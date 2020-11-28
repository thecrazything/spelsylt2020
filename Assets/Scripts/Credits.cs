using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private bool _fade = false;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 6)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime / 2), transform.position.z);
        }
        else if (!_fade)
        {
            _fade = true;
            StartCoroutine(AudioFadeOut.FadeOut(music, 5f));
        }
    }

    public void onClickExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
