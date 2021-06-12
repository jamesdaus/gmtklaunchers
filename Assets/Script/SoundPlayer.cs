using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] AudioSource pickupSound;

    [SerializeField] float DestroyTimer = 3f;

    void Start()
    {
        pickupSound = GameObject.FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
    }


    void OnCollisionEnter2D(Collision2D other)
    {

        if(gameObject.tag == transform.tag && gameObject != this)
        {
            Debug.Log(other.gameObject.name);
            Debug.Log(other.transform.tag + " " + gameObject.name);
            PlayAudio();
        }
    }

    private void PlayAudio()
    {
        if (audioClips.Length == 0) return;

        int randomAudioClip = Random.Range(0, audioClips.Length);

        pickupSound.PlayOneShot(audioClips[0]);
        
        transform.parent = null;
        Destroy(gameObject, DestroyTimer);
    }

}
