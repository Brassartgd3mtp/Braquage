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
                //Si je clique sur un objet non "clickable" et que Shift n'est pas utilisé
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
        //Si je clique droit fait apparaitre un symbole au sol
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(true);

                // Active le timer en passant isActive à true
                isActive = true;
                Invoke("DisableGroundMarker", 2.0f); // Appelle la méthode DisableGroundMarker après 2 secondes
            }
        }
    }
    void DisableGroundMarker()
    {
        // Désactive le marqueur au bout de 2 secondes
        groundMarker.SetActive(false);

        // Désactive le timer en passant isActive à false
        isActive = false;
    }
}
