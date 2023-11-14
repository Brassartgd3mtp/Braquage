// Ce script g�re la s�lection d'unit�s par glisser-d�poser (drag-and-drop) dans un projet Unity.
// Il utilise une bo�te de s�lection visuelle pour d�limiter la zone de s�lection et s�lectionne les unit�s qui se trouvent � l'int�rieur de cette zone.

using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    [SerializeField] RectTransform boxVisual;

    Rect selectionBox;  // La bo�te de s�lection en termes de coordonn�es d'�cran

    Vector2 startPosition;  // Position de d�but de la bo�te de s�lection
    Vector2 endPosition;    // Position de fin de la bo�te de s�lection

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        //Quand je clique
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();

        }

        //Quand je glisse/drag
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //Quand je relache le clique souris
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    // Dessine la bo�te de s�lection visuelle en fonction des positions de d�but et de fin
    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    // Met � jour les coordonn�es de la bo�te de s�lection en fonction de la position de la souris
    void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            //Dragging left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            //Dragging right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startPosition.y)
        {
            //Dragging down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            //Dragging up
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;

        }
    }

    // S�lectionne les unit�s qui se trouvent dans la bo�te de s�lection
    private void SelectUnits()
    {
        // Parcourt toutes les unit�s dans la liste d'unit�s
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            // Si l'unit� est dans les limites de la bo�te de s�lection et son collider est activ�
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)) && unit.GetComponent<Collider>().enabled)
            {
                // Ajoute l'unit� � la s�lection
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
