using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer[] renderers;
    public float destroyDelay = 0.01f;
    public float fadeOutTime = 4f;

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
            StartCoroutine(FadeOutAndKill(fadeOutTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == blop)
        {
            thingState = ThingState.Dead;
        }
    }

    IEnumerator FadeOutAndKill(float aTime)
    {
        float alphaDiff = 0f;

        foreach (SpriteRenderer sr in renderers)
        {
            float alpha = sr.color.a;
            Color colOrig = sr.color;

            if (alpha < 1.0f)
            {
                alphaDiff = 1.0f - alpha;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(colOrig.r, colOrig.g, colOrig.b, Mathf.Lerp(alpha, 0, t));
                sr.color = newColor;
                yield return null;
            }
        }

        thingState = ThingState.Dead;
    }

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
