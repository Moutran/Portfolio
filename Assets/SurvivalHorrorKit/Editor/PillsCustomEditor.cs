using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PillsScript))]
public class PillsCustomEditor : Editor
{
    private bool showPillsSettings = true;

    public override void OnInspectorGUI()
    {
        PillsScript pills = (PillsScript)target;

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 18;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Pills Script", titleStyle);
        GUILayout.Space(20);

        // Pills Settings Foldout
        showPillsSettings = EditorGUILayout.Foldout(showPillsSettings, "Pills Settings", true);
        if (showPillsSettings)
        {
            EditorGUILayout.BeginVertical("box");

            GUIStyle sectionTitleStyle = new GUIStyle(GUI.skin.label);
            sectionTitleStyle.fontSize = 18;
            sectionTitleStyle.fontStyle = FontStyle.Bold;
            sectionTitleStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Pills Settings", sectionTitleStyle);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("healthGain"), new GUIContent("Health Gain", "Amount of health the player regains when consuming the pills."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("consumptionTime"), new GUIContent("Consumption Time", "Time it takes to consume the pills."));

            EditorGUILayout.EndVertical();
        }

        // Apply changes
        serializedObject.ApplyModifiedProperties();
    }
}
