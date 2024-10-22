using System.Collections;
using UnityEngine;

public class FadeInSprite : MonoBehaviour
{
    // Dynamic value to control how quickly the sprite fades in
    public float fadeDuration = 1.0f;

    // Reference to the SpriteRenderer
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the fade-in process
        StartCoroutine(FadeIn());
    }

    // Coroutine to gradually fade in the sprite
    IEnumerator FadeIn()
    {
        // Time elapsed since the start of the fade
        float elapsedTime = 0.0f;

        // While the elapsed time is less than the total fade duration
        while (elapsedTime < fadeDuration)
        {
            // Calculate the new alpha based on the time passed
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // Set the sprite color with the updated alpha
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the sprite is fully visible at the end of the fade
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
}
