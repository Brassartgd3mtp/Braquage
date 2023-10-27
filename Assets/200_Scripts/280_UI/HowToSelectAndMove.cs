using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    public GameObject MainSelectAndMoveWindow;
    public GameObject firstSelectAndMoveWindow;
    public GameObject secondSelectAndMoveWindow;
    public GameObject ContinueText;
    private bool CanNext = false;
    private float Cooldown = 2;
    private bool timerCanPlay = false;
    
    public GameObject CameraControlersWindow;

    private void Start()
    {
        #region HowToSelectAndMove
        MainSelectAndMoveWindow.gameObject.SetActive(true);
        firstSelectAndMoveWindow.gameObject.SetActive(true);
        secondSelectAndMoveWindow.gameObject.SetActive(false);
        ContinueText.SetActive(true);
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                firstSelectAndMoveWindow.gameObject.SetActive(false);
                secondSelectAndMoveWindow.gameObject.SetActive(true);
                timerCanPlay = true;
                ContinueText.SetActive(false);
                
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
                secondSelectAndMoveWindow.gameObject.SetActive(false);
                MainSelectAndMoveWindow.gameObject.SetActive(false);
            }
            
        }
        #endregion
    }
}
