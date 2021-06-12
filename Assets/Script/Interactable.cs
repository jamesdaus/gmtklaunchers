using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float destroyDelay = 0.25f;
    private const float TIMER_DEATH_TICK = 1f;
    private float currentTimer; // Depending on state, this timer tracks the time left for that state thing

    public enum ThingState
    {
        Watered,
        Dying,
        Growing,
        Dead
    }

    public string blop;
    public ThingState thingState;


    // Start is called before the first frame update
    void Start()
    {
        thingState = ThingState.Dying; // Start as dying
        currentTimer = TIMER_DEATH_TICK; // Start with death timer
    }

    // Update is called once per frame
    void Update()
    {
        //Timer, counts down and resets every X seconds
        if (currentTimer <= 0.0f)
        {
            if (thingState == ThingState.Dying)
            {
                currentTimer = TIMER_DEATH_TICK;
                SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
                Color tmpCol = spriteRenderer.color;

                spriteRenderer.color = new Color(tmpCol.r, tmpCol.g, tmpCol.b, Mathf.Lerp(tmpCol.a, 0, currentTimer * Time.deltaTime));

                if (spriteRenderer.color.a < 0f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            currentTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == blop)
        {
            Destroy(gameObject, destroyDelay);

        }
    }
}
