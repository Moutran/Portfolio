using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlashlightScript))]
public class FlashlightCustomEditor : Editor
{
    private bool showFlashlightSettings = true;

    public override void OnInspectorGUI()
    {
        FlashlightScript flashlight = (FlashlightScript)target;

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Flashlight", titleStyle);
        GUILayout.Space(20);

        // Flashlight Settings Section
        showFlashlightSettings = EditorGUILayout.Foldout(showFlashlightSettings, "Flashlight Settings", true);
        if (showFlashlightSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUIStyle sectionStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.Label("Flashlight Settings", sectionStyle);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("battery"), new GUIContent("Battery", "Current flashlight battery level."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("batteryReduction"), new GUIContent("Battery Reduction", "Battery drain per second when flashlight is on."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("batteryIndicator"), new GUIContent("Battery UI Text", "TextMeshPro component that shows battery level."));

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
