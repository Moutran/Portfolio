using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableLightSwitch))]
public class InteractableLightSwitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InteractableLightSwitch lightSwitch = (InteractableLightSwitch)target;
        serializedObject.Update();

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Light Switch", titleStyle);
        GUILayout.Space(20);

        // References
        EditorGUILayout.BeginVertical("box");
        GUIStyle sectionStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("References", sectionStyle);
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("lamp"),
            new GUIContent("Light Source", "The light that this switch will control.")
        );
        EditorGUILayout.EndVertical();

        // Light Settings
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Settings", sectionStyle);
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("isOpen"),
            new GUIContent("Is On", "Is the light currently on?")
        );
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("hasPower"),
            new GUIContent("Has Power", "Does this light switch currently have power?")
        );
        EditorGUILayout.EndVertical();

        // UI Texts
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("UI Texts", sectionStyle);
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("interactionText_Open"),
            new GUIContent("Text When Off", "Text shown when the light is off and can be turned on.")
        );
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("interactionText_Close"),
            new GUIContent("Text When On", "Text shown when the light is on and can be turned off.")
        );
        EditorGUILayout.PropertyField(
            serializedObject.FindProperty("interactionText_NoPower"),
            new GUIContent("Text No Power", "Text shown when there is no power available.")
        );
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}