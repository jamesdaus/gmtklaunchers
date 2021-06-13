using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip audioClip;
    
    private void Awake()
    {
        audioSource.PlayOneShot(audioClip);

        var instance = FindObjectsOfType<SoundManager>().Length;
        if(instance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
    /*int numMusicPlayer = FindObjectsOfType<Sound_Manager>().Length;
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
}
        else
{
    DontDestroyOnLoad(gameObject);
}*/

}
