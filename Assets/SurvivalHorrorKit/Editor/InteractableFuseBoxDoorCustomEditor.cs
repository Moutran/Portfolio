using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableFuseBoxDoor))]
public class InteractableFuseBoxDoorCustomEditor : Editor
{
    private SerializedProperty isOpen;
    private SerializedProperty canOpen;
    private SerializedProperty interactionText_Open;
    private SerializedProperty interactionText_Close;

    private GUIStyle titleStyle;
    private GUIStyle sectionStyle;

    private void OnEnable()
    {
        isOpen = serializedObject.FindProperty("isOpen");
        canOpen = serializedObject.FindProperty("canOpen");
        interactionText_Open = serializedObject.FindProperty("interactionText_Open");
        interactionText_Close = serializedObject.FindProperty("interactionText_Close");

        titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        sectionStyle = new GUIStyle(EditorStyles.boldLabel)
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
        GUILayout.Label("Fuse Box Door", titleStyle);
        GUILayout.Space(20);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Door Settings", sectionStyle);
        EditorGUILayout.PropertyField(isOpen, new GUIContent("Is Open", "Current state of the door."));
        EditorGUILayout.PropertyField(canOpen, new GUIContent("Can Open", "Whether this door is allowed to be interacted with."));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundOpen"), new GUIContent("Opening Sound", "Door's opening sound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundClose"), new GUIContent("Closing Sound", "Door's closing sound"));
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Interaction Text", sectionStyle);
        EditorGUILayout.PropertyField(interactionText_Open, new GUIContent("Open Text", "Text shown when the door can be opened."));
        EditorGUILayout.PropertyField(interactionText_Close, new GUIContent("Close Text", "Text shown when the door can be closed."));
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
