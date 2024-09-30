using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    [Tooltip("This should at least have a string to work.")]
    public string[] levelNames;
    [Tooltip("Integer determines which level are you on, derived from the PlayerPref 'CurrentLevel', and defaults to 0 if none is found.")]
    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        levelIndex = PlayerPrefs.GetInt("Current Level", 0);
    }

    public void LoadSpecificLevel(int index)
    {
        SceneManager.LoadScene(levelNames[index]);
    }

    public void Progression()
    {
        levelIndex++;
        LoadSpecificLevel(levelIndex);
    }
}
