using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayDurationInSeconds = 120f;
    private Vector3 rotationAxis = Vector3.right;
    public bool toggle = true;

    [Header("Time of Day")]
    [Range(0, 23)]
    public int currentHour = 6;
    public float timeOfDay = 6f;  

    private float timeMultiplier;

    void Start()
    {
        timeMultiplier = 24f / dayDurationInSeconds;
    }

    void Update()
    {
        PlayCycle();
    }

    void PlayCycle()
    {
        if (toggle)
        {
            // Advance in-game time
            timeOfDay += timeMultiplier * Time.deltaTime;

            if (timeOfDay >= 24f)
                timeOfDay = 0f;

            currentHour = Mathf.FloorToInt(timeOfDay);

            // Offset rotation to align 6 AM with sunrise (0°)
            float rotationDegrees = ((timeOfDay - 6f + 24f) % 24f) / 24f * 360f;
            transform.rotation = Quaternion.AngleAxis(rotationDegrees, rotationAxis);
        }
    }

    public void ToggleCycle(bool Toggle)
    {
        toggle = Toggle;
    }
}