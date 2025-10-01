using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableItem))]
public class InteractableItemCustomEditor : Editor
{
    private bool showGeneralSettings = true;

    public override void OnInspectorGUI()
    {
        InteractableItem item = (InteractableItem)target;

        // Title
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Interactable Item", titleStyle);
        GUILayout.Space(20);

        // General Settings
        showGeneralSettings = EditorGUILayout.Foldout(showGeneralSettings, "General Settings", true);
        if (showGeneralSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUIStyle sectionStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.Label("General Settings", sectionStyle);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemInfo"), new GUIContent("Item Info", "The item data this object represents."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isActive"), new GUIContent("Is Active", "Determines if the item can currently be interacted with."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("interactionText"), new GUIContent("Interaction Text", "The text that is shown when the item can be interacted with"));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

