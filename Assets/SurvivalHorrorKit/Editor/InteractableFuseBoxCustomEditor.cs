using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractableFuseBox))]
public class InteractableFuseBoxCustomEditor : Editor
{
    private SerializedProperty interactionText_RemoveFuse;
    private SerializedProperty interactionText_AddFuse;
    private SerializedProperty fusesNeeded;
    private SerializedProperty lights;
    private SerializedProperty fuseModels;

    private GUIStyle titleStyle;
    private GUIStyle sectionStyle;

    private void OnEnable()
    {
        interactionText_RemoveFuse = serializedObject.FindProperty("interactionText_RemoveFuse");
        interactionText_AddFuse = serializedObject.FindProperty("interactionText_AddFuse");
        fusesNeeded = serializedObject.FindProperty("fusesNeeded");
        lights = serializedObject.FindProperty("lights");
        fuseModels = serializedObject.FindProperty("fuseModels");

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
        GUILayout.Label("Fuse Box Interactable", titleStyle);
        GUILayout.Space(20);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Interaction Text", sectionStyle);
        EditorGUILayout.PropertyField(interactionText_RemoveFuse, new GUIContent("Remove Fuse Text", "Displayed when the player can remove a fuse."));
        EditorGUILayout.PropertyField(interactionText_AddFuse, new GUIContent("Add Fuse Text", "Displayed when the player can add a fuse."));
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Fuse Configuration", sectionStyle);
        EditorGUILayout.IntSlider(fusesNeeded, 0, 2, new GUIContent("Fuses Needed", "How many fuses are required to power the box."));
        EditorGUILayout.PropertyField(fuseModels, new GUIContent("Fuse Models", "Fuse model objects to activate/deactivate based on fuse state."), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundOn"), new GUIContent("Power Off sound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("soundOff"), new GUIContent("PowerOnSound"));
        EditorGUILayout.EndVertical();

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Lights Powered", sectionStyle);
        EditorGUILayout.PropertyField(lights, new GUIContent("Lights", "Lights that are powered by this fuse box."), true);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
