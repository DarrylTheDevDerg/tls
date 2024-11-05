using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVariety : MonoBehaviour
{
    private SpriteRenderer re;

    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<SpriteRenderer>();

        re.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
    }
}
