using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    SpriteRenderer[] renderers;
    public Sprite[] sprites;

    private float destroyGameObjectDelay = 0.01f;

    private float growSpeed = 0.5f;

    private float dieSpeed = 0.5f;
    private float reviveSpeed = 0.5f;

    private float goldTimer = 15f;

    public TextMeshPro score;

    public enum ThingState
    {
        Reviving,
        Dying,
        Growing,
        Dead,
        Golden
    }

    public string pickedUpBy;
    public ThingState thingState;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Start()
    {
        thingState = ThingState.Growing; // Start as dying
    }

    void Update()
    {
        if (goldTimer >= 0) {
            
            goldTimer -= Time.deltaTime;

            if (thingState == ThingState.Dead)
            {
                DestroyInteractable();
            }
            else if (thingState == ThingState.Growing)
            {
                if (gameObject.transform.localScale.x <= 1.0f)
                {
                    GrowInteractable(growSpeed);
                }
                else
                {
                    thingState = ThingState.Dying;
                    renderers[0].sprite = sprites[1];
                }
            }
            else if (thingState == ThingState.Dying)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    if (renderer.color.a > 0.01f)
                    {
                        KillInteractable(dieSpeed);
                    }
                    else
                    {
                        thingState = ThingState.Dead;
                    }
                }
            }
            else if (thingState == ThingState.Reviving)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    if (renderer.color.a < .99f)
                    {
                        ReviveInteractable(reviveSpeed);
                    }
                    else
                    {
                        thingState = ThingState.Dying;
                        renderers[0].sprite = sprites[1];
                    }
                }
            }
        }
        else if (thingState != ThingState.Golden) {
            renderers[0].sprite = sprites[2];
            renderers[0].color = new Color(renderers[0].color.r, renderers[0].color.g, renderers[0].color.b, 255);
            thingState = ThingState.Golden;
            Debug.Log("GOLD!");
            //ADD THE SCORE HERE! IT WILL ONLY HAPPEN ONCE!
        }
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == pickedUpBy)
        {
            if (thingState == ThingState.Dying)
            {
                // Start reviving, when walking over dying interactable
                thingState = ThingState.Reviving;
                renderers[0].sprite = sprites[0];
            }
        }
    }

    private void GrowInteractable(float growSpeed)
    {
        gameObject.transform.localScale += new Vector3(
            Time.deltaTime * growSpeed,
            Time.deltaTime * growSpeed,
            0
        );
    }

    private void KillInteractable(float dieSpeed)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            float alphaStep = Time.deltaTime * dieSpeed;

            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                Mathf.Lerp(renderer.color.a, 0f, alphaStep)
            );
        }
    }

    private void ReviveInteractable(float reviveSpeed)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            float alphaStep = Time.deltaTime * reviveSpeed;

            float startOpacity;
            if (renderer.sprite.name == "shadow") {
                startOpacity = 0.5f;
            }
            else {
                startOpacity = 1.0f;
            }

            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                Mathf.Lerp(renderer.color.a, startOpacity, alphaStep)
            );
        }
    }

    private void DestroyInteractable()
    {
        Destroy(gameObject, destroyGameObjectDelay);
    }
}
