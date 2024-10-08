using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimagesEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // Reference to your main object's SpriteRenderer
    public float afterimageLifetime = 0.5f;  // How long each afterimage lasts
    public float afterimageSpawnDelay = 0.1f;  // Delay between afterimage spawns
    public int maxAfterimages = 5;  // Max number of afterimages allowed at once

    private List<GameObject> afterimages = new List<GameObject>();  // Stores current afterimages
    private float timeSinceLastAfterimage;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Create afterimages at regular intervals
        timeSinceLastAfterimage += Time.deltaTime;

        if (timeSinceLastAfterimage >= afterimageSpawnDelay)
        {
            SpawnAfterimage();
            timeSinceLastAfterimage = 0f;
        }
    }

    void SpawnAfterimage()
    {
        // Ensure we don't exceed the maximum number of afterimages
        if (afterimages.Count >= maxAfterimages)
        {
            // Remove the oldest afterimage
            Destroy(afterimages[0]);
            afterimages.RemoveAt(0);
        }

        // Create a new afterimage object
        GameObject afterimage = new GameObject("Afterimage");
        SpriteRenderer afterimageRenderer = afterimage.AddComponent<SpriteRenderer>();

        // Set the sprite and position to match the current object
        afterimageRenderer.sprite = spriteRenderer.sprite;
        afterimageRenderer.color = new Color(1f, 1f, 1f, 0.5f);  // Optional: make the afterimage semi-transparent
        afterimage.transform.position = transform.position;
        afterimage.transform.rotation = transform.rotation;
        afterimage.transform.localScale = transform.localScale;

        // Add the afterimage to the list
        afterimages.Add(afterimage);

        // Destroy the afterimage after a set time
        Destroy(afterimage, afterimageLifetime);
    }
}
