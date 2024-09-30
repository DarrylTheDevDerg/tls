using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DelusionObjects
{
    public int LevelNeeded;
    public GameObject[] IllusionaryObjects;
}
public class Delusion : MonoBehaviour
{
    public float hp, cooldown;
    public string gameOverScreen;
    public List<DelusionObjects> illusionObjects = new List<DelusionObjects>();
    public int chance, chanceThreshold, illusionAmt;

    private int realityCheckLvl, randChance;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 75f)
        {
            currentTime += Time.deltaTime;
        }

        RealityCheck(hp);

        if (currentTime > cooldown)
        {

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
}
