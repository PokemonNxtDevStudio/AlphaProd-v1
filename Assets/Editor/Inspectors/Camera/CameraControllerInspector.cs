using UnityEngine;
using UnityEditor;
using NXT;
using NXT.Controllers;

namespace NXT.Controllers
{
    /// <summary>
    /// Shows a custom inspector for CameraController.
    /// </summary>
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerInspector : Editor
    {
        private string[] m_CameraView = { "Third Person", "Top Down" };

        // CameraController
        private static bool m_MovementFoldout = true;
        private static bool m_ZoomMovementFoldout = true;
        private static bool m_ScopeMovementFoldout = true;
        private static bool m_PhysicsFoldout = true;
        private static bool m_RecoilFoldout = true;
        private static bool m_DeathOrbitFoldout = true;

        /// <summary>
        /// Initializes the InspectorUtility.
        /// </summary>
        private void OnEnable()
        {
            InspectorUtility.Initialize();
        }

        /// <summary>
        /// Draws the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var cameraController = target as CameraController;
            if (cameraController == null || serializedObject == null)
                return; // How'd this happen?

            // Show all of the fields.
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            var thirdPersonView = InspectorUtility.PropertyFromName(serializedObject, "m_ThirdPersonView");
            thirdPersonView.boolValue = EditorGUILayout.Popup("View Type", thirdPersonView.boolValue ? 0 : 1, m_CameraView) == 0;

            var characterProperty = InspectorUtility.PropertyFromName(serializedObject, "m_Character");
            characterProperty.objectReferenceValue = EditorGUILayout.ObjectField("Character", characterProperty.objectReferenceValue, typeof(GameObject), true, GUILayout.MinWidth(80)) as GameObject;
            if (characterProperty.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("This field is required. The character specifies the GameObject that the CameraController should interact with.", MessageType.Error);
            }
            else
            {
                //if ((characterProperty.objectReferenceValue as GameObject).GetComponent<PlayerInput>() == null)
               // {
                 //   EditorGUILayout.HelpBox("The Camera Controller component cannot reference an AI Agent. Ensure the Camera Controller is referencing a player controlled character.", MessageType.Error);
              //  }
            }

            var anchorProperty = InspectorUtility.PropertyFromName(serializedObject, "m_Anchor");
            anchorProperty.objectReferenceValue = EditorGUILayout.ObjectField("Anchor", anchorProperty.objectReferenceValue, typeof(Transform), true, GUILayout.MinWidth(80)) as Transform;
            if (anchorProperty.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("The anchor specifies the Transform that the camera should follow. If null it will use the Character's Transform.", MessageType.Info);
            }

            // Crosshairs are only applicable to the third person view.
            if (thirdPersonView.boolValue)
            {
                EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_CrosshairsLocation"));
            }

            var minPitchProperty = InspectorUtility.PropertyFromName(serializedObject, "m_MinPitchLimit");
            var maxPitchProperty = InspectorUtility.PropertyFromName(serializedObject, "m_MaxPitchLimit");
            var minValue = Mathf.Round(minPitchProperty.floatValue * 100f) / 100f;
            var maxValue = Mathf.Round(maxPitchProperty.floatValue * 100f) / 100f;
            InspectorUtility.DrawMinMaxLabeledFloatSlider("Pitch Limit", ref minValue, ref maxValue, thirdPersonView.boolValue ? -90 : 0, 90);
            minPitchProperty.floatValue = minValue;
            maxPitchProperty.floatValue = maxValue;

            // Cover yaw limits are only applicable to the third person view.
            if (thirdPersonView.boolValue)
            {
                var minYawProperty = InspectorUtility.PropertyFromName(serializedObject, "m_MinYawLimit");
                var maxYawProperty = InspectorUtility.PropertyFromName(serializedObject, "m_MaxYawLimit");
                minValue = Mathf.Round(minYawProperty.floatValue * 100f) / 100f;
                maxValue = Mathf.Round(maxYawProperty.floatValue * 100f) / 100f;
                InspectorUtility.DrawMinMaxLabeledFloatSlider("Cover Yaw Limit", ref minValue, ref maxValue, -180, 180);
                minYawProperty.floatValue = minValue;
                maxYawProperty.floatValue = maxValue;
            }

            if ((m_MovementFoldout = EditorGUILayout.Foldout(m_MovementFoldout, "Movement Options", InspectorUtility.BoldFoldout)))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_MoveSmoothing"));
                EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_CameraOffset"), true);
                // The following properties are only applicable to the third person view.
                if (thirdPersonView.boolValue)
                {
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_NormalFOV"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_TurnSmoothing"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_TurnSpeed"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_CanTurnInAir"));
                }
                else
                {
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_RotationSpeed"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ViewDistance"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ViewStep"));
                }
                EditorGUI.indentLevel--;
            }

            // The following properties are only applicable to the third person view.
            if (thirdPersonView.boolValue)
            {
                if ((m_ZoomMovementFoldout = EditorGUILayout.Foldout(m_ZoomMovementFoldout, "Zoom Movement Options", InspectorUtility.BoldFoldout)))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_AllowZoom"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ZoomTurnSmoothing"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ZoomCameraOffset"), true);
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ZoomFOV"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_FOVSpeed"));
                    EditorGUI.indentLevel--;
                }

                if ((m_ScopeMovementFoldout = EditorGUILayout.Foldout(m_ScopeMovementFoldout, "Scope Movement Options", InspectorUtility.BoldFoldout)))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ScopeTurnSmoothing"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ScopeCameraOffset"), true);
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_ScopeFOV"));
                    EditorGUI.indentLevel--;
                }
            }

            if ((m_PhysicsFoldout = EditorGUILayout.Foldout(m_PhysicsFoldout, "Physics Options", InspectorUtility.BoldFoldout)))
            {
                EditorGUI.indentLevel++;
                if (thirdPersonView.boolValue)
                {
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_DisableRendererDistance"));
                }
                EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_CollisionRadius"));
                EditorGUI.indentLevel--;
            }

            // The following properties are only applicable to the third person view.
            if (thirdPersonView.boolValue)
            {
                if ((m_RecoilFoldout = EditorGUILayout.Foldout(m_RecoilFoldout, "Recoil Options", InspectorUtility.BoldFoldout)))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_RecoilSpring"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_RecoilDampening"));
                    EditorGUI.indentLevel--;
                }

                if ((m_DeathOrbitFoldout = EditorGUILayout.Foldout(m_DeathOrbitFoldout, "Death Orbit Options", InspectorUtility.BoldFoldout)))
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_DeathAnchor"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_UseDeathOrbit"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_DeathRotationSpeed"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_DeathOrbitMoveSpeed"));
                    EditorGUILayout.PropertyField(InspectorUtility.PropertyFromName(serializedObject, "m_DeathOrbitDistance"));
                    EditorGUI.indentLevel--;
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(cameraController, "Inspector");
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(cameraController);
            }
        }
    }
}