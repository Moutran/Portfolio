using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UserInterfaceManager))]
public class UserInterfaceCustomEditor : Editor
{
    private bool showReferences = true;
    private bool showMessageSettings = true;

    public override void OnInspectorGUI()
    {
        UserInterfaceManager uiManager = (UserInterfaceManager)target;
        serializedObject.Update();

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("UI Manager", titleStyle);
        GUILayout.Space(20);

        // References Section
        showReferences = EditorGUILayout.Foldout(showReferences, "UI References", true);
        if (showReferences)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("UI References", SectionHeaderStyle());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("messageText_Transform"), new GUIContent("Message Text Transform", "Parent transform for spawned message text."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("messageText"), new GUIContent("Message Text Prefab", "Text prefab used for displaying messages."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionText"), new GUIContent("Interaction Text", "Text element used for interaction prompts."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Message Settings
        showMessageSettings = EditorGUILayout.Foldout(showMessageSettings, "Message Display Settings", true);
        if (showMessageSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Message Display Settings", SectionHeaderStyle());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("message_VisibilityTime"), new GUIContent("Message Visibility Time", "How long the message remains visible."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("message_FadeOut_Duration"), new GUIContent("Fade Out Duration", "Time it takes for the message to fade out."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();
    }

    private GUIStyle SectionHeaderStyle()
    {
        return new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
    }
}
