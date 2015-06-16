using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


namespace TeamName.Editors.Database
{
    /// <summary>
    /// High level heiarchy of adding subViews to an Editor surfaace
    /// </summary>
    public class EditorWindowContainer : ICustomEditorWindow
    {

       public List<ICustomEditorWindow> childEditors = new List<ICustomEditorWindow>(8);

        public ICustomEditorWindow selectedEditor
        {
            get { return childEditors[toolbarIndex]; }
        }

        public string[] editorNames
        {
            get
            {
                string[] names = new string[childEditors.Count];
                for (int i = 0; i < childEditors.Count; i++)
                {
                    names[i] = childEditors[i].ToString();
                }

                return names;
            }
        }

        public int toolbarIndex;
        public string name { get; set; }
        public EditorWindow window { get; protected set; }
        public bool requiresDatabase { get; set; }

        public EditorWindowContainer(string name, EditorWindow window)
        {
            this.name = name;
            this.window = window;
            this.toolbarIndex = 0;
            this.requiresDatabase = false;
        }

        public virtual void Focus()
        {
            //if (selectedEditor != null)
            selectedEditor.Focus();
        }

        protected virtual void DrawToolbar()
        {
            if (toolbarIndex >= childEditors.Count || toolbarIndex < 0)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            for (int i = 0; i < childEditors.Count; i++)
            {
                string buttonType = "LargeButtonMid";
                if (i == 0)
                    buttonType = "LargeButtonLeft";
                else if (i == childEditors.Count - 1)
                    buttonType = "LargeButtonRight"; 

                if(i == toolbarIndex)
                    GUI.color = Color.gray;

                if (GUILayout.Button(editorNames[i], buttonType))
                {
                    toolbarIndex = i;
                    selectedEditor.Focus();
                }

                GUI.color = Color.white;
            }

            //var style = new GUIStyle(EditorStyles.toolbarButton);
            //style.fixedHeight = 30;

            //GUILayout.Button("Step 1", (GUIStyle)"GUIEditor.BreadcrumbLeft", GUILayout.Height(50));
            //GUILayout.Button("Step 2", (GUIStyle)"GUIEditor.BreadcrumbMid");
            //GUILayout.Button("Step 2", (GUIStyle)"GUIEditor.BreadcrumbMid");
            //GUILayout.Button("Step 2", (GUIStyle)"GUIEditor.BreadcrumbMid");

            //GUILayout.Label("Test text", (GUIStyle)"TL Selection H2");

            //GUI.color = new Color(0.8f, 0.8f, 0.8f);
            //toolbarIndex = GUILayout.Toolbar(toolbarIndex, editorNames, style);
            //GUI.color = Color.white;

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        /// <summary>
        /// Empty editor only draws child options
        /// </summary>
        public virtual void Draw()
        {
            DrawToolbar();

            for (int i = 0; i < childEditors.Count; i++)
            {
                if (childEditors[i] == selectedEditor)
                    childEditors[i].Draw();
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}