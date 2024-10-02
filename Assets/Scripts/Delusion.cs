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
    [Tooltip("The player's HP and delusional 'cooldown' to avoid overcluttering of GameObjects.")]
    public float hp, deluCooldown;
    [Tooltip("The Game Over scene name, must not be blank unless for debugging purposes.")]
    public string gameOverScreen;
    [Tooltip("List used for the different sanity levels, containing different illusions (GameObjects or effects) each one.")]
    public List<DelusionObjects> illusionObjects = new List<DelusionObjects>();
    [Tooltip("The threshold used for the spawn chance and the amount of illusions to spawn at a time.")]
    public int chanceThreshold, illusionAmt;
    [Tooltip("Unity Events that happen when the player has reached 0 HP.")]
    public UnityEvent onDeath;

    private int realityCheckLvl, randChance, maxHP;
    private float currentTime;
    private bool dead;

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

        if (hp <= 75f)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime >= deluCooldown)
        {
            RandomizeSpawnChance();
            currentTime = 0;
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
        randChance = (int)Random.Range(0, chanceThreshold);
    }

    void SpawnManagement()
    {
        int illuLimit = illusionObjects.Count;

        if (illusionAmt > 1)
        {
            for (int i = 0; i < illusionAmt; i++)
            {
                Instantiate(illusionObjects[realityCheckLvl].IllusionaryObjects[(int)Random.Range(0, illuLimit)]);
            }
        }
        else if (illusionAmt == 1)
        {
            Instantiate(illusionObjects[realityCheckLvl].IllusionaryObjects[(int)Random.Range(0, illuLimit)]);
        }
    }

    void SpawnChance(int chance)
    {
        if (chance > randChance)
        {
            SpawnManagement();
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

            vignette.intensity.value = 0.45f - normalizedHP;
            cA.intensity.value = 0.5f - normalizedHP;
            motionBlur.intensity.value = 1f - normalizedHP;
            lD.intensity.value = (1f - Mathf.Lerp(0.5f, 1f, normalizedHP)) * -1f;

            colorAdjustments.saturation.value = Mathf.Lerp(0.1f, 0.85f, hp);
        }
    }
}
