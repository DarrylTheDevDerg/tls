using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTracking : MonoBehaviour
{
    public int maxEnemies;
    public UnityEvent stuff;
    public bool timed;
    public float threshold;

    private EnemyCounter eC;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timed)
        {
            time += Time.deltaTime;
        }

        Check();
    }

    void Check()
    {
        if (eC.enemyCount >= maxEnemies)
        {
            if (timed && time > threshold)
            {
                stuff.Invoke();
            }
            else
            {
                stuff.Invoke();
            }
        }
    }
}
