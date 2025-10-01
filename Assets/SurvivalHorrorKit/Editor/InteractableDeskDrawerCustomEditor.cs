using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableDrawer))]
public class InteractableDrawerCustomEditor : Editor
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

        titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

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
        GUILayout.Label("Interactable Desk Drawer", titleStyle);
        GUILayout.Space(20);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Interaction Settings", sectionTitleStyle);
        EditorGUILayout.PropertyField(canOpen, new GUIContent("Can Open", "Is this drawer interactable?"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundOpen"), new GUIContent("Opening Sound", "Door's opening sound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundClose"), new GUIContent("Closing Sound", "Door's closing sound"));
        EditorGUILayout.PropertyField(interactionText_Open, new GUIContent("Open Text", "Text shown when the drawer is closed."));
        EditorGUILayout.PropertyField(interactionText_Close, new GUIContent("Close Text", "Text shown when the drawer is open."));
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
