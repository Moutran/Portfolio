using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PistolScript))]
public class PistolCustomEditor : Editor
{
    private bool showReferences = true;
    private bool showWeaponSettings = true;
    private bool showShellSettings = true;
    private bool showJamSettings = true;
    private bool showKeyBinds = true;

    public override void OnInspectorGUI()
    {
        PistolScript pistol = (PistolScript)target;

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Pistol Script", titleStyle);
        GUILayout.Space(20);

        serializedObject.Update();

        // References
        showReferences = EditorGUILayout.Foldout(showReferences, "References", true);
        if (showReferences)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("References", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MuzzleFlash"), new GUIContent("Muzzle Flash", "Particle system played when firing."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletsUI"), new GUIContent("Bullets UI", "Text element displaying bullets."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Weapon Settings
        showWeaponSettings = EditorGUILayout.Foldout(showWeaponSettings, "Weapon Settings", true);
        if (showWeaponSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Weapon Settings", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ammo"), new GUIContent("Ammo", "Total ammo available."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"), new GUIContent("Clip", "Ammo currently in the clip."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clipSize"), new GUIContent("Clip Size", "Maximum ammo per clip."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("range"), new GUIContent("Range", "Maximum shooting range."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shootCooldown"), new GUIContent("Shoot Cooldown", "Delay between shots."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("reloadTime"), new GUIContent("Reload Time", "Time it takes to reload."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage", "Damage per shot."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("reloadTime"), new GUIContent("Reload Time", "Time it takes to reload.")); 
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage", "Damage per shot."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundShoot"), new GUIContent("Shooting Sound"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("soundReload"), new GUIContent("Reloading Sound"));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Bullet Shell Ejection
        showShellSettings = EditorGUILayout.Foldout(showShellSettings, "Bullet Shell Ejection Settings", true);
        if (showShellSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Bullet Shell Ejection", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletShellEjection"), new GUIContent("Enable Ejection", "Should shells be ejected?"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletShellRemoveTimer"), new GUIContent("Shell Remove Timer", "Time after which shell is destroyed."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BulletShell"), new GUIContent("Bullet Shell Prefab", "Prefab for the bullet shell."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BulletShellEjectionTransform"), new GUIContent("Ejection Transform", "Transform where the shell is ejected from."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Jam Settings
        showJamSettings = EditorGUILayout.Foldout(showJamSettings, "Jam Settings", true);
        if (showJamSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Jam Settings", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("canJam"), new GUIContent("Can Jam", "Can the weapon jam?"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("jamChance"), new GUIContent("Jam Chance", "Chance (0-100) of the weapon jamming on fire."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Key Binds
        showKeyBinds = EditorGUILayout.Foldout(showKeyBinds, "Key Binds", true);
        if (showKeyBinds)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Key Binds", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Shoot_Key"), new GUIContent("Shoot Key", "Key used for shooting."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Reload_Key"), new GUIContent("Reload Key", "Key used for reloading."));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
