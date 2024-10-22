using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManipulation : MonoBehaviour
{
    public TextMeshProUGUI[] pauseMenuStrings;
    public string replacement;
    public int chanceNeed, maxChance;
    public float timeNeeded;
    public CanvasGroup obstructions;
    
    private Delusion d;
    private int dLevel, cChance;
    private string[] origStrings;
    private float time, fontsize;

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();

        origStrings = new string[pauseMenuStrings.Length];
        fontsize = pauseMenuStrings[0].fontSize;
        StoreStrings();
    }

    // Update is called once per frame
    void Update()
    {
        dLevel = d.RetrieveRCLevel();

        UIObstruction(d.hp);

        if (dLevel == 3)
        {
            time += Time.deltaTime;
            if (time >= timeNeeded)
            {
                RandomChance(0, maxChance);
                CheckRandom(cChance);
            }
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
            pauseMenuStrings[i].text = origStrings[i];
        }
    }

    void StoreStrings()
    {
        for (int i = 0; i < pauseMenuStrings.Length; i++)
        {
            origStrings[i] = pauseMenuStrings[i].text.ToString();
        }
    }

    void UIObstruction(float value)
    {
        float normalizedValue = Mathf.Clamp01(value / d.RetrieveMaxHP()) * (Mathf.Clamp01(value / d.RetrieveMaxHP()) * 5);
        obstructions.alpha = 1f - normalizedValue;
    }

    int RandomChance(int minChance, int maxChance)
    {
        cChance = Random.Range(minChance, maxChance);

        return cChance;
    }

    void CheckRandom(int chance)
    {
        if (chance > chanceNeed && Time.timeScale > 0f)
        {
            StringManip();
        }

        if (chance > chanceNeed)
        {
            EpilepsyWarning();
        }

        if (chance <  chanceNeed && Time.timeScale > 0f)
        {
            ReverseManip();

            for (int i = 0; i < pauseMenuStrings.Length; i++)
            {
                pauseMenuStrings[i].fontSize = fontsize;
            }
        }
    }

    void EpilepsyWarning()
    {
        float chance = Random.Range(0, maxChance);

        switch (chance)
        {
            case float c when c <= maxChance / 3:
                for (int i = 0; i < pauseMenuStrings.Length; i++)
                {
                    pauseMenuStrings[i].fontSize = fontsize;
                }
                break;

            case float c when c >= maxChance / 3:
                for (int i = 0; i < pauseMenuStrings.Length; i++)
                {
                    pauseMenuStrings[i].fontSize = fontsize / 1.5f;
                }
                break;

            case float c when c >= maxChance / 1.5f:
                for (int i = 0; i < pauseMenuStrings.Length; i++)
                {
                    pauseMenuStrings[i].fontSize = fontsize / 2;
                }
                break;
        }
    }
}
