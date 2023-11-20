using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielSkip : MonoBehaviour
{
    public GameObject TutorielDisplay;
    public GameObject Characters;
    public GameObject MapButton;
    public GameObject Booty;
    public GameObject TutorielZone;
    private void Update()
    {
        //simple fonction qui peremt de désactiver directement tout ce qui est lié au tutoriel
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TutorielDisplay.gameObject.SetActive(false);
            TutorielZone.gameObject.SetActive(false);
            Characters.gameObject.SetActive(true);
            MapButton.gameObject.SetActive(true);
            Booty.gameObject.SetActive(true);
        }
    }
}
