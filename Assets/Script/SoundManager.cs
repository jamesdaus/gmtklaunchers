using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{

    public UnityEvent playsound;
    
    void Awake()
    {
        SoundPlayer sp = GetComponent<SoundPlayer>();
        //playsound.AddListener(sp.PlaySound());

        //transform.parent = null;
    }
    void Action()
    {
        
    }
}
