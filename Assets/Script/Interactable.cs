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

    private List<Coroutine> growCoroutines = new List<Coroutine>();
    private List<Coroutine> fadeOutCoroutines = new List<Coroutine>();
    private List<Coroutine> fadeInCoroutines = new List<Coroutine>();

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
            Debug.Log("DEAD");
            Destroy(gameObject, destroyDelay);
        }
        else if (thingState == ThingState.Dying)
        {
            Debug.Log("DYING");
            if (fadeOutCoroutines.Count == 0)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    FadeOut(renderer);
                }
            }
        }
        else if (thingState == ThingState.Growing)
        {
            Debug.Log("GROWING");
            ClearCoroutines(fadeOutCoroutines);

            if (fadeInCoroutines.Count == 0)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    FadeIn(renderer);
                }
            }

            float randGrowthFactor = scales[Random.Range(0, scales.Length)];

            // Only start scaling, if it's a bigger number
            if (randGrowthFactor > gameObject.transform.localScale.x)
            {
                growCoroutines.Add(StartCoroutine(GrowTo(randGrowthFactor, growDuration)));
            }
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

    private void ClearCoroutines(List<Coroutine> coroutines)
    {
        foreach (Coroutine coroutine in coroutines)
        {
            StopCoroutine(coroutine);
        }
        coroutines.Clear();
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
                thingState = ThingState.Dead;
            }
            else
            {
                thingState = ThingState.Dying;
            }
        }
    }

    private void FadeOut(SpriteRenderer renderer)
    {
        fadeOutCoroutines.Add(StartCoroutine(FadeTo(renderer, 0f, fadeTime)));
    }

    private void FadeIn(SpriteRenderer renderer)
    {
        fadeInCoroutines.Add(StartCoroutine(FadeTo(renderer, 1f, fadeTime)));
    }
}
