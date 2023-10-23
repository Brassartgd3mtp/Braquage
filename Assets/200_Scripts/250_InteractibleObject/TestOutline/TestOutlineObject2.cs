using UnityEngine;

public class TestOutlineObject2 : MonoBehaviour
{
    [Header("Name property Shader")] //Nom des r�f�rences exact sur lesquels on veut agir dans le script
    public string nameMaterialOutline = "M_OutlineTest";
    public string propertyAlpha = "_Alpha";
    public string propertyColor = "_Outline_Color";

    // Remplace avec la nouvelle ou l'ancienne valeur d'alpha
    [Header("Alpha property")]
    public float originAlpha = 0f;
    public float newAlpha = 1f;

    [Header("Color property")] //Permet de d�finir les couleurs et l'intensit� lorsque la souris passe sur objet et qu'un "Player" est � proximit�
    public Color mouseColor;
    public Color proximtyColor;
    [Range(-10.0f, 100.0f)]
    public float intensityHDR = 10f;

    private new Renderer renderer;
    [SerializeField]
    private bool playerNearby = false;

    void Start()
    {
        // R�cup�re le Renderer
        renderer = GetComponent<Renderer>();

        // Assure que la valeur d'alpha initiale est correcte au d�but
        AppliquerAlpha(originAlpha);
        AppliquerCouleur(mouseColor, intensityHDR);
    }

    void OnMouseEnter()
    {
        if (!playerNearby)
        {
            // Modifie l'alpha et la couleur lorsque la souris est au-dessus
            AppliquerAlpha(newAlpha);
            AppliquerCouleur(mouseColor, intensityHDR);
        }
    }

    // Appel� lorsque la souris quitte l'objet
    void OnMouseExit()
    {
        if (!playerNearby)
        {
            // R�tablit l'alpha � sa valeur initiale
            AppliquerAlpha(originAlpha);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            AppliquerAlpha(newAlpha);
            AppliquerCouleur(proximtyColor, intensityHDR);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            AppliquerAlpha(originAlpha);
        }
    }

    // M�thode pour appliquer l'alpha � tous les mat�riaux de l'objet
    void AppliquerAlpha(float alpha)
    {
        // Parcourt tous les mat�riaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // V�rifie si la propri�t� Alpha existe dans ce mat�riau
            if (material.HasProperty(propertyAlpha))
            {
                // Modifie la propri�t� Alpha sp�cifique � ce mat�riau
                material.SetFloat(propertyAlpha, alpha);
            }
        }
    }
    void AppliquerCouleur(Color couleur, float intensite)
    {
        // Parcourt tous les mat�riaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // V�rifie si la propri�t� Couleur existe dans ce mat�riau
            if (material.HasProperty(propertyColor))
            {
                // Modifie la propri�t� Couleur sp�cifique � ce mat�riau
                material.SetColor(propertyColor, couleur * intensite);
            }
        }
    }

}
