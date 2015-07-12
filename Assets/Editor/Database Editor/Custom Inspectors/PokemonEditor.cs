using UnityEngine;
using UnityEditor;
using System.Collections;
using NXT.Inventory;
using TeamName.Editors;
using System.Collections.Generic;


[CustomEditor(typeof(Pokemon),true)]
public class PokemonEditor : Editor {


	protected SerializedProperty ID;
	protected SerializedProperty Name; // Name is used by Editor.name...
	protected SerializedProperty Description;
	protected SerializedProperty level;
	protected SerializedProperty movesList;
	protected SerializedProperty m_level;
	protected SerializedProperty m_pp;
	protected SerializedProperty m_health;
	protected SerializedProperty m_attack;
	protected SerializedProperty m_defence;
	protected SerializedProperty m_demage;

	protected SerializedProperty m_speed;
	protected SerializedProperty m_type1;
	protected SerializedProperty m_type2;


	private UnityEditorInternal.ReorderableList propertiesList { get; set; }
	public void OnEnable()
	{
		ID= serializedObject.FindProperty("m_id");
		Name = serializedObject.FindProperty("m_name");
		Description = serializedObject.FindProperty("m_description");
		movesList = serializedObject.FindProperty("m_moves");
		m_level  = serializedObject.FindProperty("m_level");
		m_pp  = serializedObject.FindProperty("m_pp");
		m_health  = serializedObject.FindProperty("m_health");
		m_attack  = serializedObject.FindProperty("m_attack");
		m_defence  = serializedObject.FindProperty("m_defence");
		m_demage  = serializedObject.FindProperty("m_demage");
		m_speed  = serializedObject.FindProperty("m_speed");
		m_type1 = serializedObject.FindProperty("m_type1");
		m_type2 = serializedObject.FindProperty("m_type2");
		

		var t = (Pokemon)target;

		
		propertiesList = new UnityEditorInternal.ReorderableList(serializedObject, movesList, true, true, true, true);
		propertiesList.drawHeaderCallback += rect => GUI.Label(rect, "Move List");
		propertiesList.drawElementCallback += (rect, index, active, focused) =>
		{
			rect.height = 16;
			rect.y += 2;
			
			var popupRect = rect;
			popupRect.width /= 2;
			popupRect.width -= 5; // Some spacing
			
			var i = t.Moves[index];
			
			// Variables
			i.ID = EditorGUI.Popup(popupRect,i.ID , EditorUtils.moveNames);
			if (i.ID >= EditorUtils.selectedPokeDatabase.moveList.Length)
				i.ID = EditorUtils.selectedPokeDatabase.moveList.Length - 1;
			
			popupRect.x += popupRect.width;
			popupRect.x += 5;
			
			i.ID = Mathf.Max(i.ID, 0);
			i.levelLearnt = EditorGUI.IntField(popupRect, i.levelLearnt);
			
			// Changed something, copy property data
			if (GUI.changed)
			{
				// We're actually copying the values, can't edit source, because of value
				var db = EditorUtils.selectedPokeDatabase;
				if (db.moveList.Length > 0)
				{
					i.Name= db.moveList[i.ID].Name;
					//i.showInUI = db.properties[i.ID].showInUI;
					//i.uiColor = db.properties[i.ID].uiColor;
				}
				
				EditorUtility.SetDirty(target);
				serializedObject.ApplyModifiedProperties();
				Repaint();
			}
		};
		propertiesList.onAddCallback += (list) =>
		{
			var l = new List<MoveData>(t.Moves);
			l.Add(new MoveData());
			t.Moves = l;
			
			GUI.changed = true; // To save..
			EditorUtility.SetDirty(target);
			serializedObject.ApplyModifiedProperties();
			Repaint();
		};
		
		
	}
	
	private IEnumerator DestroyImmediateThis(Pokemon obj)
	{
		yield return null;
		DestroyImmediate(obj.gameObject, false); // Destroy this object
	}
	
	public override void OnInspectorGUI()
	{
		OnCustomInspectorGUI();
	}
	protected void OnCustomInspectorGUI()
	{
		serializedObject.Update();
		serializedObject.ApplyModifiedProperties();
		GUILayout.Label("Basic Info", DatabaseEditorStyles.titleStyle);
		EditorGUILayout.PropertyField(ID);
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Description", GUILayout.Width(EditorGUIUtility.labelWidth - 5));
		EditorGUILayout.BeginVertical();
		EditorGUILayout.HelpBox("This will show up on pokedex ", MessageType.Info);
		Description.stringValue = EditorGUILayout.TextArea(Description.stringValue, DatabaseEditorStyles.richTextArea);
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal ();

		Name.stringValue = EditorGUILayout.TextField ("PokemonName", Name.stringValue);

		GUILayout.Label("Stats", DatabaseEditorStyles.titleStyle);
		EditorGUILayout.BeginVertical();
		EditorGUILayout.PropertyField(m_level);
		EditorGUILayout.PropertyField(m_pp);
		EditorGUILayout.PropertyField(m_health);
		EditorGUILayout.PropertyField(m_attack);
		EditorGUILayout.PropertyField(m_defence);
		EditorGUILayout.PropertyField(m_speed);
		EditorGUILayout.PropertyField(m_type1);
		EditorGUILayout.PropertyField(m_type2);
		EditorGUILayout.PropertyField(movesList);
		EditorGUILayout.EndVertical();
		#region Moves
		
		GUILayout.Label("Moves Organizer", DatabaseEditorStyles.titleStyle);
		GUILayout.Label("You can create Moves in the move editor ");
		
		EditorGUILayout.BeginVertical(DatabaseEditorStyles.reorderableListStyle);
		propertiesList.DoLayoutList();
		EditorGUILayout.EndVertical();
		serializedObject.ApplyModifiedProperties();
		#endregion
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
