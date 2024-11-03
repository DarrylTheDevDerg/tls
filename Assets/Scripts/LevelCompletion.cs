using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    public GameObject ui, pause;
    public string shop, ending;

    private PauseScreen pS;
    private PlayerPrefsManager PlayerPrefsManager;
    private int level;
    private bool complete;

    // Start is called before the first frame update
    void Start()
    {
        pS = FindObjectOfType<PauseScreen>();
        PlayerPrefsManager = FindObjectOfType<PlayerPrefsManager>();

        level = PlayerPrefs.GetInt("Level", 0);
    }

    private void Update()
    {
        if (complete)
        {
            ShopWarp();
            PlayerPrefsManager.SaveAll();
        }
        else
        {

        }
    }

    public void ActivateScreen()
    {
        if (pause)
        {
            pause.SetActive(false);
        }

        pS.enabled = false;
        ui.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        complete = true;
    }

    public void ShopWarp()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(shop);
    }

    public void LevelLoad()
    {
        Time.timeScale = 1f;

        if (level < 3)
        {
            SceneManager.LoadScene($"Level{level + 1}");
        }
        else if (level == 3)
        {
            SceneManager.LoadScene(ending);
        }
        
    }
}
