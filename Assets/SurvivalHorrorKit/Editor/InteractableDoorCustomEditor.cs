using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractableDoor))]
public class InteractableDoorCustomEditor : Editor
{
    private bool showStates = true;
    private bool showUISettings = true;

    public override void OnInspectorGUI()
    {
        InteractableDoor door = (InteractableDoor)target;
        SerializedObject so = serializedObject;
        so.Update();

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 18;
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Interactable Door", titleStyle);
        GUILayout.Space(15);

        // States Section
        showStates = EditorGUILayout.Foldout(showStates, "Door States", true);
        if (showStates)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Door States", new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            EditorGUILayout.PropertyField(so.FindProperty("isLocked"), new GUIContent("Is Locked", "Whether the door starts locked."));
            EditorGUILayout.PropertyField(so.FindProperty("canOpen"), new GUIContent("Can Open", "Whether the door is interactable."));
            EditorGUILayout.PropertyField(so.FindProperty("soundOpen"), new GUIContent("Opening Sound", "Door's opening sound"));
            EditorGUILayout.PropertyField(so.FindProperty("soundClose"), new GUIContent("Closing Sound", "Door's closing sound"));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // UI Settings
        showUISettings = EditorGUILayout.Foldout(showUISettings, "UI Settings", true);
        if (showUISettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("UI Settings", new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            EditorGUILayout.PropertyField(so.FindProperty("interactionText_Open"), new GUIContent("Open Text", "Text displayed when the door is closed."));
            EditorGUILayout.PropertyField(so.FindProperty("interactionText_Close"), new GUIContent("Close Text", "Text displayed when the door is open."));
            EditorGUILayout.EndVertical();
        }

        so.ApplyModifiedProperties();
    }
}
