using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class ResourceDistributor : MonoBehaviour
{
    private ItemManager itemManager;
    private LocationManager locationManager;

    public List<GameObject> spawnPoints;
    public List<Item> availableItems;

    private GameObject GarbageCollector;

    private void Awake()
    {
        itemManager = FindObjectOfType<ItemManager>();
        locationManager = FindObjectOfType<LocationManager>();
        GarbageCollector = (transform.Find("GarbageCollector")).gameObject;
    }

    public void Distribute()
    {
        Despawnitems();
        GetSpawnInformation();
        ListShuffle();
        SpawnAllAvailable();
    }

    private void GetSpawnInformation()
    {
        spawnPoints = locationManager.PickRandomSpawnPoints();
        availableItems = itemManager.GetAvailableItems();
    }

    private void SpawnAllAvailable()
    {
        while (spawnPoints.Count > 0 && availableItems.Count > 0)
        {
            SpawnRandomItem();
        }
    }

    private void SpawnRandomItem()
    {
        if (spawnPoints.Count == 0 || availableItems.Count == 0)
        {
            Debug.LogWarning("No spawners or items available!");
            return;
        }

        // pick random item
        int itemIndex = Random.Range(0, availableItems.Count);
        Item randomItem = availableItems[itemIndex];

        // pick random spawner
        int spawnerIndex = Random.Range(0, spawnPoints.Count);
        GameObject randomSpawner = spawnPoints[spawnerIndex];

        // get the spawner script
        ItemSpawnPoint spawnerScript = randomSpawner.GetComponent<ItemSpawnPoint>();
        if (spawnerScript == null)
        {
            Debug.LogWarning("Spawner is missing ItemSpawnPoint script!");
            return;
        }

        // spawn the item
        spawnerScript.SpawnItem(randomItem);
        itemManager.RegisterItem(randomItem);

        // remove used entries
        availableItems.RemoveAt(itemIndex);
        spawnPoints.RemoveAt(spawnerIndex);
    }

    private void ListShuffle()
    {
        for (int i = 0; i < availableItems.Count; i++)
        {
            int randomIndex = Random.Range(i, availableItems.Count); // pick a random element from i to end
            Item temp = availableItems[i];
            availableItems[i] = availableItems[randomIndex];
            availableItems[randomIndex] = temp;
        }

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(i, spawnPoints.Count); // pick a random element from i to end
            GameObject temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }

    public void Despawnitems() //Despawn items on spawner list
    {
        CleanGarbageCollector();
        List<GameObject> spawns = locationManager.GetUnavailableSpawners();
        if (spawns.Count == 0) return;
        foreach (GameObject spawnPoint in spawns)
        {
            ItemSpawnPoint spawnPointScript = spawnPoint.GetComponent<ItemSpawnPoint>();

            if (!spawnPointScript.isAvailable)
            {
                itemManager.UnRegisterItem(spawnPointScript.itemInfo);
                spawnPointScript.DespawnCurrentItem();
            }
        }
    }

    private void CleanGarbageCollector()
    {
        if (GarbageCollector == null) return;
        foreach (Transform garbage in GarbageCollector.transform)
        {
            GameObject garbageObject = garbage.gameObject;
            var interactable = garbageObject.GetComponent<InteractableItem>();
            if (interactable != null)
            {
                itemManager.UnRegisterItem(interactable.itemInfo);
            }
            Destroy(garbageObject);

        }
        Debug.Log("Garbage Collector Cleaned");
    }
}
