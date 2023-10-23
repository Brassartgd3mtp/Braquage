using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorScriptableObject", menuName = "SoundsScriptableObjects/Door")]
public class InteractibleScriptableObjects : ScriptableObject
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] door = new AudioClip[0];

    private void Start()
    {
        DoorManager.OnDoorOpen += PlayDoorOpen;
        DoorManager.OnDoorClose += PlayDoorClose;
    }

    public void PlayDoorOpen(Vector3 _position, float _intensity)
    {
        source.transform.position = _position;
        source.clip = door[0];
        source.PlayOneShot(door[0], _intensity);
        
    }

    public void PlayDoorClose(Vector3 _position, float _intensity)
    {
        source.transform.position = _position;
        source.clip = door[1];
        source.PlayOneShot(door[1], _intensity);
    }

    public class DoorManager : MonoBehaviour
    {
        public static event Action<Vector3, float> OnDoorOpen;
        public static event Action<Vector3, float> OnDoorClose;

        private void Update()
        {
            OnDoorOpen?.Invoke(Vector3.zero, 1.0f);
            OnDoorClose?.Invoke(Vector3.zero, 1.0f);
        }
    }
}
