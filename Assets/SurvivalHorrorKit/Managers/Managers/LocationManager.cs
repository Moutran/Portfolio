using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class LocationManager : MonoBehaviour
{



    [SerializeField] private List<GameObject> LocationA = new List<GameObject>();
    [SerializeField] private List<GameObject> LocationB = new List<GameObject>();
    [SerializeField] private List<GameObject> LocationC = new List<GameObject>();

    private List<GameObject> PickRandomFromList(List<GameObject> source, int count)
    {
        List<GameObject> picks = new List<GameObject>();
        List<GameObject> temp = new List<GameObject>(source);

        int picksToMake = Mathf.Min(count, temp.Count);

        for (int i = 0; i < picksToMake; i++)
        {
            int index = Random.Range(0, temp.Count);
            picks.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return picks;
    }

    public List<GameObject> PickRandomSpawnPoints()
    {
        List<GameObject> result = new List<GameObject>();

        result.AddRange(PickRandomFromList(LocationA, 3));
        result.AddRange(PickRandomFromList(LocationB, 3));
        result.AddRange(PickRandomFromList(LocationC, 3));

        return result;
    }

    public List<GameObject> GetUnavailableSpawners()
    {
        List<GameObject> unavailable = new List<GameObject>();
        List<GameObject> allSpawners = new List<GameObject>();

        allSpawners.AddRange(LocationA);
        allSpawners.AddRange(LocationB);
        allSpawners.AddRange(LocationC);

        foreach (GameObject spawner in allSpawners)
        {
            if (spawner == null) continue;

            ItemSpawnPoint itemSpawnPoint = spawner.GetComponent<ItemSpawnPoint>();
            if (itemSpawnPoint != null && !itemSpawnPoint.isAvailable)
            {
                unavailable.Add(spawner);
            }
        }
        return unavailable;
    }

}
