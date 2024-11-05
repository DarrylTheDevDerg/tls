using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    public GameObject ui, pause;
    public string shop, ending;
    public KeyCode shopKey, endingKey;

    private PauseScreen pS;
    private PlayerPrefsManager PlayerPrefsManager;
    private LevelProgression lP;
    private int level;
    private bool complete;

    // Start is called before the first frame update
    void Start()
    {
        pS = FindObjectOfType<PauseScreen>();
        PlayerPrefsManager = FindObjectOfType<PlayerPrefsManager>();
        lP = FindObjectOfType<LevelProgression>();

        level = PlayerPrefs.GetInt("Level", 0);
    }

    private void Update()
    {
        if (complete)
        {
            PlayerPrefsManager.SaveAll();
            KeyCheck();
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
        complete = true;
    }

    public void ShopWarp()
    {
        SceneManager.LoadScene(shop);
    }

    public void LevelLoad()
    {
        lP.Progression();
    }

    public void KeyCheck()
    {
        if (Input.GetKeyDown(shopKey) && Time.deltaTime > 0f)
        {
            ShopWarp();
        }

        if (Input.GetKeyDown(endingKey) && Time.deltaTime > 0f)
        {
            LevelLoad();
        }
    }
}
