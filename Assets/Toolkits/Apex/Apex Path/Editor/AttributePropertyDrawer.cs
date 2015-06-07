namespace Apex.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Apex.Common;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(AttributePropertyAttribute))]
    public class AttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!AttributesMaster.attributesEnabled)
            {
                EditorGUI.HelpBox(position, "To enable attribute specific behaviours, create an entity attribute enum and decorate it with the EntityAttributesEnum.", MessageType.Info);
                return;
            }

            var attrib = this.attribute as AttributePropertyAttribute;

            if (!string.IsNullOrEmpty(attrib.label))
            {
                label.text = attrib.label;
            }

            if (!string.IsNullOrEmpty(attrib.tooltip))
            {
                label.tooltip = attrib.tooltip;
            }

            EditorUtilities.EnumToIntField(position, property, AttributesMaster.attributesEnumType, label);
        }
    }
}
