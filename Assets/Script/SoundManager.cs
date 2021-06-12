using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    string[] allTags;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSource;
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
