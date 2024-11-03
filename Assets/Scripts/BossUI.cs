using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public GameObject bossUi;
    public TextMeshProUGUI bossHP;

    private BossBehavior bH;
    private float maxHP;
    private float current; 
    private float percent;

    // Start is called before the first frame update
    void Start()
    {
        bH = FindObjectOfType<BossBehavior>();

        if (bH != null)
        {
            bossUi.SetActive(true);
        }
        else
        {
            bossUi.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bH != null)
        {
            if (current > maxHP)
            {
                maxHP = current;
            }

            current = bH.GetComponent<HealthSystem>().GetHP();
            percent = CalculatePercentage(current, maxHP);

            bossHP.text = ((int)percent).ToString();
        }
    }

    float CalculatePercentage(float current, float max)
    {
        if (max == 0)
        {
            return 0f;
        }

        return (current / max) * 100f;
    }
}
