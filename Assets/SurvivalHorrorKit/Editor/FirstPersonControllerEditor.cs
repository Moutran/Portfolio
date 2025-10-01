/*using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FirstPersonController))]
public class FirstPersonControllerEditor : Editor
{
    private bool showMovementSettings = true;
    private bool showMouseSettings = true;
    private bool showFovSettings = true;
    private bool showCrouchSettings = true;
    private bool showSprintStaminaSettings = true;
    private bool showUIElements = true;
    private bool showHealthSettings = true;

    public override void OnInspectorGUI()
    {
        FirstPersonController controller = (FirstPersonController)target;

        // Title section
        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 18; // Increased font size
        titleStyle.fontStyle = FontStyle.Bold;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("First Person Controller", titleStyle);
        GUILayout.Space(20);

        // Movement Settings Window
        showMovementSettings = EditorGUILayout.Foldout(showMovementSettings, "Movement Settings", true);
        if (showMovementSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUIStyle sectionTitleStyle = new GUIStyle(GUI.skin.label);
            sectionTitleStyle.fontSize = 18; // Increased font size for section titles
            sectionTitleStyle.fontStyle = FontStyle.Bold;
            sectionTitleStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("Movement Settings", sectionTitleStyle); // Centered title
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkSpeed"), new GUIContent("Walk Speed", "The speed at which the player walks."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintSpeed"), new GUIContent("Sprint Speed", "The speed at which the player sprints."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchSpeed"), new GUIContent("Crouch Speed", "The speed at which the player crouches."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jumpForce"), new GUIContent("Jump Force", "The force applied when the player jumps."));
            EditorGUILayout.Slider(serializedObject.FindProperty("crouchHeightMultiplier"), 0f, 1f, new GUIContent("Crouch Height Multiplier", "Multiplier for the player's height when crouching."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("groundCheckDistance"), new GUIContent("Ground Check Distance", "The distance used to check if the player is on the ground."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Mouse Settings Window
        showMouseSettings = EditorGUILayout.Foldout(showMouseSettings, "Mouse Settings", true);
        if (showMouseSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Mouse Settings", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mouseSensitivity"), new GUIContent("Mouse Sensitivity", "The sensitivity of the mouse movement."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("verticalLookLimit"), new GUIContent("Vertical Look Limit", "Limits the vertical movement of the mouse to prevent over-rotation."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // FOV Settings Window
        showFovSettings = EditorGUILayout.Foldout(showFovSettings, "FOV Settings", true);
        if (showFovSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("FOV Settings", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.Slider(serializedObject.FindProperty("normalFOV"), 0f, 100f, new GUIContent("Normal FOV", "The normal field of view of the camera."));
            EditorGUILayout.Slider(serializedObject.FindProperty("sprintFOV"), 0f, 100f, new GUIContent("Sprint FOV", "The field of view when sprinting."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fovStepTime"), new GUIContent("FOV Step Time", "The time it takes to transition between normal FOV and sprint FOV."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Crouch Settings Window
        showCrouchSettings = EditorGUILayout.Foldout(showCrouchSettings, "Crouch Settings", true);
        if (showCrouchSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Crouch Settings", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.PropertyField(serializedObject.FindProperty("crouchCooldown"), new GUIContent("Crouch Cooldown", "Cooldown time in seconds before the player can toggle crouch again."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Sprint Stamina Settings Window
        showSprintStaminaSettings = EditorGUILayout.Foldout(showSprintStaminaSettings, "Sprint Stamina Settings", true);
        if (showSprintStaminaSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Sprint Stamina Settings", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStamina"), new GUIContent("Max Stamina", "The maximum amount of stamina the player can have."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("staminaDrainRate"), new GUIContent("Stamina Drain Rate", "The rate at which stamina is drained while sprinting."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("staminaRegenRate"), new GUIContent("Stamina Regen Rate", "The rate at which stamina regenerates when not sprinting."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintCooldownThreshold"), new GUIContent("Sprint Cooldown Threshold", "The stamina threshold below which sprinting will be disabled."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        showHealthSettings = EditorGUILayout.Foldout(showHealthSettings, "Health Settings", true);
        if (showHealthSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Health Settings", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"), new GUIContent("Max Health", "The maximum amount of health the player can have."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentHealth"), new GUIContent("Health", "Player's current health."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // UI Elements Window
        showUIElements = EditorGUILayout.Foldout(showUIElements, "UI Elements", true);
        if (showUIElements)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("UI Elements", new GUIStyle(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprintBar"), new GUIContent("Sprint Bar", "UI element showing the player's current stamina."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("healthBar"), new GUIContent("Health Bar", "UI element showing the player's current health."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hidingIcon"), new GUIContent("Hiding Icon", "UI element showing the player is hiding"));
            EditorGUILayout.EndVertical();
        }

        // Apply changes to serializedObject
        serializedObject.ApplyModifiedProperties();
    }
}*/
