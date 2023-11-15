using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTuto : MonoBehaviour
{
    public GameObject Display;
    private bool AlreadyActived = false;
    void Start()
    {
        Display.gameObject.SetActive(false);        
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!AlreadyActived)
        {
            if (other.CompareTag("Player"))
            {
                Display.gameObject.SetActive(true);
                AlreadyActived = true;
            }
        }
        
    }
}
