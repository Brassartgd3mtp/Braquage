using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public GameObject groundMarker;

    private bool isActive;


    public LayerMask clickable;
    public LayerMask ground;
    void Start()
    {
        myCam = Camera.main;
        isActive = false;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {

                //Si je clique sur un objet "clickable"

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //Shift+click
                    UnitSelections.Instance.ShifClickSelect(hit.collider.gameObject);

                }
                else
                {
                    //Click normal
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                //Si je clique sur un objet non "clickable" et que Shift n'est pas utilis�
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }

        // Ajout des touches pour s�lectionner/d�s�lectionner
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Touche &
        {
            UnitSelections.Instance.NumKeySelect(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // Touche �
        {
            UnitSelections.Instance.NumKeySelect(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))  // Touche "
        {
            UnitSelections.Instance.NumKeySelect(1);
        }
    }
    void DisableGroundMarker()
    {
        // D�sactive le marqueur au bout de 2 secondes
        groundMarker.SetActive(false);

        // D�sactive le timer en passant isActive � false
        isActive = false;
    }
}
