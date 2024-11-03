using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVariety : MonoBehaviour
{
    public float r, g, b;
    public float r2, g2, b2;

    private SpriteRenderer re;

    // Start is called before the first frame update
    void Start()
    {
        re = GetComponent<SpriteRenderer>();

        re.color = new Color(Random.Range(r, r2), Random.Range(g, g2), Random.Range(g, g2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
