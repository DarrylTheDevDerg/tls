using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCooldownManager : MonoBehaviour
{
    public static GlobalCooldownManager Instance;  // Singleton to allow easy access from HealingItem scripts

    [SerializeField] public float cooldownDuration = 10f;  // Cooldown time in seconds
    private float cooldownTimeRemaining;
    private bool isOnCooldown;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        // Decrement the cooldown timer if it’s active
        if (isOnCooldown)
        {
            cooldownTimeRemaining -= Time.deltaTime;
            if (cooldownTimeRemaining <= 0f)
            {
                cooldownTimeRemaining = 0f;
                isOnCooldown = false;  // Reset cooldown
            }
        }
    }

    // Call this method to initiate the cooldown
    public void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimeRemaining = cooldownDuration;
    }

    // Public getter for isOnCooldown
    public bool IsCooldownActive()
    {
        return isOnCooldown;
    }

    public int GetCooldown()
    {
        return (int)cooldownTimeRemaining;
    }
}
