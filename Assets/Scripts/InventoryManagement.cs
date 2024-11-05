using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public TextMeshProUGUI mkDisplay, fakDisplay;

    public int medkits, fak;
    private Delusion d;

    public enum ItemType
    {
        MedKit,
        FirstAidKit
    }

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();

        GetPlayerPrefs();

        medkits += Random.Range(1, 3);

        fak += Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmount();
    }

    public void HealPlayer(ItemType item)
    {
        switch (item)
        {
            case ItemType.MedKit:

                if (medkits > 0)
                {
                    d.hp += 60;
                }

                break;

            case ItemType.FirstAidKit:

                if (fak > 0)
                {
                    d.hp += 35;
                }
                
                break;
        }
    }

    void DisplayAmount()
    {
        mkDisplay.text = medkits.ToString();
        fakDisplay.text = fak.ToString();
    }

    void GetPlayerPrefs()
    {
        medkits = PlayerPrefs.GetInt("Medkit", 1);
        fak = PlayerPrefs.GetInt("First Aid Kit", 1);
    }
}
