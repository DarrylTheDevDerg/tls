using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    public float threshold, resisHold;
    public float[] multiplier = new float[4];
    public string animBool;

    private EnemyAttack eA;
    private CircleEnemy cE;
    private HealthSystem hS;
    private Animator a;
    private NavMeshAgent agent;

    private float time, origFire, resisTime, graceTime;
    private int stage;
    private bool inRange = true;
    private SpriteRenderer sprite;
    private Color origColor;
    private Color newColor;
    private float origspeed;

    // Start is called before the first frame update
    void Start()
    {
        eA = GetComponent<EnemyAttack>();
        cE = GetComponent<CircleEnemy>();
        hS = GetComponent<HealthSystem>();
        a = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        sprite = GetComponent<SpriteRenderer>();
        origColor = sprite.color;
        newColor = new Color(origColor.r, 0.5f, 0.5f);
        origspeed = agent.speed;

        origFire = eA.attackDelay;
        SwitchDistance(inRange);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (resisHold > 0)
        {
            resisTime += Time.deltaTime;
        }

        if (graceTime >= 0)
        {
            graceTime -= Time.deltaTime;
        }

        if (graceTime < 0)
        {
            graceTime = 0;
        }

        if (resisHold > 0 && resisTime > 0.85f)
        {
            resisHold -= 0.0725f;
            resisTime = 0;
            sprite.color = Color.Lerp(origColor, newColor, resisHold);
        }

        StageCheck();

        if (time > threshold)
        {
            if (inRange)
            {
                inRange = false;
                eA.attackType = EnemyAttack.AttackType.Meele;
            }
            else
            {
                inRange = true;
                eA.attackType = EnemyAttack.AttackType.Range;
            }

            SwitchDistance(inRange);

            time = 0;
        }
    }

    void SwitchDistance(bool state)
    {
        switch (state)
        {
            case true:
                cE.distance = 15f;
                cE.radius = 15f;
                cE.speed = cE.speed * multiplier[stage];
                cE.minTime = cE.minTime / multiplier[stage];
                cE.maxTime = cE.maxTime / multiplier[stage];
                eA.attackDelay = origFire / multiplier[stage];
                agent.speed = agent.speed * multiplier[stage];
                a.SetBool(animBool, true);
                break;

            case false:
                cE.distance = 3f;
                cE.radius = 3f;
                cE.speed = (cE.speed / 1.5f) * multiplier[stage];
                cE.minTime = cE.minTime / (multiplier[stage] / 1.2f);
                cE.maxTime = cE.maxTime / (multiplier[stage] / 1.2f);
                eA.attackDelay = origFire / (multiplier[stage] / 2);
                agent.speed = origspeed;
                a.SetBool(animBool, false);
                break;

        }
    }

    void StageCheck()
    {
        float hp = hS.GetHP();
        float max = 0;

        if (hS.GetHP() > max)
        {
            max = hS.GetHP();
        }

        float percent = CalculatePercentage(hp, max);

        switch (percent)
        {
            case float h when h > 75f:
                stage = 0;
                break;

            case float h when h > 50f && h <= 75f:
                stage = 1;
                break;

            case float h when h > 25f && h <= 50f:
                stage = 2;
                break;

            case float h when h < 25f:
                stage = 3;
                break;

            default:
                print("????????????????");
                break;
        }
    }

    void DamageResistance()
    {
        DealDamage damage = FindObjectOfType<DealDamage>();

        if (resisHold > 0)
        {
            damage.dealDmg -= resisHold;
        }

        if (graceTime > 0 && resisTime < 0.85f && resisHold > 0)
        {
            resisHold += 0.05f;
            graceTime = 1.45f;
            resisTime = 0;
            sprite.color = Color.Lerp(origColor, newColor, resisHold);
        }

        if (resisHold > 2.5f)
        {
            resisHold = 2.5f;
        }

        if (resisHold < 0f)
        {
            resisHold = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DealDamage damage = other.GetComponent<DealDamage>();

        if (damage.RetrieveSearchTag() == "Enemy")
        {
            if (graceTime == 0f)
            {
                graceTime = 1.45f;
            }

            if (graceTime > 0f)
            {
                graceTime = 1.45f;
                DamageResistance();
                resisHold += 0.001f;
            }
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
