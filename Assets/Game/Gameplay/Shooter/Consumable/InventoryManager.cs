using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int maxItemsPerType = 3;
    [SerializeField] private float cooldownTime = 2f;

    [Header("UI Settings")]
    [SerializeField] private TMP_Text meatCountText;
    [SerializeField] private TMP_Text potionCountText;

    [Header("Key Bindings")]
    [SerializeField] private KeyCode meatKey = KeyCode.R;
    [SerializeField] private KeyCode potionKey = KeyCode.T;

    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        itemCounts["Meat"] = 0;
        itemCounts["Potion"] = 0;
        cooldownTimers["Meat"] = 0;
        cooldownTimers["Potion"] = 0;

        UpdateUI();
    }


    private void Update()
    {
        HandleCooldowns();
        HandleInput();
    }

    private void HandleCooldowns()
    {
        List<string> keys = new List<string>(cooldownTimers.Keys);
        foreach (string key in keys)
        {
            if (cooldownTimers[key] > 0)
            {
                cooldownTimers[key] -= Time.deltaTime;
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(meatKey) && CanUseItem("Meat"))
        {
            UseItem("Meat");
        }

        if (Input.GetKeyDown(potionKey) && CanUseItem("Potion"))
        {
            UseItem("Potion");
        }
    }

    public void AddItem(string itemType)
    {
        if (itemCounts.ContainsKey(itemType) && itemCounts[itemType] < maxItemsPerType)
        {
            itemCounts[itemType]++;
            UpdateUI();
        }
    }

    private void UseItem(string itemType)
    {
        if (itemCounts[itemType] > 0)
        {
            itemCounts[itemType]--;
            cooldownTimers[itemType] = cooldownTime;

            if (playerController != null)
            {
                if (itemType == "Meat")
                {
                    playerController.IncreaseLives(10); 
                }
                else if (itemType == "Potion")
                {
                    playerController.IncreaseAttackSpeed(20f, 5f); 
                }
            }

            UpdateUI();
        }
    }


    private bool CanUseItem(string itemType)
    {
        return itemCounts[itemType] > 0 && cooldownTimers[itemType] <= 0;
    }

    private void UpdateUI()
    {
        meatCountText.text = itemCounts["Meat"].ToString();
        potionCountText.text = itemCounts["Potion"].ToString();
    }
}

