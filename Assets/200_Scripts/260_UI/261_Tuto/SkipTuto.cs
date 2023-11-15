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


    // Update is called once per frame
    void Update()
    {
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
