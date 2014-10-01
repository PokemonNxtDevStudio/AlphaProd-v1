namespace Apex.Editor
{
    using System;
    using Apex.Common;
    using UnityEditor;

    /// <summary>
    /// Editor for <see cref="AttributedComponent"/>s.
    /// </summary>
    [CustomEditor(typeof(AttributedComponent), true)]
    public class AttributedComponentEditor : AttributeMaskedEditorBase
    {
        protected sealed override void ContinueGUI()
        {
            var unit = this.target as AttributedComponent;

            unit.attributes = GetValue(unit.attributes, "Attributes");

            FinishGUI();
        }

        protected virtual void FinishGUI()
        {
            DrawDefaultInspector();
        }
    }
}
