using System;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryScript : MonoBehaviour
{
    public static PlayerInventoryScript instance;

    [Header("Player Inventory Settings")]
    public List<Item> itemLibrary = new List<Item>(); //All the items in the Game
    public List<Item> playerInventory = new List<Item>(); //Player's Current Inventory
    public List<GameObject> playerItemObjects = new List<GameObject>(); //Item Game Objects on player prefab
    public int inventoryCapacity = 5; //Player's Inventory Capacity
    private Item currentSeletedItem; //The iten the player is currently holding

    [Header("Ammo")]
    public int lightAmmo = 0;
    public int shotgunShells = 0;
    public int rifleAmmo = 0;

    [Header("UI Elements")]
    public Image[] hotbarSlots; //Inventory UI Slots
    public Image[] hotbarBGSlots; //Inventory Background UI Slots
    private UserInterfaceManager userInterfaceManager;

    [Header("Throw Settings")]
    public KeyCode throwItemKey = KeyCode.G;
    public Transform throwItemPosition;
    public int throwItemForce = 15;
    public GameObject garbageCollector;

    [Header("Player Input")]
    private int selectedIndex = -1;

    private ItemManager itemManager;
    void Start()
    {
        userInterfaceManager = GetComponent<UserInterfaceManager>();
        itemManager = FindObjectOfType<ItemManager>();
        SelectItem(0);
    }

    void Update()
    {
        HandleInput();
        ThrowItemDetection();
    }

    void HandleInput()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectItem(i);
            }
        }
    }

    void SelectItem(int index)
    {
        if (index >= playerInventory.Count) return;

        if (index < 0)
        {
            currentSeletedItem = null;
        }
        else
        {
            selectedIndex = index;
            currentSeletedItem = playerInventory[selectedIndex];
        }

        foreach (GameObject playerItem in playerItemObjects)
        {
            playerItem.SetActive(false);
            Item item = playerItem.GetComponent<PlayerItem>().itemInfo;
            if (item == currentSeletedItem)
            {
                playerItem.SetActive(true);
            }
        }
        UpdateHotbarUI(index);
    }
    void UpdateHotbarUI(int index)
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < playerInventory.Count && playerInventory[i] != null)
            {
                hotbarSlots[i].sprite = playerInventory[i].itemSprite;
                hotbarSlots[i].enabled = true;
                hotbarBGSlots[i].color = Color.black;
            }
            else
            {
                hotbarSlots[i].sprite = null;
                hotbarSlots[i].enabled = false;
                hotbarBGSlots[i].color = Color.black;
            }
        }

        //Highlight selected Item
        if (index >= 0 && index < hotbarBGSlots.Length)
        {
            hotbarBGSlots[index].color = Color.white;
        }
    }

    public void PickUpItem(Item itemtoPickup, GameObject itemObj)
    {
        if (playerInventory.Count < inventoryCapacity)
        {
            playerInventory.Add(itemtoPickup);
            Destroy(itemObj);
            UpdateHotbarUI(selectedIndex);
        }
        else
        {
            userInterfaceManager.ShowMessage("Inventory Full");
        }
    }

    void ThrowItemDetection() //Detect and Execute throwing a item
    {
        if (Input.GetKeyDown(throwItemKey) && currentSeletedItem != null && currentSeletedItem.isDroppable)
        {
            Debug.Log("Dropped " + currentSeletedItem.name);
            GameObject thrownItem = Instantiate(currentSeletedItem.itemPrefab, throwItemPosition.position, Quaternion.identity);
            Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwItemForce);
            thrownItem.transform.parent = garbageCollector.transform;
            RemoveItemHolding(false);
        }
    }
    public void RemoveItemHolding(bool Consumed) //Remove the item player is holding
    {
        if (!Consumed)
        {
            playerInventory.Remove(currentSeletedItem);
            SelectItem(selectedIndex - 1);
        }
        else
        {
            itemManager.UnRegisterItem(currentSeletedItem);
            playerInventory.Remove(currentSeletedItem);
            SelectItem(selectedIndex - 1);
        }
    }

    public bool SearchInventory(string search) //Search for a item in player's inventory
    {
        foreach (Item item in playerInventory)
        {
            if (item.name == search)
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckItemHolding(string search) //Search for a item the player might be holding
    {
        if (currentSeletedItem != null)
        {
            if (currentSeletedItem.name == search)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void GiveItemByName(string search)
    {
        if (playerInventory.Count < inventoryCapacity)
        {
            bool found = false;
            foreach (Item item in itemLibrary) 
            { 
                if (item.name == search)
                {
                    playerInventory.Add(item);
                    UpdateHotbarUI(selectedIndex);
                    found = true;
                    return;
                }
            }
            if (!found) Debug.LogWarning("Please add the item in Item Library");
        }
        else
        {
            userInterfaceManager.ShowMessage("Inventory Full");
        }
    }

    public GameObject GetPlayerItemGameObject(string search)
    {
        foreach (GameObject playerItem in playerItemObjects)
        {
            if (playerItem.GetComponent<PlayerItem>().itemInfo.name == search)
            {
                return playerItem;
            }
        }
        return null;    
    }

    public GameObject GetItemHolding()
    {
        foreach (GameObject playerItem in playerItemObjects)
        {
            if (playerItem.GetComponent<PlayerItem>().itemInfo == currentSeletedItem)
            {
                return playerItem;
            }
        }
        return null;
    }

    public void AddAmmo(string AmmoType)
    {
        switch (AmmoType) 
        {
            case "LightAmmo":
                lightAmmo += 2;
                return;
            case "ShotgunShells":
                shotgunShells += 2;
                return;
            case "RifleAmmo":
                rifleAmmo += 2;
                return;
        }
    }
}
