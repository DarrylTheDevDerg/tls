using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private int realityCheckLvl, randChance;
    private float currentTime;
    private bool dead;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 75f)
        {
            currentTime += Time.deltaTime;
        }

        RealityCheck(hp);

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
}
