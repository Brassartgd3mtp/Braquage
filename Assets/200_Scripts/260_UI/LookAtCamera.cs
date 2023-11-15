using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera cam;
    void Awake()
    {
        cam = Camera.main;
    }


    void Update()
    {
        //Fait en sorte que l'UI (Canvas) se tourne dans la direction de la camera
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}
