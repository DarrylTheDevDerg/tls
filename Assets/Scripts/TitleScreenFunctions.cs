using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TitleScreenFunctions : MonoBehaviour
{
    [DisableIf("@this.shouldStart == true")]
    public UnityEvent whatToDo;
    public bool shouldStart;

    [EnableIf("@this.shouldStart == true")]
    public UnityEvent inStart;

    // Start is called before the first frame update
    void Start()
    {
        if (shouldStart && inStart != null)
        {
            inStart.Invoke();
        }
    }

    public void DoStuff()
    {
        if (whatToDo != null)
        {
            whatToDo.Invoke();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
