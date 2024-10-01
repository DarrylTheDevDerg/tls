using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
    public float knockbackForce = 10f;     // How hard you knock them back
    public float knockbackDuration = 0.5f; // How long the knockback lasts
    private NavMeshAgent navMeshAgent;
    private bool isKnockedBack = false;

    private Vector3 knockbackDirection;
    private float knockbackTimer;
    private Vector3 sourcePosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        sourcePosition = transform.position;

        if (isKnockedBack)
        {
            // Continue applying knockback force using the velocity of the NavMeshAgent
            navMeshAgent.velocity = knockbackDirection * knockbackForce;

            // Countdown the knockback duration
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0f)
            {
                // End knockback and reset the velocity to resume normal movement
                isKnockedBack = false;
            }
        }
    }

    public void ApplyKnockback()
    {
        if (!isKnockedBack)
        {
            // Calculate knockback direction away from the source of the hit
            knockbackDirection = (transform.position - sourcePosition).normalized;

            // Set knockback duration
            knockbackTimer = knockbackDuration;
            isKnockedBack = true;
        }
    }
}
