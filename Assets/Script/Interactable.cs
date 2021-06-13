using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer[] renderers;

    private float destroyGameObjectDelay = 0.01f;

    private float growSpeed = 0.5f;

    private float dieSpeed = 0.05f;
    private float reviveSpeed = 0.05f;

    public enum ThingState
    {
        Reviving,
        Dying,
        Growing,
        Dead
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
        if (thingState == ThingState.Dead)
        {
            DestroyInteractable();
        }
        if (thingState == ThingState.Growing)
        {
            if (gameObject.transform.localScale.x <= 1.0f)
            {
                GrowInteractable(growSpeed);
            }
            else
            {
                thingState = ThingState.Dying;
            }
        }
        if (thingState == ThingState.Dying)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                if (renderer.color.a > 0.001f)
                {
                    KillInteractable(dieSpeed);
                }
                else
                {
                    thingState = ThingState.Dead;
                }
            }
        }
        if (thingState == ThingState.Reviving)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                if (renderer.color.a < 0.001f)
                {
                    ReviveInteractable(reviveSpeed);
                }
                else
                {
                    thingState = ThingState.Dying;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == pickedUpBy)
        {
            if (thingState == ThingState.Dying)
            {
                // Start reviving, when walking over dying interactable
                thingState = ThingState.Reviving;
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
            float startOpacity = renderer.color.a;

            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                Mathf.Lerp(startOpacity, 0f, alphaStep)
            );
        }
    }

    private void ReviveInteractable(float reviveSpeed)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            float alphaStep = Time.deltaTime * reviveSpeed;
            float startOpacity = renderer.color.a;

            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                Mathf.Lerp(0f, startOpacity, alphaStep)
            );
        }
    }

    private void DestroyInteractable()
    {
        Destroy(gameObject, destroyGameObjectDelay);
    }
}
