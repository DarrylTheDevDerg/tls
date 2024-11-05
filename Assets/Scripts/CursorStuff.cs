using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
