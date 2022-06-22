using UnityEditor;
using UnityEngine;

namespace Samples
{
    /// <summary>
    ///     Simple custom editor used to show how to enable custom UI for XR Management
    ///     configuraton data.
    /// </summary>
    [CustomEditor(typeof(SampleSettings))]
    public class SampleSettingsEditor : Editor
    {
        private static readonly string k_RequiresProperty = "m_RequiresItem";
        private static readonly string k_RuntimeToggleProperty = "m_RuntimeToggle";

        private static readonly GUIContent k_ShowBuildSettingsLabel = new GUIContent("Build Settings");
        private static readonly GUIContent k_RequiresLabel = new GUIContent("Item Requirement");

        private static readonly GUIContent k_ShowRuntimeSettingsLabel = new GUIContent("Runtime Settings");
        private static readonly GUIContent k_RuntimeToggleLabel = new GUIContent("Should I stay or should I go?");

        private SerializedProperty m_RequiesItemProperty;
        private SerializedProperty m_RuntimeToggleProperty;

        private bool m_ShowBuildSettings = true;
        private bool m_ShowRuntimeSettings = true;

        /// <summary>Override of Editor callback.</summary>
        public override void OnInspectorGUI()
        {
            if (serializedObject == null || serializedObject.targetObject == null)
                return;

            if (m_RequiesItemProperty == null)
                m_RequiesItemProperty = serializedObject.FindProperty(k_RequiresProperty);
            if (m_RuntimeToggleProperty == null)
                m_RuntimeToggleProperty = serializedObject.FindProperty(k_RuntimeToggleProperty);

            serializedObject.Update();
            m_ShowBuildSettings = EditorGUILayout.Foldout(m_ShowBuildSettings, k_ShowBuildSettingsLabel);
            if (m_ShowBuildSettings)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_RequiesItemProperty, k_RequiresLabel);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            m_ShowRuntimeSettings = EditorGUILayout.Foldout(m_ShowRuntimeSettings, k_ShowRuntimeSettingsLabel);
            if (m_ShowRuntimeSettings)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_RuntimeToggleProperty, k_RuntimeToggleLabel);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}