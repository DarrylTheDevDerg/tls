using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInRepeatedly : MonoBehaviour
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

    IEnumerator FadeLoop()
    {
        int currentCycle = 0;

        while (currentCycle < fadeCycles)
        {
            if (isFadingIn)
            {
                yield return StartCoroutine(Fade(0f, 1f));  // Fades in
            }
            else
            {
                yield return StartCoroutine(Fade(1f, 0f));  // Fades out
            }

            isFadingIn = !isFadingIn;

            yield return new WaitForSeconds(fadeDelay);

            // Increment only when it's fully faded out (transparent)
            if (!isFadingIn)
            {
                currentCycle++;
            }
        }

        // Once cycles are done, ensure the object fades out before destroying
        yield return StartCoroutine(Fade(1f, 0f));  // Final fade-out to transparency
        Destroy(gameObject);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color spriteColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);
            yield return null;
        }

        // Ensure the final color is set to the exact endAlpha
        spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, endAlpha);
    }
}
