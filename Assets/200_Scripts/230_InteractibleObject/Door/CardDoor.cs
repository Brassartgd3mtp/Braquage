using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDoor : ObjectDoorV2
{
    //private bool FirstTime = false;
    public string aNameCardAnimation;
    public GameObject Text;

    private bool playerNearby = false;
    void Start()
    {
        Text.gameObject.SetActive(false);

        doorAnimation = GetComponent<Animation>();
        if (doorAnimation == null)
        {
            Debug.LogError("Animation component is not assigned to ObjectDoorV2.");
            return;
        }
    }


    void Update()
    {
        //if (FirstTime == false)
        //{
        //    if (requiresAccessCard == false)
        //    {
        //        doorAnimation.Play(NameCardAnimation);
        //        FirstTime = true;
        //    }
        //}
    }


    public override void OnInteraction(GameObject interactablePlayer)
    {
        base.OnInteraction(gameObject);
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