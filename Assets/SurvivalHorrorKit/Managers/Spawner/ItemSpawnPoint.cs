using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    public Item itemInfo;
    [SerializeField]
    private GameObject itemGameObject;

    public bool isAvailable = true;

    private void Update()
    {
        if (itemGameObject != null)
        {
            if (itemGameObject.GetComponent<InteractableItem>())
            {
                itemInfo = itemGameObject.GetComponent<InteractableItem>().itemInfo;
            }
            else if (itemGameObject.GetComponent<InteractableAmmo>())
            {
                itemInfo = itemGameObject.GetComponent<InteractableAmmo>().itemInfo;
            }
        }
        else
        {
            itemInfo = null;    
        }
    }

    public void SpawnItem(Item itemToSpawn)
    {
        Debug.Log("Spawn");
        if (isAvailable)
        {
            itemInfo = itemToSpawn;
            isAvailable = false;
            itemGameObject = Instantiate(itemToSpawn.itemPrefab, gameObject.transform.position, Quaternion.identity);
            Debug.Log("Item Spawn On " + gameObject.transform.name);
            isAvailable = false;
        }
    }

    public void DespawnCurrentItem()
    {
        isAvailable = true;
        Destroy(itemGameObject);
        itemInfo = null;
    }
}