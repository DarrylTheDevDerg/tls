using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CirclingEnemy : MonoBehaviour
{
    public string targetTag = "Target"; // Tag for the target object
    public float stoppingDistance = 2f; // Distance to start circling
    public float minCircleTime = 2f;    // Minimum time to circle
    public float maxCircleTime = 5f;    // Maximum time to circle
    public float circleSpeed = 30f;     // Speed of circling rotation in degrees per second
    public float circleRadius = 2f;     // Radius from target
    public string speedParam = "Speed"; // Name of the Animator parameter for speed

    private NavMeshAgent agent;
    private Transform target;
    private bool isCircling = false;
    private float circleTimer;
    private bool clockwise;
    private Animator animator;           // Animator to control movement speed parameter

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        FindTarget();
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= stoppingDistance && !isCircling)
        {
            StartCoroutine(StartCircling());
        }
        else if (!isCircling)
        {
            agent.SetDestination(target.position);
            UpdateAnimatorSpeed(agent.velocity.magnitude);
        }
    }

    void FindTarget()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
    }

    IEnumerator StartCircling()
    {
        isCircling = true;
        circleTimer = Random.Range(minCircleTime, maxCircleTime);
        clockwise = Random.value > 0.5f; // Randomly choose circling direction

        agent.ResetPath();
        agent.updatePosition = false;
        agent.updateRotation = false;

        while (circleTimer > 0)
        {
            float rotationAngle = circleSpeed * Time.deltaTime * (clockwise ? 1 : -1);

            // Calculate circling position
            Vector3 offset = (transform.position - target.position).normalized * circleRadius;
            offset = Quaternion.Euler(0, rotationAngle, 0) * offset;

            Vector3 circlePosition = target.position + offset;

            // Smoothly move toward the calculated circling position
            transform.position = Vector3.Lerp(transform.position, circlePosition, Time.deltaTime * circleSpeed / 10f);

            UpdateAnimatorSpeed(Vector3.Distance(transform.position, circlePosition) * 10f);
            circleTimer -= Time.deltaTime;
            yield return null;
        }

        agent.updatePosition = true;
        agent.updateRotation = true;
        isCircling = false;
    }

    void UpdateAnimatorSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat(speedParam, speed);
        }
    }
}

