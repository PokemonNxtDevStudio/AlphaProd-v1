using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using NXT.Inventory;

namespace TeamName.Editors
{
    public class PokeComponentPicker : ItemPickerBase<Type>
    {

        public static PokeComponentPicker Get(string title = "Item type picker", Vector2 minSize = new Vector2())
        {
            var window = GetWindow<PokeComponentPicker>(true);
            window.windowTitle = title;
            window.minSize = minSize;
            window.isUtility = true;

            return window;
        }

        protected override IList<Type> FindObjects(bool searchProjectFolder)
        {
            var types = new List<Type>(16);
            foreach (var script in Resources.FindObjectsOfTypeAll<MonoScript>())
            {
                if (script.GetClass() != null && script.GetClass() == typeof(Pokemon))
                    types.Add(script.GetClass());
                if (script.GetClass() != null && script.GetClass() == typeof(MotorController))
                    types.Add(script.GetClass());
            }

            return types;
        }

        protected override bool MatchesSearch(Type obj, string search)
        {
            return obj.Name.ToLower().Contains(search);
        }

        protected override void DrawObjectButton(Type item)
        {
            if (GUILayout.Button(item.Name))
                NotifyPickedObject(item);
        }
    }
}
