using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManager : MonoBehaviour
{
    public string aggroName, playerTag, aggroState;
    public float minDmg, maxDmg, threshold, minSpeed, maxSpeed;

    private bool isAggressive;
    private Animator a;
    private float randomDmg, time, randomSpeed;
    private Delusion d;
    private GameObject player;
    private FadeInRepeatedly fR;
    private SpriteRenderer sR;
    private Color spriteColor;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        d = FindObjectOfType<Delusion>();
        fR = GetComponent<FadeInRepeatedly>();
        sR = GetComponent<SpriteRenderer>();

        spriteColor = sR.color;

        player = GameObject.FindGameObjectWithTag(playerTag);
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > threshold)
        {
            RandomizeDamage(minDmg, maxDmg);
            time = 0f;
        }

        if (a.GetBool(aggroState) == true)
        {
            spriteColor = new Color(1f, spriteColor.g, spriteColor.b, 1f);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, randomSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAggressive && other.GetComponent<DealDamage>() == true)
        {
            isAggressive = true;
            fR.StopAllCoroutines();
            spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1f);

            sR.color = spriteColor;
            a.SetBool(aggroName, isAggressive);
        }

        if (isAggressive && other.GetComponent<Collider>().CompareTag(playerTag))
        {
            d.LoseHP(randomDmg);
            Destroy(gameObject);
        }
    }

    public void RandomizeDamage(float min, float max)
    {
        randomDmg = Random.Range(minDmg, maxDmg);
    }

    void AggressiveState()
    {
        a.SetBool(aggroState, true);
    }
}
