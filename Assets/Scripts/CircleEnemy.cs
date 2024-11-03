using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CircleEnemy : MonoBehaviour
{
    public float distance, speed, minTime, maxTime, radius;
    public string playerTag, paramName;
    public bool animate;

    private NavMeshAgent agent;
    private Animator a;
    private GameObject player;
    private bool isApproaching = true;
    private float time, randTime, dTarget, angle;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag(playerTag);
        angle = Random.Range(0f, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        dTarget = Vector3.Distance(transform.position, player.transform.position);

        if (dTarget <= distance && isApproaching)
        {
            isApproaching = false;
        }

        if (dTarget > distance && isApproaching)
        {
            agent.SetDestination(player.transform.position);
        }

        if (!isApproaching)
        {
            time += Time.deltaTime;
        }

        if (time > randTime)
        {
            CircleAround();
            ResetTime();
        }
    }

    void CircleAround()
    {
        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        Vector3 destination = player.transform.position + offset;

        agent.SetDestination(destination);

        angle += Random.Range(-speed * Time.deltaTime, speed * Time.deltaTime);

        if (angle > Mathf.PI * 2f)
        {
            angle -= Mathf.PI * 2f;
        }
    }

    void ResetTime()
    {
        randTime = Random.Range(minTime, maxTime);

        time = 0;
    }
}
