namespace Apex.Editor
{
    using System;
    using Apex.WorldGeometry;
    using UnityEditor;

    /// <summary>
    /// Editor for <see cref="DynamicObstacle"/>s.
    /// </summary>
    [CustomEditor(typeof(DynamicObstacle))]
    public class DynamicObstacleComponentEditor : AttributeMaskedEditorBase
    {
        protected override void ContinueGUI()
        {
            var c = this.target as DynamicObstacle;

            c.exceptionsMask = GetValue(c.exceptionsMask, "Exceptions");

            DrawDefaultInspector();
        }
    }
}
