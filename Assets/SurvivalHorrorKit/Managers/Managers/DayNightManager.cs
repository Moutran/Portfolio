using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instance { get; private set; }

    private DayNightCycle daynightCycle;
    private ResourceDistributor distributor;
    private UserInterfaceManager userInterfaceManager;

    public int timeOfDay = 6;
    public int daysSurvived = 0;

    private bool canSleep = false;

    private bool db = true;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keep FPC across scenes
    }

    private void Start()
    {
        daynightCycle = FindObjectOfType<DayNightCycle>();
        distributor = FindObjectOfType<ResourceDistributor>();
        userInterfaceManager = FindObjectOfType<UserInterfaceManager>();
        StartNewDay();
    }

    private void Update()
    {
        timeOfDay = daynightCycle.currentHour;
        switch (timeOfDay)
        {
            case 19:
                return;
            case 5:
                if (db)
                {
                    canSleep = true;
                    daynightCycle.ToggleCycle(false);
                }
                return;
        }
    }

    public void Sleep()
    {
        if (canSleep)
        {
            daynightCycle.timeOfDay += 1;
            daynightCycle.currentHour += 1;
            canSleep = false;
            StartNewDay();
        }
        else
        {
            userInterfaceManager.ShowMessage("You can not sleep in the day");
        }
    }

    void StartNewDay()
    {
        daysSurvived++;
        distributor.Distribute();
        daynightCycle.ToggleCycle(true);
    }

    void InitializeNightTime()
    {
        Debug.Log("NightTime");
    }

    IEnumerator DebounceEnum()
    {
        yield return new WaitForSeconds(daynightCycle.dayDurationInSeconds/10);
        db = true;
    }
    
}