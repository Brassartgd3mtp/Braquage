using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCinematic : MonoBehaviour
{
    public GameObject Cinematic;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Cinematic.gameObject.SetActive(false);
        }
    }
}
