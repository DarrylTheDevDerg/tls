using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeManager : MonoBehaviour
{
    public string aggroName, playerTag;
    public float minDmg, maxDmg, threshold;

    private bool isAggressive;
    private Animator a;
    private float randomDmg, time;
    private Delusion d;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        d = FindObjectOfType<Delusion>();
        player = GameObject.FindGameObjectWithTag(playerTag);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAggressive && other.GetComponent<DealDamage>() == true)
        {
            isAggressive = true;
            a.SetBool(aggroName, isAggressive);
        }

        if (isAggressive && other.GetComponent<Collider>().CompareTag(playerTag))
        {
            d.hp -= randomDmg;
            Destroy(gameObject);
        }
    }

    public void RandomizeDamage(float min, float max)
    {
        randomDmg = Random.Range(minDmg, maxDmg);
    }
}
