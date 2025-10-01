using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShotgunScript))]
public class ShotgunCustomEditor : Editor
{
    private bool showReferences = true;
    private bool showWeaponSettings = true;
    private bool showShellSettings = true;
    private bool showKeyBinds = true;

    public override void OnInspectorGUI()
    {
        ShotgunScript shotgun = (ShotgunScript)target;

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Shotgun Script", titleStyle);
        GUILayout.Space(20);

        serializedObject.Update();

        // References
        showReferences = EditorGUILayout.Foldout(showReferences, "References", true);
        if (showReferences)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("References", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MuzzleFlash"), new GUIContent("Muzzle Flash", "Particle system that plays on firing."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletsUI"), new GUIContent("Bullets UI", "UI text displaying current ammo."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shootOrigin"), new GUIContent("Shoot Origin", "Transform from which bullets are fired."));
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"), new GUIContent("Clip", "Current ammo in clip."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clipSize"), new GUIContent("Clip Size", "Maximum ammo per clip."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("range"), new GUIContent("Range", "Effective range of the shotgun."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRadius"), new GUIContent("Attack Radius", "Radius for hit detection."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shootCooldown"), new GUIContent("Shoot Cooldown", "Delay between shots."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("reloadTime"), new GUIContent("Reload Time", "Time required to reload."));

            GUILayout.Space(5);
            GUILayout.Label("Pellet Settings", new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            EditorGUILayout.PropertyField(serializedObject.FindProperty("pelletCount"), new GUIContent("Pellet Count", "Number of pellets per shot."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("spreadAngle"), new GUIContent("Spread Angle", "Random angle spread for each pellet."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damagePerPellet"), new GUIContent("Damage Per Pellet", "Damage applied per pellet."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Bullet Shell Ejection
        showShellSettings = EditorGUILayout.Foldout(showShellSettings, "Bullet Shell Ejection Settings", true);
        if (showShellSettings)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Bullet Shell Ejection", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletShellEjection"), new GUIContent("Enable Ejection", "Enable/Disable bullet shell ejection."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletShellRemoveTimer"), new GUIContent("Remove Timer", "Time after which shell is destroyed."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BulletShell"), new GUIContent("Bullet Shell Prefab", "Prefab for the ejected shell."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("BulletShellEjectionTransform"), new GUIContent("Ejection Transform", "Transform from which shell is ejected."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Key Binds
        showKeyBinds = EditorGUILayout.Foldout(showKeyBinds, "Key Binds", true);
        if (showKeyBinds)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Key Binds", titleStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Shoot_Key"), new GUIContent("Shoot Key", "Key used to shoot."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Reload_Key"), new GUIContent("Reload Key", "Key used to reload."));
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}