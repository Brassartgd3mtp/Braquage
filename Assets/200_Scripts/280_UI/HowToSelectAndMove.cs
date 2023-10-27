using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    public GameObject mainWindow;
    public GameObject firstWindow;
    public GameObject secondWindow;
    public GameObject ContinueText;
    private bool CanNext = false;
    private float Cooldown = 2;
    private bool timerCanPlay = false;
    

    private void Start()
    {
        #region HowToSelectAndMove
        mainWindow.gameObject.SetActive(true);
        firstWindow.gameObject.SetActive(true);
        secondWindow.gameObject.SetActive(false);
        ContinueText.SetActive(false);
        #endregion
    }

    void Update()
    {
        #region HowToSelectAndMove
        ///
        /// Permet de pouvoir passer d'un display à un autre avec une latence avant de pouvoir skipper la deuxième. 
        ///


        if (CanNext == false)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                ContinueText.SetActive(true);
            }
            if (ContinueText.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    firstWindow.gameObject.SetActive(false);
                    secondWindow.gameObject.SetActive(true);
                    timerCanPlay = true;
                    ContinueText.SetActive(false);
                    Cooldown = 2;

                }
            }
            
        }
        if (timerCanPlay == true)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                CanNext = true;
                ContinueText.SetActive(true);
            }
        }
        if (CanNext == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                secondWindow.gameObject.SetActive(false);
                mainWindow.gameObject.SetActive(false);
            }
            
        }
        #endregion
    }
}
