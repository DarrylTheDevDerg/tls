using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManagement;

public class ItemCooldown : MonoBehaviour
{
    [Range(0f, 2f)]
    public float threshold;
    public int cooldownAmt;
    public TextMeshProUGUI cooldownDisplay;
    public Slider[] items;
    public Slider bar;
    public string animBool;
    public KeyCode key;
    public ItemType type;

    private Animator a;
    private InventoryManagement inventory;
    private GlobalCooldownManager gCm;
    private int cooldown;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        inventory = FindObjectOfType<InventoryManagement>();
        gCm = FindObjectOfType<GlobalCooldownManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TextDisplay();
        KeyCodeTrigger();

        if (cooldown > 0)
        {
            time += Time.deltaTime;
        }

        if (cooldown > 0 && time > threshold)
        {
            cooldown--;
            time = 0;
        }
    }

    void TextDisplay()
    {
        cooldownDisplay.text = gCm.GetCooldown().ToString();
        bar.value = gCm.GetCooldown();

        if (gCm.GetCooldown() <= 0)
        {
            cooldownDisplay.enabled = false;

            foreach (Slider item in items)
            {
                item.enabled = false;
            }

            a.SetBool(animBool, false);
        }
        else
        {
            cooldownDisplay.enabled = true;

            foreach (Slider item in items)
            {
                item.enabled = true;
            }

            a.SetBool(animBool, true);
        }
    }

    public void SetUniversalCooldown()
    {
        gCm.cooldownDuration = cooldownAmt;

        foreach (Slider item in items)
        {
            item.maxValue = cooldownAmt;
        }
    }

    void KeyCodeTrigger()
    {
        if (Input.GetKeyDown(key))
        {
            if (inventory.medkits > 0 || inventory.fak < 0)
            {
                bar.value = gCm.GetCooldown();
                UseItem();
            }
        }
    }

    public void UseItem()
    {
        // Check if the cooldown is active before healing
        if (!gCm.IsCooldownActive() && inventory.medkits > 0 || !gCm.IsCooldownActive() && inventory.fak < 0)
        {
            inventory.HealPlayer(type);

            if (type == ItemType.MedKit && inventory.medkits > 0)
            {
                inventory.medkits--;
            }

            if (type == ItemType.FirstAidKit && inventory.fak > 0)
            {
                inventory.fak--;
            }

            SetUniversalCooldown();
            gCm.StartCooldown();  // Start the global cooldown
        }
        else
        {
            print("Don't.");
        }
    }
}