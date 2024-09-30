using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [Tooltip("This determines if the enemy should drop coins or not.")]
    public bool shouldDrop = true;
    [Tooltip("Enemy related stats, being the health, the attack speed based in cooldown and the bullet speed.")]
    public float enemyHP, attackSpeed, bulletSpeed;
    [Tooltip("The prefabs used to instantiate the bullet and coin.")]
    public GameObject bulletPrefab, coinPrefab;
    [Tooltip("Values that control the amount of coins that should drop and the chance they have of happening.")]
    public int dropCoins, chanceThreshold;

    private float currentHP, currentTime;
    private int currentChance;
    private EnemyCounter eC;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCounter>();
        currentChance = (int)Random.Range(0, chanceThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP < 0)
        {
            OnDeath(currentChance);
        }
    }

    void OnDeath(int chance)
    {
        switch (chance)
        {
            case int c when c == chanceThreshold:
                DropItems(dropCoins);
                break;

            default:
                break;
        }

        eC.enemyCount++;
        Destroy(gameObject);
    }

    void DropItems(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Instantiate(coinPrefab);
        }
    }
}
