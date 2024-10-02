using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private Delusion d;
    private Slider slider;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();
        slider = GetComponent<Slider>();

        health = d.hp;
        slider.maxValue = d.hp;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = d.hp;
    }
}
