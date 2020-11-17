using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonBehaviour : MonoBehaviour
{
    public string mapName;
    public Image image;
    public Color selectedColor;
    MapBehaviour _map;
    Button _button;
    Color _deselectColor;

    // Start is called before the first frame update
    void Start()
    {
        _map = ComponentUtil.RequireComponent<MapBehaviour>(GameObject.Find("MapArea"));
        _button = ComponentUtil.RequireComponent<Button>(gameObject);
        _deselectColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        _button.interactable = GameStatsService.Instance.selectedCharacter != null;

        if (_map.selectedMap == mapName)
        {
            image.color = selectedColor;
        } else
        {
            image.color = _deselectColor;
        }
    }

    public void onClick()
    {
        _map.selectedMap = mapName;
    }
}
