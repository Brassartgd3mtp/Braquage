using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class TutorielTwoDisplaySkip : MonoBehaviour
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
        
        mainWindow.SetActive(true);
        firstWindow.gameObject.SetActive(true);
        secondWindow.gameObject.SetActive(false);
        ContinueText.SetActive(false);
    }

    void Update()
    {
        // Permet de pouvoir passer d'un display à un autre avec une latence avant de pouvoir 
        // skipper la deuxième et donc le bouton espace arrive après la fin du cooldown. 

        if (!CanNext)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                ContinueText.gameObject.SetActive(true);
            }
            if (ContinueText.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    firstWindow.gameObject.SetActive(false);
                    secondWindow.gameObject.SetActive(true);
                    timerCanPlay = true;
                    ContinueText.gameObject.SetActive(false);
                    Cooldown = 3;

                }
            }
        }
        //Le timer permet de mettre un temps entre les skip, sinon ils se font en même temps
        //et donc la deuxième display n'apparait pas.
        if (timerCanPlay)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                CanNext = true;
                ContinueText.gameObject.SetActive(true);
            }
        }
        if (CanNext)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CanNext = false;
                Cooldown = 2;
                timerCanPlay = false;
                mainWindow.gameObject.SetActive(false);
                // On reset tout à la fin au cas où il faudrait le réutiliser.
            }
            
        }
    }
}
