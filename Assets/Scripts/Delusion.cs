using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DelusionObjects
{
    public int LevelNeeded;
    public GameObject[] IllusionaryObjects;
}
public class Delusion : MonoBehaviour
{
    [Range(0f, 100f)]
    [Tooltip("The player's HP and sanity effects manager.")]
    public float hp;
    [Tooltip("The delusional 'cooldown' to avoid overcluttering of GameObjects.")]
    [Range(10f, 100f)]
    public float deluCooldown;
    [Tooltip("The Game Over scene name, must not be blank unless for debugging purposes.")]
    public string gameOverScreen;
    [Tooltip("List used for the different sanity levels, containing different illusions (GameObjects or effects) each one.")]
    public List<DelusionObjects> illusionObjects = new List<DelusionObjects>();
    [Tooltip("The threshold used for the spawn chance.")]
    public int chanceThreshold;
    public int[] illusionAmt;
    [Tooltip("Unity Events that happen when the player has reached 0 HP.")]
    public UnityEvent onDeath;
    public float cylinderRadius;
    public Transform centralPoint;

    private int realityCheckLvl, randChance, maxHP;
    private float currentTime;
    private bool dead, spawned;

    // Volume + effects
    private Volume globalVolume;
    private Vignette vignette;
    private ChromaticAberration cA;
    private ColorAdjustments colorAdjustments;
    private MotionBlur motionBlur;
    private LensDistortion lD;

    private void Start()
    {
        globalVolume = FindObjectOfType<Volume>();

        if (globalVolume.profile.TryGet(out vignette) && globalVolume.profile.TryGet(out cA) && globalVolume.profile.TryGet(out motionBlur) && globalVolume.profile.TryGet(out colorAdjustments) && globalVolume.profile.TryGet(out lD))
        {
            print("Effects found.");
        }

        maxHP = (int)hp;
    }

    // Update is called once per frame
    void Update()
    {
        RealityCheck(hp);
        UpdateEffects();
        RandomizeSpawnChance();

        if (hp <= 75f)
        {
            if (!spawned)
            {
                SpawnChance(randChance);
            }

            if (spawned)
            {
                currentTime += Time.deltaTime;
            }
        }

        if (currentTime >= deluCooldown)
        {
            spawned = false;
            currentTime = 0;
        }

        if (hp > 100)
        {
            hp = 100;
        }
        else if (hp < 0)
        {
            hp = 0;
            GameOverEvents();
        }
    }

    void RealityCheck(float delu)
    {
        switch (delu)
        {
            case float d when d > 75f:
                realityCheckLvl = 0;
                break;

            case float d when d <= 75f && d >= 50f:
                realityCheckLvl = 1;
                break;

            case float d when d <= 50f && d >= 25f:
                realityCheckLvl = 2;
                break;

            case float d when d <= 25f:
                realityCheckLvl = 3;
                break;

            default:
                Debug.Log("?????????");
                break;
        }
    }

    void RandomizeSpawnChance()
    {
        randChance = Random.Range(0, chanceThreshold);
    }

    void SpawnManagement(bool check)
    {
        int illuLimit = illusionObjects.Count;
        float angleIncrement = 360f / illuLimit;

        if (illusionAmt[realityCheckLvl] > 1 && !check)
        {
            for (int i = 0; i < illusionAmt[realityCheckLvl]; i++)
            {
                float angle = i * angleIncrement * Mathf.Deg2Rad;

                float xPos = centralPoint.position.x + Mathf.Cos(angle) * cylinderRadius;
                float zPos = centralPoint.position.z + Mathf.Sin(angle) * cylinderRadius;

                float yPos = centralPoint.position.y + ((Mathf.Sin(angle) * cylinderRadius) / 1.5f);

                if (yPos < 0)
                {
                    yPos = yPos * -1;
                }

                Vector3 spawnPos = new Vector3(xPos, yPos, zPos);
                Instantiate(illusionObjects[realityCheckLvl].IllusionaryObjects[Random.Range(0, illuLimit-1)], spawnPos, Quaternion.identity);
            }
        }
        else if (illusionAmt[realityCheckLvl] == 1 && !check)
        {
            Instantiate(illusionObjects[realityCheckLvl].IllusionaryObjects[Random.Range(0, illuLimit-1)]);
        }

        spawned = true;
    }

    void SpawnChance(int chance)
    {
        if (chance > chanceThreshold / 1.5f)
        {
            SpawnManagement(spawned);
        }
    }

    float LoseHP(float amount)
    {
        hp -= amount;
        return hp;
    }

    void GoToGameOver()
    {
        SceneManager.LoadScene(gameOverScreen);
    }

    void GameOverEvents()
    {
        if (!dead)
        {
            onDeath.Invoke();
        }
    }

    void UpdateEffects()
    {
        if (vignette != null && cA != null && motionBlur != null && colorAdjustments != null && lD != null)
        {
            float normalizedHP = Mathf.Clamp01(hp / maxHP);

            vignette.intensity.value = 0.5f - Mathf.Lerp(0f, 0.65f, normalizedHP);
            cA.intensity.value = 0.625f - normalizedHP;
            motionBlur.intensity.value = 1f - normalizedHP;
            lD.intensity.value = (1f - Mathf.Lerp(0.5f, 1f, normalizedHP)) * -1f;

            colorAdjustments.saturation.value = 0f - (normalizedHP * 2);
        }
    }

    void OnDrawGizmos()
    {
        if (centralPoint != null)
        {
            // Set Gizmo color
            Gizmos.color = Color.green;

            // Draw a wireframe circle around the player with the specified radius
            Gizmos.DrawWireSphere(centralPoint.position, cylinderRadius);
        }
    }
}
