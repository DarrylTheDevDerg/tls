using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    public float maxDistance = 5f; // Maximum distance from the initial position
    public string paramName;

    [Range(0, 10)]
    public float moveInterval = 2f; // Time interval for movement

    private Vector3 initialPosition, previousPosition;
    private NavMeshAgent navMeshAgent;

    private Animator a;
    private Rigidbody rb;
    private EnemyAttack eA;

    void Start()
    {
        initialPosition = transform.position; // Store the initial position
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        a = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        eA = GetComponent<EnemyAttack>();

        if (navMeshAgent.enabled == true && !eA.GetAnimBool())
        {
            // Start the movement coroutine
            InvokeRepeating(nameof(MoveToRandomPosition), 0, moveInterval);
        }

        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If you're using Rigidbody, you can get velocity instead
        float moveSpeed = (transform.position - previousPosition).magnitude / Time.deltaTime;

        // Set the 'MoveSpeed' parameter in the Animator
        a.SetFloat(paramName, moveSpeed);

        // Update previous position for the next frame's calculation
        previousPosition = transform.position;

        moveSpeed = Mathf.Clamp(moveSpeed, 0, navMeshAgent.speed);

    }

    void MoveToRandomPosition()
    {
        // Generate a random point within a sphere defined by maxDistance
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;

        // Calculate the new destination
        Vector3 newDestination = initialPosition + randomDirection;

        // Check if the destination is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, maxDistance, NavMesh.AllAreas) || navMeshAgent.enabled == true)
        {
            navMeshAgent.SetDestination(hit.position); // Set the destination to the hit position
        }
    }
}
