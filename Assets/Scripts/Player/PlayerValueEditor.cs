// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(PlayerValue))]
// public class PlayerValueEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();

//         PlayerValue playerValue = (PlayerValue)target;

//         GUILayout.Space(10);

//         if (GUILayout.Button("Reset Values"))
//         {
//             // Set default values
//             playerValue.Gravity = -9.18f * 4f;
//             playerValue.groundDistance = 1.8f;
//             playerValue.speed = 30f;
//             playerValue.jumpHeight = 5f;
//             playerValue.sensitivity = 9f;

//             // Mark the object as dirty to save changes
//             EditorUtility.SetDirty(target);
//         }
//     }
// }
