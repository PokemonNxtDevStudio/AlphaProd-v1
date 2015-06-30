using NXT.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace TeamName.Editors.Database
{
    public class PokeEditor : DatabaseEditorBase<Pokemon>
    {
        protected class TypeFilter
        {
            public Type type { get; set; }
            public bool enabled { get; set; }

            public TypeFilter(Type type, bool enabled)
            {
                this.type = type;
                this.enabled = enabled;
            }
        }


        protected override List<Pokemon> crudList
        {
            get { return new List<Pokemon>(EditorUtils.selectedPokeDatabase.PokemonsList); }
            set { EditorUtils.selectedPokeDatabase.PokemonsList = value; }
        }

        public Editor itemEditorInspector;

        private List<TypeFilter> _allItemTypes;

        protected List<TypeFilter> allItemTypes
        {
            get
            {
                if (_allItemTypes == null)
                    _allItemTypes = GetAllItemTypes();

                return _allItemTypes;
            }
            set { _allItemTypes = value; }
        }



        public PokeEditor(string singleName, string pluralName, EditorWindow window)
            : base(singleName, pluralName, window)
        {
            if (selectedItem != null)
                itemEditorInspector = Editor.CreateEditor(selectedItem);
        }

        protected virtual List<TypeFilter> GetAllItemTypes()
        {
            var types = new List<TypeFilter>(16);
            foreach (var script in Resources.FindObjectsOfTypeAll<MonoScript>())
            {
                if (script.GetClass() != null && script.GetClass().IsSubclassOf(typeof(Pokemon)))
                    types.Add(new TypeFilter(script.GetClass(), false));
            }

            return types;
        }

        protected override bool MatchesSearch(Pokemon item, string searchQuery)
        {
            string search = searchQuery.ToLower();
            return (item.Name.ToLower().Contains(search) || item.Description.ToLower().Contains(search) ||
                item.ID.ToString().Contains(search) || item.GetType().Name.ToLower().Contains(search));
        }

        protected override void CreateNewItem()
        {
            //TODO-URGENT

            var picker = CreateNewPokeEditor.Get((System.Type type, GameObject obj, EditorWindow thisWindow) =>
            {
                string prefabPath = EditorPrefs.GetString("Pokemon_PrefabPath") + "/item_" + System.DateTime.Now.ToFileTimeUtc() + "_PFB.prefab";

                //var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var prefab = PrefabUtility.CreatePrefab(prefabPath, obj);

                AssetDatabase.SetLabels(prefab, new string[] { "InventoryItemPrefab" });

                var comp = (Pokemon)prefab.AddComponent(type);
                comp.ID = crudList.Count == 0 ? 0 : crudList[crudList.Count - 1].ID + 1;

                // if (prefab.GetComponent<ObjectTriggererItem>() == null)
                //prefab.AddComponent<ObjectTriggererItem>();

                // Avoid deleting the actual prefab / model, only the cube / internal models without an asset path.
                if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(obj)))
                    UnityEngine.Object.DestroyImmediate(obj);

                AddItem(comp, true);
                thisWindow.Close();
            });
            picker.Show();

        }



        public override void RemoveItem(int i)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(EditorUtils.selectedPokeDatabase.PokemonsList[i]));
            base.RemoveItem(i);
        }

        public override void EditItem(Pokemon item)
        {
            base.EditItem(item);

            //var asset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(item), typeof(InventoryItemBase)) as InventoryItemBase;
            //EditorGUIUtility.PingObject(asset);

            if (selectedItem != null)
                itemEditorInspector = Editor.CreateEditor(selectedItem);
        }

        protected override void DrawSidebar()
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();

            int i = 0;
            foreach (var type in allItemTypes)
            {
                if (i % 3 == 0)
                    GUILayout.BeginHorizontal();

                type.enabled = GUILayout.Toggle(type.enabled, type.type.Name.Replace("InventoryItem", ""), "OL Toggle");

                if (i % 3 == 2 || i == allItemTypes.Count - 1)
                    GUILayout.EndHorizontal();

                i++;
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            base.DrawSidebar();

        }

        protected override void DrawSidebarRow(Pokemon item, int i)
        {
            int checkedCount = 0;
            foreach (var type in allItemTypes)
            {
                if (type.enabled)
                    checkedCount++;
            }

            if (checkedCount > 0)
                if (allItemTypes.FirstOrDefault(o => o.type == item.GetType() && o.enabled) == null)
                    return;


            //GUI.color = new Color(1.0f,1.0f,1.0f);
            BeginSidebarRow(item, i, 280);

            DrawSidebarRowElement("#" + item.ID.ToString(), 40);
            DrawSidebarRowElement(item.Name, 120);
            DrawSidebarRowElement(item.GetType().Name.ToString().Replace("InventoryItem", ""), 120);
            bool t = DrawSidebarRowElementToggle(true, "", "VisibilityToggle", 20);
            if (t == false) // User clicked view icon
                AssetDatabase.OpenAsset(selectedItem);

            EndSidebarRow(item, i);
        }

        protected override void DrawDetail(Pokemon item, int index)
        {
            EditorUtils.ErrorIfEmpty(EditorPrefs.GetString("InventorySystem_ItemPrefabPath") == string.Empty, "Inventory item prefab folder is not set, items cannot be saved! Please go to settings and define the Inventory item prefab folder.");
            if (EditorPrefs.GetString("InventorySystem_ItemPrefabPath") == string.Empty)
            {
                canCreateItems = false;
                return;
            }
            canCreateItems = true;

            GUILayout.Label("Use the inspector if you want to add custom components.", DatabaseEditorStyles.titleStyle);
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            itemEditorInspector.OnInspectorGUI();

            string newName = "Item_" + (string.IsNullOrEmpty(item.Name) ? string.Empty : item.Name.ToLower().Replace(" ", "_")) + "_#" + item.ID + "_" + EditorUtils.selectedDatabase.name + "_PFB";
            if (AssetDatabase.GetAssetPath(item).EndsWith(newName + ".prefab") == false)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(item), newName);
            }
        }

        protected override bool IDsOutOfSync()
        {
            uint next = 0;
            foreach (var item in crudList)
            {
                if (item == null || item.ID != next)
                    return true;

                next++;
            }

            return false;
        }

        protected override void SyncIDs()
        {
            Debug.Log("Item ID's out of sync, force updating...");

            crudList = crudList.Where(o => o != null).ToList();
            int lastID = 0;
            foreach (var item in crudList)
            {
                item.ID = lastID++;
                EditorUtility.SetDirty(item);
            }

            GUI.changed = true;
            EditorUtility.SetDirty(EditorUtils.selectedDatabase);
        }
    }
}
