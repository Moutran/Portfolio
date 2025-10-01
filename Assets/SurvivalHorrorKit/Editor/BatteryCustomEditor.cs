using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BatteryScript))]
public class BatteryCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BatteryScript batteryScript = (BatteryScript)target;

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Battery Script", titleStyle);
        GUILayout.Space(20);

        // Battery Settings Section
        EditorGUILayout.BeginVertical("box");
        GUIStyle sectionStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Battery Settings", sectionStyle);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
