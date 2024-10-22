using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManagement : MonoBehaviour
{
    public enum Product
    {
        FirstAidKit,
        Medkit,
        ThirtyBullets,
        NinetyBullets
    }

    public Product product;
    public int price;
    public TextMeshProUGUI displayName, priceText, coinDisplay, descriptionDisplay;
    public string description;

    private int currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        currentCoins = PlayerPrefs.GetInt("Coins", 0);
        coinDisplay.text = currentCoins.ToString();

        switch (product)
        {
            case Product.FirstAidKit:
                displayName.text = "First-Aid Kit";
                priceText.text = price.ToString();

                break;
            case Product.Medkit:
                displayName.text = "Med-Kit";
                priceText.text = price.ToString();

                break;
            case Product.ThirtyBullets:
                displayName.text = "30 Bullets";
                priceText.text = price.ToString();

                break;
            case Product.NinetyBullets:
                displayName.text = "90 Bullets";
                priceText.text = price.ToString();

                break;
            default:
                print("Error!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplay.text = currentCoins.ToString();
    }

    void BuyItem()
    {
        int extrab = PlayerPrefs.GetInt("Extra Ammo", 0);

        switch (product)
        {
            case Product.FirstAidKit:
                int currentfak = PlayerPrefs.GetInt("First Aid Kit", 0);
                PlayerPrefs.SetInt("First Aid Kit", currentfak + 1);

                break;
            case Product.Medkit:
                int currentmk = PlayerPrefs.GetInt("Medkit", 0);
                PlayerPrefs.SetInt("Medkit", currentmk + 1);

                break;
            case Product.ThirtyBullets:
                int bullets = PlayerPrefs.GetInt("Ammo", 0);
                int bullcalc = bullets + 30;

                int bfinal = bullets - bullcalc;

                if (bullcalc > 100)
                {
                    PlayerPrefs.SetInt("Ammo", 100);
                    PlayerPrefs.SetInt("Extra Ammo", extrab + bfinal);
                }
                else
                {
                    PlayerPrefs.SetInt("Ammo", bullets + 30);
                }

                break;
            case Product.NinetyBullets:
                int ninbullets = PlayerPrefs.GetInt("Ammo", 0);
                int ninbcalc = ninbullets + 90;
                int nfinal = ninbullets - ninbcalc;

                if (ninbcalc > 100)
                {
                    PlayerPrefs.SetInt("Ammo", 100);
                    PlayerPrefs.SetInt("Extra Ammo", extrab + nfinal);
                }
                else
                {
                    PlayerPrefs.SetInt("Ammo", ninbullets + 90);
                }

                break;
            default:
                print("Error!");
                break;
        }

        currentCoins -= price;
    }

    private void OnMouseOver()
    {
        descriptionDisplay.text = description;
    }
}
