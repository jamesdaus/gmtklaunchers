using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    private float timer;
    public GameObject obj;

    private const float TOTAL_TIMER = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = TOTAL_TIMER;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer, counts down and resets every 1 second
        if (timer <= 0.0f) {
            timer = TOTAL_TIMER;
            Instantiate(obj, new Vector3(0,1,0), Quaternion.identity);
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
