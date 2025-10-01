using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PowerCut))]
public class CutPowerOffCustomEditor : Editor
{
    private bool showPowerCutSettings = true;

    public override void OnInspectorGUI()
    {
        PowerCut powerCut = (PowerCut)target;

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Power Cut Trigger", titleStyle);
        GUILayout.Space(20);

        serializedObject.Update();

        // Settings Section
        showPowerCutSettings = EditorGUILayout.Foldout(showPowerCutSettings, "Power Cut Settings", true);
        if (showPowerCutSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Power Cut Settings", titleStyle);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("FuseBoxes"), new GUIContent("Fuse Boxes", "List of fuse boxes affected by the power cut."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fusesToRemove"), new GUIContent("Fuses To Remove", "Number of fuses to disable when triggered."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("oneTimeUse"), new GUIContent("One Time Use", "If enabled, power cut can only trigger once."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isActive"), new GUIContent("Is Active", "Whether the power cut trigger is currently enabled."));

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
