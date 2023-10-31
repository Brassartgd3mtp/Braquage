using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTest : MonoBehaviour
{
    public GameObject UI_explications;
    private void Start()
    {
        UI_explications.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            UI_explications.gameObject.SetActive(true);
        }
    }
}
