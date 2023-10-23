using UnityEngine;

public class TestOutlineObject2 : MonoBehaviour
{
    [Header("Name property Shader")] //Nom des références exact sur lesquels on veut agir dans le script
    public string nameMaterialOutline = "M_OutlineTest";
    public string propertyAlpha = "_Alpha";
    public string propertyColor = "_Outline_Color";

    // Remplace avec la nouvelle ou l'ancienne valeur d'alpha
    [Header("Alpha property")]
    public float originAlpha = 0f;
    public float newAlpha = 1f;

    [Header("Color property")] //Permet de définir les couleurs et l'intensité lorsque la souris passe sur objet et qu'un "Player" est à proximité
    public Color mouseColor;
    public Color proximtyColor;
    [Range(-10.0f, 100.0f)]
    public float intensityHDR = 10f;

    private new Renderer renderer;
    [SerializeField]
    private bool playerNearby = false;

    void Start()
    {
        // Récupère le Renderer
        renderer = GetComponent<Renderer>();

        // Assure que la valeur d'alpha initiale est correcte au début
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

    // Appelé lorsque la souris quitte l'objet
    void OnMouseExit()
    {
        if (!playerNearby)
        {
            // Rétablit l'alpha à sa valeur initiale
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

    // Méthode pour appliquer l'alpha à tous les matériaux de l'objet
    void AppliquerAlpha(float alpha)
    {
        // Parcourt tous les matériaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // Vérifie si la propriété Alpha existe dans ce matériau
            if (material.HasProperty(propertyAlpha))
            {
                // Modifie la propriété Alpha spécifique à ce matériau
                material.SetFloat(propertyAlpha, alpha);
            }
        }
    }
    void AppliquerCouleur(Color couleur, float intensite)
    {
        // Parcourt tous les matériaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // Vérifie si la propriété Couleur existe dans ce matériau
            if (material.HasProperty(propertyColor))
            {
                // Modifie la propriété Couleur spécifique à ce matériau
                material.SetColor(propertyColor, couleur * intensite);
            }
        }
    }

}
