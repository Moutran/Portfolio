using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableLocker))]
public class InteractableLockerEditor : Editor
{
    private bool showUI = true;

    public override void OnInspectorGUI()
    {
        InteractableLocker locker = (InteractableLocker)target;

        // Title section
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 18;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Interactable Locker", titleStyle);
        GUILayout.Space(20);

        serializedObject.Update();

        // UI Section
        showUI = EditorGUILayout.Foldout(showUI, "User Interface Settings", true);
        if (showUI)
        {
            EditorGUILayout.BeginVertical("box");
            GUIStyle sectionTitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.Label("User Interface Settings", sectionTitleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionText_Open"), new GUIContent("Open Text", "UI text shown when locker is closed."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionText_Close"), new GUIContent("Close Text", "UI text shown when locker is open."));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
