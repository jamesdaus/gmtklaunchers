using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer[] renderers;
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
    private bool isDoneFading = false;
    private bool isDoneGrowing = false;
    public float destroyDelay = 0.01f;
    public float fadeTime = 4f;
    public float growDuration = 4f;

    private Coroutine growCoroutine;
    private List<Coroutine> fadeOutCoroutines = new List<Coroutine>();
    private Coroutine fadeInCoroutine;

    public enum ThingState
    {
        Watered,
        Dying,
        Growing,
        Dead
    }

    public string blop;
    public ThingState thingState;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        thingState = ThingState.Dying; // Start as dying
    }

    // Update is called once per frame
    void Update()
    {
        if (thingState == ThingState.Dead)
        {
            Destroy(gameObject, destroyDelay);
        }
        else if (thingState == ThingState.Dying)
        {
            if (fadeOutCoroutines.Count == 0)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    fadeOutCoroutines.Add(StartCoroutine(FadeTo(renderer, 0f, fadeTime)));
                }
            }
        }
        else if (thingState == ThingState.Growing)
        {
            foreach (Coroutine fadeOutCoroutine in fadeOutCoroutines)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutines.Clear();

            foreach (SpriteRenderer renderer in renderers)
            {
                fadeInCoroutine = StartCoroutine(FadeTo(renderer, 1.0f, fadeTime));
            }

            float randGrowthFactor = scales[Random.Range(0, scales.Length)];

            // Only start scaling, if it's even a bigger number
            if (randGrowthFactor > gameObject.transform.localScale.x)
                growCoroutine = StartCoroutine(GrowTo(randGrowthFactor, growDuration));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == blop)
        {
            thingState = ThingState.Growing;
        }
    }

    IEnumerator GrowTo(float targetScale, float duration)
    {
        isDoneGrowing = false;
        Vector3 startScale = gameObject.transform.localScale;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);

            startScale.x = Mathf.Lerp(startScale.x, targetScale, blend);
            startScale.y = Mathf.Lerp(startScale.y, targetScale, blend);

            gameObject.transform.localScale = startScale;

            yield return null;
            isDoneGrowing = true;
        }

        if (isDoneGrowing)
        {
            thingState = ThingState.Dying;
        }
    }

    IEnumerator FadeTo(SpriteRenderer renderer, float targetOpacity, float duration)
    {
        isDoneFading = false;

        Color color = renderer.color;
        float startOpacity = color.a;

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);

            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            renderer.color = color;

            yield return null;
            isDoneFading = true;
        }

        if (isDoneFading)
        {
            if (color.a <= 0f)
            {
                Debug.Log("dead");
                thingState = ThingState.Dead;
            }
            else
            {
                Debug.Log("dying, color.a: " + color.a);
                thingState = ThingState.Dying;
            }
        }
    }

    /*IEnumerator FadeOutAndKill(float aTime)
    {
        foreach (SpriteRenderer sr in renderers)
        {
            float alpha = sr.color.a;
            Color colOrig = sr.color;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(colOrig.r, colOrig.g, colOrig.b, Mathf.Lerp(alpha, 0, t));
                sr.color = newColor;
                yield return null;
            }
        }

        thingState = ThingState.Dead;
    }*/

    /*
    private IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        Color tmp = spriteRenderer.color;
        spriteRenderer.color = tmp;
        float _progress = 0.0f;

        while (_progress < 1)
        {

            Color _tmpColor = spriteRenderer.color;
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(_tmpColor.r, _tmpColor.g, _tmpColor.b, Mathf.Lerp(tmp.a, 0, _progress));
            _progress += Time.deltaTime;
            yield return null;
        }
    }
    */
}
