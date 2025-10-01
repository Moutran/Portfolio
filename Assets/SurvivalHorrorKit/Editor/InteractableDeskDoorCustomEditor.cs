using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableDeskDoor))]
public class InteractableDeskDoorCustomEditor : Editor
{
    private GUIStyle titleStyle;
    private GUIStyle sectionTitleStyle;
    private SerializedProperty canOpen;
    private SerializedProperty interactionText_Open;
    private SerializedProperty interactionText_Close;

    private void OnEnable()
    {
        canOpen = serializedObject.FindProperty("canOpen");
        interactionText_Open = serializedObject.FindProperty("interactionText_Open");
        interactionText_Close = serializedObject.FindProperty("interactionText_Close");

        // Title style
        titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        // Section title style
        sectionTitleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Space(10);
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Interactable Desk Door", titleStyle);
        GUILayout.Space(20);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Interaction Settings", sectionTitleStyle);
        EditorGUILayout.PropertyField(canOpen, new GUIContent("Can Open", "Whether the drawer can currently be interacted with."));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundOpen"), new GUIContent("Opening Sound", "Door's opening sound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundClose"), new GUIContent("Closing Sound", "Door's closing sound"));
        EditorGUILayout.PropertyField(interactionText_Open, new GUIContent("Open Text", "Text shown to the player when the drawer can be opened."));
        EditorGUILayout.PropertyField(interactionText_Close, new GUIContent("Close Text", "Text shown to the player when the drawer can be closed."));
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}

