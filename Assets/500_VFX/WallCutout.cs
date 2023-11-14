using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCutout : MonoBehaviour
{
    [SerializeField]
    private List<Transform> targetObjects; // Utilise une liste au lieu d'un seul objet

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        for (int t = 0; t < targetObjects.Count; ++t)
        {
            Transform targetObject = targetObjects[t];

            Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
            cutoutPos.y /= (Screen.width / Screen.height);

            Vector3 offset = targetObject.position - transform.position;
            RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

            for (int i = 0; i < hitObjects.Length; ++i)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

                for (int m = 0; m < materials.Length; ++m)
                {
                    materials[m].SetVector("_CutoutPos", cutoutPos);
                    materials[m].SetFloat("_CutoutSize", 0.1f);
                    materials[m].SetFloat("_FalloffSize", 0.05f);
                }
            }
        }
    }
}
