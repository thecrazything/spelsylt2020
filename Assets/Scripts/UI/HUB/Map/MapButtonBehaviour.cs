using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonBehaviour : MonoBehaviour
{
    public string mapName;
    public Image image;
    public CanvasGroup icon;
    public Color selectedColor;
    public bool locked;
    MapBehaviour _map;
    Button _button;
    Color _deselectColor;

    // Start is called before the first frame update
    void Start()
    {
        _map = ComponentUtil.RequireComponent<MapBehaviour>(GameObject.Find("MapArea"));
        _button = ComponentUtil.RequireComponent<Button>(gameObject);
        _deselectColor = image.color;

        if (locked)
        {
            if (GameStatsService.Instance.gameStats.GetItems().Where(x => x is MapKey).Cast<MapKey>().Where(x => x.mapName == mapName).FirstOrDefault() != null)
            {
                locked = false;
            }
        }

        if (locked)
        {
            _button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _button.interactable = GameStatsService.Instance.selectedCharacter != null && !locked;

        if (_map.selectedMap == mapName)
        {
            image.color = selectedColor;
        } else
        {
            image.color = _deselectColor;
        }

        if (locked)
        {
            icon.alpha = 0.2f;
        }
        else
        {
            icon.alpha = 1f;
        }
    }

    public void onClick()
    {
        _map.selectedMap = mapName;
    }
}
