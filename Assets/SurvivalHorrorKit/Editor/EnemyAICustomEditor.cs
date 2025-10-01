/*using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy_AI))]
public class EnemyAICustomEditor : Editor
{
    private bool showMovement = true;
    private bool showHealth = true;
    private bool showAttack = true;

    public override void OnInspectorGUI()
    {
        Enemy_AI enemy = (Enemy_AI)target;
        serializedObject.Update();

        GUILayout.Space(10);
        GUIStyle titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        GUILayout.Label("Survival Horror Kit", titleStyle);
        GUILayout.Label("Enemy AI", titleStyle);
        GUILayout.Space(20);

        // Movement
        showMovement = EditorGUILayout.Foldout(showMovement, "Movement Settings", true);
        if (showMovement)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Movement Settings", SectionHeaderStyle());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("walkSpeed"), new GUIContent("Walk Speed", "Speed while patrolling."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("chaseSpeed"), new GUIContent("Chase Speed", "Speed while chasing the player."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("stoppingDistance"), new GUIContent("Stopping Distance", "Minimum distance to stop when approaching target."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("patrolRadius"), new GUIContent("Patrol Radius", "Radius within which the enemy will patrol."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("proximityRange"), new GUIContent("Proximity Range", "Distance the enemy can detect the player in their proximity."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Health
        showHealth = EditorGUILayout.Foldout(showHealth, "Health Settings", true);
        if (showHealth)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Health Settings", SectionHeaderStyle());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"), new GUIContent("Max Health", "Maximum health of the enemy."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), new GUIContent("Current Health", "Current health value."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        // Attack
        showAttack = EditorGUILayout.Foldout(showAttack, "Attack Settings", true);
        if (showAttack)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Attack Settings", SectionHeaderStyle());
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("Damage", "Amount of damage dealt to player."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackCooldown"), new GUIContent("Attack Cooldown", "Cooldown between attacks."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"), new GUIContent("Attack Range", "Forward range of the attack detection."));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRadius"), new GUIContent("Attack Radius", "Radius for the attack OverlapSphere."));
            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        serializedObject.ApplyModifiedProperties();
    }

    private GUIStyle SectionHeaderStyle()
    {
        return new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
    }
}*/
