using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int coins, ammo, extraAmmo;
    public string[] playerPrefs;
    public TextMeshProUGUI coinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinDisplay();
    }

    public void LoadPlayerStats()
    {
        for (int i = 0; i < playerPrefs.Length; i++)
        {
            if (playerPrefs[i] == "Coins")
            {
                coins = PlayerPrefs.GetInt(playerPrefs[i], 0);
            }

            if (playerPrefs[i] == "Ammo")
            {
                ammo = PlayerPrefs.GetInt(playerPrefs[i], 0);
            }

            if (playerPrefs[i] == "Extra Ammo")
            {
                extraAmmo = PlayerPrefs.GetInt(playerPrefs[i], 0);
            }
        }
    }

    public void SavePlayerStats()
    {
        for (int i = 0; i < playerPrefs.Length; i++)
        {
            if (playerPrefs[i] == "Coins")
            {
                PlayerPrefs.SetInt(playerPrefs[i], coins);
            }

            if (playerPrefs[i] == "Ammo")
            {
                PlayerPrefs.SetInt(playerPrefs[i], ammo);
            }

            if (playerPrefs[i] == "Extra Ammo")
            {
               PlayerPrefs.SetInt(playerPrefs[i], extraAmmo);
            }
        }
    }

    public void CoinDisplay()
    {
        coinDisplay.text = coins.ToString();
    }
}
