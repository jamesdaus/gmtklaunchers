using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private float timer;
    public GameObject[] objArray;

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
            
            Vector3 spot = new Vector3(Random.Range(-1,1), Random.Range(-2,2));

            int ct = 0;
            while (Physics2D.OverlapCircle(spot, .1f) != null && ct < 10) {
                spot = new Vector3(Random.Range(-1,1), Random.Range(-2,2));
                ct++;
            }
            
            if (ct != 10) {
                int arrayChoice = Random.Range(0, objArray.Length);
                GameObject newSpawn = Instantiate(objArray[arrayChoice], spot, Quaternion.identity);
                Transform newSprite = newSpawn.transform.GetChild(0);
                newSprite.GetComponent<SpriteRenderer>().sortingOrder = (int)(((-spot.y)+25) * 100); //Ordering madness
                newSprite.GetComponent<SpriteRenderer>().flipX = Random.Range(0,2) == 1 ? true : false;
                //newSprite.rotation = new Quaternion(0f, 0f, Random.Range(0f,0.3f), 0f);
            }
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
