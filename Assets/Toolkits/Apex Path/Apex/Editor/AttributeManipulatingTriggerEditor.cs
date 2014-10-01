namespace Apex.Editor
{
    using System;
    using Apex.Common;
    using UnityEditor;

    /// <summary>
    /// Editor for <see cref="AttributeManipulatingTrigger"/>s.
    /// </summary>
    [CustomEditor(typeof(AttributeManipulatingTrigger))]
    public class AttributeManipulatingTriggerEditor : AttributeMaskedEditorBase
    {
        protected override void ContinueGUI()
        {
            var t = this.target as AttributeManipulatingTrigger;

            t.applies = GetValue(t.applies, "Applies");
            t.removes = GetValue(t.removes, "Removes");

            DrawDefaultInspector();
        }
    }
}
