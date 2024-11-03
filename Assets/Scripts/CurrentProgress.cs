using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentProgress : MonoBehaviour
{
    public TextMeshProUGUI display;

    private EnemyCounter eC;
    private EnemyTracking eT;
    private int current, max;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCounter>();
        eT = FindObjectOfType<EnemyTracking>();

        if (eC == null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        max = eT.maxEnemies;
        current = Mathf.RoundToInt(CalculatePercentage(eC.enemyCount, max));

        Check();
    }

    float CalculatePercentage(float current, float max)
    {
        if (max == 0)
        {
            return 0f;
        }

        return (current / max) * 100f;
    }

    void Check()
    {
        display.text = current.ToString();
    }
}
