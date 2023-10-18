using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject mapWindow;
    private void Start()
    {
        
    }

    private void Update()
    {
        //Open or close the map window when player hit Tab 
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            mapWindow.gameObject.SetActive(!mapWindow.gameObject.activeSelf);
        }
        //Close the window when player hit Escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            mapWindow.gameObject.SetActive(false);
        }
    }
}
