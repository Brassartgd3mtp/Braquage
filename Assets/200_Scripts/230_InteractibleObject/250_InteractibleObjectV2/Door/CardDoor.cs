using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDoor : CrochetDoorV2
{
    private bool FirstTime = false;
    public string NameCardAnimation;
    void Start()
    {
        
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
}
