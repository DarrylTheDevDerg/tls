using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [Tooltip("This determines if the enemy should drop coins or not.")]
    public bool shouldDrop = true;
    [Tooltip("The prefabs used to instantiate the bullet and coin.")]
    public GameObject coinPrefab;
    [Tooltip("Values that control the amount of coins that should drop and the chance they have of happening.")]
    public int dropCoins, chanceThreshold, maxChance;

    private int currentChance;
    private EnemyCounter eC;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCounter>();
        currentChance = (int)Random.Range(0, maxChance);
    }

    public void OnDeath()
    {
        switch (currentChance)
        {
           case int c when c >= chanceThreshold:
                DropItems(dropCoins);
                break;

           default:
                break;
        }

        eC.enemyCount++;
        Destroy(gameObject);
    }

    public void DropItems(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
