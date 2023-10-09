using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{

    public virtual void OnInteraction()
    {
        Debug.Log("Contact Parent");
    }
}
