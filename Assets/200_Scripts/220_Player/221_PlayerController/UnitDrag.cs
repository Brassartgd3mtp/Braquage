// Ce script gère la sélection d'unités par glisser-déposer (drag-and-drop) dans un projet Unity.
// Il utilise une boîte de sélection visuelle pour délimiter la zone de sélection et sélectionne les unités qui se trouvent à l'intérieur de cette zone.

using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    [SerializeField] RectTransform boxVisual;

    Rect selectionBox;  // La boîte de sélection en termes de coordonnées d'écran

    Vector2 startPosition;  // Position de début de la boîte de sélection
    Vector2 endPosition;    // Position de fin de la boîte de sélection

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

    // Dessine la boîte de sélection visuelle en fonction des positions de début et de fin
    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    // Met à jour les coordonnées de la boîte de sélection en fonction de la position de la souris
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

    // Sélectionne les unités qui se trouvent dans la boîte de sélection
    private void SelectUnits()
    {
        // Parcourt toutes les unités dans la liste d'unités
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            // Si l'unité est dans les limites de la boîte de sélection et son collider est activé
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)) && unit.GetComponent<Collider>().enabled)
            {
                // Ajoute l'unité à la sélection
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
