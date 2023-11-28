using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisplayScript : MonoBehaviour
{
    public GameObject InputGO;
    void Start()
    {
        
    }

    void Update()
    {
        if (InputGO)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InputGO.gameObject.SetActive(false);
            }
        }
    }
}
