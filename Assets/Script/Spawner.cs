using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private float[] scales = {
        0.3f, 0.3f, 0.3f, 0.3f,
        0.4f, 0.4f, 0.4f,
        0.5f, 0.5f,
        0.6f,
        0.7f,
        0.8f,
        0.9f,
        1.0f
    };

    private float spawn_timer;
    private float game_timer;
    public GameObject[] objArray;

    private const float TOTAL_SPAWN_TIMER = 1.0f;
    private const float TOTAL_GAME_TIMER = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawn_timer = TOTAL_SPAWN_TIMER;
        game_timer = TOTAL_GAME_TIMER;
    }

    // Update is called once per frame
    void Update()
    {

        if (game_timer <= 0.0f) {
            //THIS IS THE TOTAL TIME OF THE GAME RUNNING OUT, ADD SCORE DISPLAY AND RELOAD MENU HERE!
            
            SceneManager.LoadScene(0);
        }
        else {
            game_timer -= Time.deltaTime;
        }


        //Timer, counts down and resets every 1 second
        if (spawn_timer <= 0.0f)
        {
            spawn_timer = TOTAL_SPAWN_TIMER;

            Vector3 spot = new Vector3(Random.Range(-5, 5), Random.Range(-4, 4));

            int ct = 0;
            while (Physics2D.OverlapCircle(spot, .1f) != null && ct < 10)
            {
                spot = new Vector3(Random.Range(-5, 5), Random.Range(-4, 4));
                ct++;
            }

            if (ct != 10)
            {
                int arrayChoice = Random.Range(0, objArray.Length);
                GameObject newSpawn = Instantiate(objArray[arrayChoice], spot, Quaternion.identity);
                Transform newSprite = newSpawn.transform.GetChild(0);
                newSprite.GetComponent<SpriteRenderer>().sortingOrder = (int)(((-spot.y) + 25) * 100); //Ordering madness
                newSprite.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 1 ? true : false;
                newSprite.transform.Rotate(new Vector3(0, 0, Random.Range(-10, 10)));
                //float newScale = scales[Random.Range(0, scales.Length)];
                //newSpawn.transform.localScale = new Vector3(newScale, newScale, 1);
            }
        }
        else
        {
            spawn_timer -= Time.deltaTime;
        }
    }
}
