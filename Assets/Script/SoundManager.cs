using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    string[] allTags;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSource;
    [SerializeField] UnityEvent playsound;
    
    void Awake()
    {
        transform.parent = null;
    }
    void Start()
    {
        audioSource = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
