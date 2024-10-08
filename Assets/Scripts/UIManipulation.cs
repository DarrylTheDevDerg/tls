using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManipulation : MonoBehaviour
{
    public TextMeshProUGUI[] pauseMenuStrings;
    public string replacement;
    public int chanceNeed, maxChance;
    
    private Delusion d;
    private int dLevel, cChance;
    private TextMeshProUGUI[] origStrings;

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();

        origStrings = pauseMenuStrings;
    }

    // Update is called once per frame
    void Update()
    {
        dLevel = d.RetrieveRCLevel();

        if (dLevel == 3)
        {
            StringManip();
        }
        if (dLevel < 3)
        {
            ReverseManip();
        }
    }

    void StringManip()
    {
        for (int i = 0; i < pauseMenuStrings.Length; i++)
        {
            pauseMenuStrings[i].text = replacement;
        }
    }

    void ReverseManip()
    {
        for (int i = 0; i < pauseMenuStrings.Length; i++)
        {
            pauseMenuStrings[i].text = origStrings[i].text;
        }
    }

    void UIObstruction()
    {

    }
}
