namespace Apex.Editor
{
    using Apex.Steering.Components;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(SteerForPathComponent), false), CanEditMultipleObjects]
    public class SteerForPathComponentEditor : ArrivalBaseEditor
    {
        protected override void CreateUI()
        {
            base.CreateUI();
        }
    }
}