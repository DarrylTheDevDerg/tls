using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammo, extraAmmo, coins;
    private PlayerStats pS;

    // Start is called before the first frame update
    void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();
    }

    public void DisplayText()
    {
        if (pS != null && ammo != null && extraAmmo != null && coins != null)
        {
            ammo.text = pS.ammo.ToString();
            extraAmmo.text = pS.extraAmmo.ToString();
            coins.text = pS.coins.ToString();
        }
    }

}
