using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public Dictionary<Item, int> Item_Dictionary = new Dictionary<Item, int>();
    public PlayerInventoryScript playerInventory;

    private void Awake()
    {
        Initialize();
    }

    public List<Item> GetAvailableItems() //Find Available Items and List them
    {
        List<Item> itemsBelowMax = new List<Item>();

        foreach (var kvp in Item_Dictionary)
        {
            Item item = kvp.Key;
            int itemOnScene = kvp.Value;

            if (itemOnScene < item.maxItems)
            {
                int spawnableCount = item.maxItems - itemOnScene;
                for (int i = 0; i < spawnableCount; i++)
                {
                    itemsBelowMax.Add(item);
                }
            }
        }
        if (itemsBelowMax.Count == 0)
        {
            Debug.Log("All items are at or above their max values.");
        }
        return itemsBelowMax;
    }

    public void RegisterItem(Item item) //Register Spawned Items in Dictionary
    {
        if (Item_Dictionary.ContainsKey(item))
        {
            Item_Dictionary[item] +=  1;
        }
        else
        {
            Debug.Log("No entry found");
        }

        //Debug.Log("Registered " + item.name);
    }

    public void UnRegisterItem(Item item)
    {
        if(item != null) 
        {
            if (Item_Dictionary.TryGetValue(item, out int count))
            {
                Item_Dictionary[item] = Mathf.Max(0, count - 1);
                Debug.Log("Unregistered " + item.name);
            }
            else
            {
                //Debug.LogWarning($"Tried to unregister {item.name}, but it’s not in the dictionary!");
            }
        }
    }


    public void RegisterPlayerInventory() //Register Player Inventory in Dictionary
    {
        foreach (Item item in playerInventory.playerInventory)
        {
            RegisterItem(item);
        }
    }

    public void RegisterPlayerAmmo()
    {
        for (int i = 0; i <= playerInventory.lightAmmo / 2; i++)
        {
            foreach (var key in Item_Dictionary.Keys)
            {
                if (key.name == "LightAmmo")
                {
                    Item_Dictionary[key] += 1;
                }
            }
        }

        for (int i = 0; i <= playerInventory.shotgunShells; i++)
        {
            foreach (var key in Item_Dictionary.Keys)
            {
                if (key.name == "ShotgunShells")
                {
                    Item_Dictionary[key] += 1;
                }
            }
        }

        for (int i = 0; i <= playerInventory.rifleAmmo; i++)
        {
            foreach (var key in Item_Dictionary.Keys)
            {
                if (key.name == "RifleAmmo")
                {
                    Item_Dictionary[key] += 1;
                }
            }
        }
    }

    void Initialize() //Initilize Manager
    {
        RegisterPlayerInventory();
        foreach (Item item in playerInventory.itemLibrary)
        {
            Item_Dictionary.Add(item, 0);
            Debug.Log(item.name);
        }
    }
}