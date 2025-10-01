using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerItem))]
public class PlayerItemCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerItem playerItem = (PlayerItem)target;

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Player Item", titleStyle);
        GUILayout.Space(20);

        // Item Reference
        EditorGUILayout.BeginVertical("box");
        GUIStyle sectionTitleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Item Info", sectionTitleStyle);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("itemInfo"), new GUIContent("Item Info", "Reference to the associated item data."));

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
