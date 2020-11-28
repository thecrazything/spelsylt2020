using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    public GameObject needle;

    public void SetProgress(float percentage)
    {
        needle.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 180 - (180 * percentage));
    }
}
