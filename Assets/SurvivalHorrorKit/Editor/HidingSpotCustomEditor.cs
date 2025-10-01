using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HidingSpot))]
public class HidingSpotCustomEditor : Editor
{
    private bool showSettings = true;

    public override void OnInspectorGUI()
    {
        HidingSpot hidingSpot = (HidingSpot)target;

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Hiding Spot", titleStyle);
        GUILayout.Space(20);

        serializedObject.Update();

        showSettings = EditorGUILayout.Foldout(showSettings, "Settings", true);
        if (showSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Settings", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isActive"), new GUIContent("Is Active", "Enable or disable the hiding spot."));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
