using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDoor : CrochetDoorV2
{
    private bool FirstTime = false;
    public string NameCardAnimation;
    public GameObject Text;

    private bool playerNearby = false;
    void Start()
    {
        Text.gameObject.SetActive(false);
    }


    void Update()
    {
        if (FirstTime == false)
        {
            if (requiresAccessCard == false)
            {
                doorAnimation.Play(NameCardAnimation);
                FirstTime = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Text.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Text.gameObject.SetActive(false);
        }
    }
}