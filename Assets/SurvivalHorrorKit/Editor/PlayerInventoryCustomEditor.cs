/*using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInventoryScript))]
public class PlayerInventoryScriptEditor : Editor
{
    private bool showInventorySettings = true;
    private bool showUIElements = true;
    private bool showThrowSettings = true;

    public override void OnInspectorGUI()
    {
        PlayerInventoryScript inventory = (PlayerInventoryScript)target;
        serializedObject.Update();

        // Title section
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Player Inventory", titleStyle);
        GUILayout.Space(20);

        // Inventory Settings
        showInventorySettings = EditorGUILayout.Foldout(showInventorySettings, "Inventory Settings", true);
        if (showInventorySettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Player Inventory Settings", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemLibrary"), new GUIContent("Item Library", "All Items Listed In The Game"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playerInventory"), new GUIContent("Player Inventory", "List of the player's current items."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playerItemObjects"), new GUIContent("Player Item Objects", "Item GameObjects attached to the player prefab."));
            EditorGUILayout.IntSlider(serializedObject.FindProperty("inventoryCapacity"), 1, 20, new GUIContent("Inventory Capacity", "The total number of items the player can carry."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // UI Elements
        showUIElements = EditorGUILayout.Foldout(showUIElements, "UI Elements", true);
        if (showUIElements)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("UI Elements", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hotbarSlots"), new GUIContent("Hotbar Slots", "Image components for displaying item sprites."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hotbarBGSlots"), new GUIContent("Hotbar BG Slots", "Background images for highlighting selected items."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Throw Settings
        showThrowSettings = EditorGUILayout.Foldout(showThrowSettings, "Throw Settings", true);
        if (showThrowSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Throw Settings", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("throwItemKey"), new GUIContent("Throw Item Key", "Key used to throw the currently selected item."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("throwItemPosition"), new GUIContent("Throw Item Position", "Transform position where the thrown item will appear."));
            EditorGUILayout.IntSlider(serializedObject.FindProperty("throwItemForce"), 1, 500, new GUIContent("Throw Item Force", "Force applied to the thrown item."));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}*/

