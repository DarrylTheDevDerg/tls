using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI display;
    public string[] levelNames;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        CheckName(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckName(string name)
    {
        foreach (string s in levelNames)
        {
            if (name == s)
            {
                if (s == "Level1")
                {
                    display.text = "Tutorial";
                }
                else if (s == "Level2")
                {
                    display.text = "Level 1";
                }
                else if (s == "Level3")
                {
                    display.text = "Level 2";
                }
                else if (s == "Level4")
                {
                    display.text = "The Last Run";
                }
                else
                {
                    display.text = "???";
                }
            }
        }
    }
}
