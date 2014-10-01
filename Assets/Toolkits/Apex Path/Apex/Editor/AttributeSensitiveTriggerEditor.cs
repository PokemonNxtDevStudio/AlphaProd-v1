namespace Apex.Editor
{
    using System;
    using Apex.Common;
    using UnityEditor;

    /// <summary>
    /// Editor for <see cref="AttributeSensitiveTrigger"/>s.
    /// </summary>
    [CustomEditor(typeof(AttributeSensitiveTrigger), true)]
    public class AttributeSensitiveTriggerEditor : AttributeMaskedEditorBase
    {
        protected override void ContinueGUI()
        {
            var t = this.target as AttributeSensitiveTrigger;

            t.triggeredBy = GetValue(t.triggeredBy, "Triggered By");

            DrawDefaultInspector();
        }
    }
}
