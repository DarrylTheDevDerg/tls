using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    [Tooltip("This should at least have a string to work.")]
    public string[] levelNames;

    private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        levelIndex = PlayerPrefs.GetInt("Current Level", 0);
    }

    public void LoadSpecificLevel()
    {
        SceneManager.LoadScene(levelNames[levelIndex]);
    }

    public void Progression()
    {
        levelIndex++;
        LoadSpecificLevel();
    }

    public void ResetLevel()
    {
        levelIndex = 0;
        PlayerPrefs.SetInt("Current Level", levelIndex);
    }

    public void GetLevelNumber(TextMeshProUGUI text)
    {
        text.text = (levelIndex + 1).ToString();
    }
}
