using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInRepeatdly : MonoBehaviour
{
    public float fadeDuration, fadeDelay;
    public int fadeCycles;
    public bool onStart;

    private bool isFadingIn;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (onStart)
        {
            StartCoroutine(FadeLoop());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeLoop()
    {
        int currentCycle = 0;

        while (currentCycle < fadeCycles)
        {
            if (isFadingIn)
            {
                yield return StartCoroutine(Fade(0f, 1f));
            }
            else
            {
                yield return StartCoroutine(Fade(1f, 0f));
            }

            isFadingIn = !isFadingIn;

            yield return new WaitForSeconds(fadeDelay);

            if (!isFadingIn)
            {
                currentCycle++;
            }
        }


        if (currentCycle >= fadeCycles)
        {
            yield return StartCoroutine(Fade(0f, 1f));

            if (spriteRenderer.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        Color spriteColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            spriteRenderer.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);

            yield return null;
        }

        spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, endAlpha);
    }
}
