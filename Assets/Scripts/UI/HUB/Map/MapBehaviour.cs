using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBehaviour : MonoBehaviour
{
    public Text notAssignedText;
    public CanvasGroup mapGroup;

    public string selectedMap;

    // Start is called before the first frame update
    void Start()
    {
        ComponentUtil.ArgumentNotNull(notAssignedText);
    }

    // Update is called once per frame
    void Update() {
        if (GameStatsService.Instance.selectedCharacter == null)
        {
            notAssignedText.gameObject.SetActive(true);
            mapGroup.alpha = 0.5f;
        }
        else
        {
            notAssignedText.gameObject.SetActive(false);
            mapGroup.alpha = 1.0f;
        }
    }
}
