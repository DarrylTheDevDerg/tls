using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPoint;
    public GameObject bullet;
    public float minDmg, maxDmg;

    public enum AttackType
    {
        Meele,
        Range
    }

    public AttackType attackType;

    private float randmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateDamage(float min, float max)
    {
        float result;

        result = Random.Range(min, max);
        
        return result;
    }
}
