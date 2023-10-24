using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsDatabase", menuName = "SoundsScriptableObjects/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [SerializeField] private AudioClip[] door = new AudioClip[0];
}
