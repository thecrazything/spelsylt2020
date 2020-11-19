using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryWrapper : MonoBehaviour
{
    public HubInvetoryBehaviour hubInv;
    public HubPlayerInvetoryBehaviour plrInv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
        hubInv.Show();
        plrInv.Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        hubInv.Hide();
        plrInv.Hide();
    }
}
