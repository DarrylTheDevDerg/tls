using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerPrefItem
{
    public string key;     // The key for the PlayerPref
    public string value;   // The string representation of the value (we'll parse it dynamically based on the type)
    public PlayerPrefType prefType; // The type of the PlayerPref (int, float, string)

    public enum PlayerPrefType
    {
        Int,
        Float,
        String
    }
}

public class PlayerPrefsManager : MonoBehaviour
{
    // A list of PlayerPrefItems to show in the Inspector
    [SerializeField]
    private List<PlayerPrefItem> playerPrefsList = new List<PlayerPrefItem>();

    // Save all values in the PlayerPrefsList to PlayerPrefs
    public void SaveAll()
    {
        foreach (var item in playerPrefsList)
        {
            switch (item.prefType)
            {
                case PlayerPrefItem.PlayerPrefType.Int:
                    if (int.TryParse(item.value, out int intValue))
                    {
                        PlayerPrefs.SetInt(item.key, intValue);
                    }
                    break;

                case PlayerPrefItem.PlayerPrefType.Float:
                    if (float.TryParse(item.value, out float floatValue))
                    {
                        PlayerPrefs.SetFloat(item.key, floatValue);
                    }
                    break;

                case PlayerPrefItem.PlayerPrefType.String:
                    PlayerPrefs.SetString(item.key, item.value);
                    break;
            }
        }
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs saved!");
    }

    // Load all values from PlayerPrefs into the PlayerPrefsList
    public void LoadAll()
    {
        foreach (var item in playerPrefsList)
        {
            if (PlayerPrefs.HasKey(item.key))
            {
                switch (item.prefType)
                {
                    case PlayerPrefItem.PlayerPrefType.Int:
                        item.value = PlayerPrefs.GetInt(item.key).ToString();
                        break;

                    case PlayerPrefItem.PlayerPrefType.Float:
                        item.value = PlayerPrefs.GetFloat(item.key).ToString();
                        break;

                    case PlayerPrefItem.PlayerPrefType.String:
                        item.value = PlayerPrefs.GetString(item.key);
                        break;
                }
            }
        }
        Debug.Log("All PlayerPrefs loaded into the list!");
    }

    // Clear all PlayerPrefs both in the list and saved in the system
    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        playerPrefsList.Clear();
        Debug.Log("All PlayerPrefs cleared!");
    }
}
