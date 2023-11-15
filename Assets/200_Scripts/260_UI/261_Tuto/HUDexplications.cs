using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorielHUD_Explications : MonoBehaviour
{
    public GameObject mainDisplay;
    public GameObject CharactersRole;
    public GameObject CharactersActions;
    public GameObject mapOutline;
    public GameObject mapTuto;
    public GameObject bountyOutline;
    public GameObject bountyTuto;
    public GameObject continueTxt;
    public GameObject blurDisplay;

    public GameObject ExempleAction01;
    public GameObject ExempleAction02;
    public GameObject ExempleAction03;

    private float Cooldown = 2;
    private bool NextToCharactersRoles = true;
    private bool NextToCharactersActions = false;
    private bool NextToBounty = false;
    private bool NextToMap = false;

    public GameObject bountyDisplay;
    public GameObject mapButton;
    public GameObject CharactersDisplay;

    void Start()
    {
        blurDisplay.gameObject.SetActive(true);
        bountyDisplay.gameObject.SetActive(false);
        mapButton.gameObject.SetActive(false);
        CharactersDisplay.gameObject.SetActive(true);
        CharactersRole.gameObject.SetActive(true);
        CharactersActions.gameObject.SetActive(false);
        continueTxt.gameObject.SetActive(false);

        //On s'assure qu'avant de débuter la séquence, les exemple d'actions soient bien désactivés
        ExempleAction01.gameObject.SetActive(false);
        ExempleAction02.gameObject.SetActive(false);
        ExempleAction03.gameObject.SetActive(false);
    }

    void Update()
    {

        if (NextToCharactersRoles)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                continueTxt.gameObject.SetActive(true);
            }
            if (continueTxt.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    continueTxt.gameObject.SetActive(false);
                    NextToCharactersActions = true;
                    NextToCharactersRoles = false;
                    CharactersRole.gameObject.SetActive(false);
                    CharactersActions.gameObject.SetActive(true);
                    Cooldown = 2;

                    ExempleAction01.gameObject.SetActive(true);
                    ExempleAction02.gameObject.SetActive(true);
                    ExempleAction03.gameObject.SetActive(true);

                }
            }
        }
        if (NextToCharactersActions)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                continueTxt.gameObject.SetActive(true);
            }
            if (continueTxt.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    continueTxt.gameObject.SetActive(false);
                    NextToBounty = true;
                    NextToCharactersActions = false;
                    CharactersActions.gameObject.SetActive(false);
                    bountyDisplay.gameObject.SetActive(true);
                    bountyOutline.gameObject.SetActive(true);
                    bountyTuto.gameObject.SetActive(true);
                    Cooldown = 2;

                    ExempleAction01.gameObject.SetActive(false);
                    ExempleAction02.gameObject.SetActive(false);
                    ExempleAction03.gameObject.SetActive(false);
                }
            }
        }
        if (NextToBounty)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                continueTxt.gameObject.SetActive(true);
            }
            if (continueTxt.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Cooldown = 2;
                    NextToMap = true;
                    NextToBounty = false;
                    continueTxt.gameObject.SetActive(false);
                    bountyOutline.gameObject.SetActive(false);
                    bountyTuto.gameObject.SetActive(false);
                    mapButton.gameObject.SetActive(true);
                    mapOutline.gameObject.SetActive(true);
                    mapTuto.gameObject.SetActive(true);
                }
            }
        }
        if (NextToMap)
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
            if (Cooldown <= 0)
            {
                continueTxt.gameObject.SetActive(true);
            }
            if (continueTxt.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    mainDisplay.gameObject.SetActive(false);
                    mapOutline.gameObject.SetActive(false);
                    mapTuto.gameObject.SetActive(false);
                    blurDisplay.gameObject.SetActive(false);
                }
            }
        }
    }
}
