using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoDelay : MonoBehaviour
{
    public GameObject NewDisplay;
    public GameObject OldDisplay;

    private bool AlreadyDone = false;
    private float Timer = 4;
    public bool Condition = false;
    private void Start()
    {
        NewDisplay.SetActive(false);
    }
    void Update()
    {
        if (!Condition)
        {
            if (!OldDisplay.activeSelf)
            {
                if (Timer <= 0)
                {
                    if (!AlreadyDone)
                    {
                        NewDisplay.gameObject.SetActive(true);
                        AlreadyDone = true;
                    }
                }
                else
                {
                    Timer -= Time.deltaTime;
                }
            }
        }
        else
        {
            if(OldDisplay.activeSelf)
            {
                Condition = false;
            }
        }
    }
}
