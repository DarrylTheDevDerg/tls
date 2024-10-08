using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHPText : MonoBehaviour
{
    private Delusion d;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int)d.hp).ToString();
    }
}
