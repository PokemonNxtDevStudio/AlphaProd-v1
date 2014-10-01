namespace Apex.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Apex.Common;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Editor base class for editors support attributed components.
    /// </summary>
    public abstract class AttributeMaskedEditorBase : Editor
    {
        private static Type _attributesEnumType;
        private static bool _lookupComplete;
        private static bool _enumFound;

        public override void OnInspectorGUI()
        {
            if (!_lookupComplete && _attributesEnumType == null)
            {
                var asm = Assembly.GetAssembly(typeof(EntityAttributesEnumAttribute));
                _attributesEnumType = asm.GetTypes().Where(t => t.IsEnum && Attribute.IsDefined(t, typeof(EntityAttributesEnumAttribute)) && t != typeof(DefaultEntityAttributesEnum)).FirstOrDefault();

                if (_attributesEnumType == null)
                {
                    _enumFound = false;
                    _attributesEnumType = typeof(DefaultEntityAttributesEnum);
                }
                else
                {
                    _enumFound = true;
                }

                _lookupComplete = true;
            }

            if (!_enumFound)
            {
                EditorGUILayout.HelpBox("To enable attribute specific behaviours, create an entity attribute enum and decorate it with the EntityAttributesEnum.", MessageType.Info);
            }

            ContinueGUI();
        }

        protected int GetValue(int currentValue, string fieldLabel)
        {
            if (!_enumFound)
            {
                return 0;
            }

            var curEnumVal = (Enum)Enum.ToObject(_attributesEnumType, currentValue);

            var newValRaw = EditorGUILayout.EnumMaskField(fieldLabel, curEnumVal) as IConvertible;

            var newVal = newValRaw.ToInt32(null);
            if (newVal != currentValue)
            {
                EditorUtility.SetDirty(this.target);
            }

            return newVal;
        }

        protected abstract void ContinueGUI();
    }
}
