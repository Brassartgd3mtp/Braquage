using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardDeath : MonoBehaviour
{
    public GameObject guard;
    private GuardController guardController;
    private new Renderer renderer;
    private Animator animatorGuard;
    private NavMeshAgent agent;

    [Header("Name property Shader")] //Nom des r�f�rences exact sur lesquels on veut agir dans le script
    public string nameMaterialDissolve = "M_Dissolve";
    public string propertyCutoff = "_Cutoff_Height";


    [Header("Cutoff property")]
    public float originCutoff = -1f;
    public float newCutoff = 2f;
    public float deathDuration = 3.0f;


    void Start()
    {
        // R�cup�re le Renderer
        renderer = GetComponent<Renderer>();
        guardController = GetComponentInParent<GuardController>();
        animatorGuard = GetComponentInParent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();

        ApplyCutoff(originCutoff);
    }
    private void Update()
    {
        if(guardController != null && guardController.guardDead)
        {
            StartCoroutine(ProcessDeath());
            animatorGuard.SetBool("Immobilisation", true);
            agent.speed = 0f;
        }
    }

    void ApplyCutoff(float cutoff)
    {
        // Parcourt tous les mat�riaux de l'objet
        foreach (Material material in renderer.materials)
        {
            // V�rifie si la propri�t� Cutoff existe dans ce mat�riau
            if (material.HasProperty(propertyCutoff))
            {
                // Modifie la propri�t� Cutoff sp�cifique � ce mat�riau
                material.SetFloat(propertyCutoff, cutoff);
            }
        }
    }

    private IEnumerator ProcessDeath()
    {
        foreach (Material material in renderer.materials)
        {
            // V�rifie si la propri�t� Cutoff existe dans ce mat�riau
            if (material.HasProperty(propertyCutoff))
            {
                // R�cup�re la valeur initiale de la variable du shader graph
                float currentShaderValue = material.GetFloat(propertyCutoff);

                // Le temps �coul�
                float elapsed_time = 0.0f;

                while (elapsed_time < deathDuration)
                {
                    // Calculer la nouvelle valeur interpol�e
                    float newShaderValue = Mathf.Lerp(currentShaderValue, newCutoff, elapsed_time / deathDuration);

                    // Appliquer la nouvelle valeur au shader
                    material.SetFloat(propertyCutoff, newShaderValue);

                    // Mettre � jour le temps �coul�
                    elapsed_time += Time.deltaTime;

                    // Attendre la prochaine frame
                    yield return null;
                }
            }
            // � la fin de la transition, tu peux d�truire le garde ou effectuer d'autres actions
            Destroy(guard);
        }
    }
}