
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using TeamName.Editors.Database;

namespace TeamName.Editors.Database
{
	public class MoveEditor : DatabaseEditorBase<MoveData>
	{
		protected override List<MoveData> crudList
		{
			get { return new List<MoveData>(EditorUtils.selectedPokeDatabase.moveList); }
			set { EditorUtils.selectedPokeDatabase.moveList= value.ToArray(); }
		}
		
		public Editor itemEditorInspector;
		
		
		
		
		public MoveEditor(string singleName, string pluralName, EditorWindow window)
			: base(singleName, pluralName, window)
		{
			
		}
		
		protected override bool MatchesSearch(MoveData item, string searchQuery)
		{
			string search = searchQuery.ToLower();
			return (item.ID.ToString().Contains(search) || item.Name.ToLower().Contains(search));
		}
		
		protected override void CreateNewItem()
		{

			var item = new MoveData();
			item.ID = (crudList.Count > 0) ? crudList[crudList.Count - 1].ID + 1 : 0;
			AddItem(item, true);
			EditorUtility.SetDirty(EditorUtils.selectedPokeDatabase);
		}
		/// <summary>
		/// Removes the item.this is comments allalala
		/// </summary>
		/// <param name="i">The index.</param>
		public override void RemoveItem(int i)
		{
			//AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(EditorUtils.selectedPokeDatabase.PokemonsList[i]));
			base.RemoveItem(i);
		}
		
		protected override void DrawSidebarRow(MoveData item, int i)
		{
			//GUI.color = new Color(1.0f,1.0f,1.0f);
			BeginSidebarRow(item, i);
			
			DrawSidebarRowElement("#" + item.ID.ToString(), 40);
			DrawSidebarRowElement(item.Name, 260);
			
			EndSidebarRow(item, i);
		}
		
		protected override void DrawDetail(MoveData item, int index)
		{
			EditorGUILayout.BeginVertical(DatabaseEditorStyles.boxStyle);
			
			EditorGUILayout.LabelField("ID", item.ID.ToString());
			EditorGUILayout.Space();
			
			EditorGUILayout.LabelField("The name of the Move, is displayed in the tooltip in UI elements.", DatabaseEditorStyles.labelStyle);
			item.Name = EditorGUILayout.TextField("Move name", item.Name);
			item.TM = EditorGUILayout.IntField("TM #", item.TM);
			item.PP = EditorGUILayout.FloatField("PP Cost", item.PP);
			item.Power = EditorGUILayout.FloatField("Attack Power", item.Power);
			item.MoveCategory = (MoveCategory)EditorGUILayout.EnumPopup ("Move Category ",item.MoveCategory);
			item.MoveEffect = (MoveEffect)EditorGUILayout.EnumPopup ("Move Effect" ,item.MoveEffect);
			item.MoveType = (MoveType)EditorGUILayout.EnumPopup ("MoveType" , item.MoveType);

			item.Icon = (Sprite)EditorGUILayout.ObjectField("Move Icon",(Object)item.Icon,typeof(Sprite),true);
			item.MoveVFXPrefab = (GameObject)EditorGUILayout.ObjectField("VFX Prefab  ",(Object)item.MoveVFXPrefab,typeof(GameObject),true);
			item.MoveAudio = (AudioClip)EditorGUILayout.ObjectField ("Audio Clip ", (Object)item.MoveAudio, typeof(AudioClip), true);

			EditorGUILayout.LabelField("Note : Moves could potentially go on global cooldown by category", DatabaseEditorStyles.labelStyle);
			GUI.color = Color.yellow;
			GUI.color = Color.white;
			item.MoveCooldown = EditorGUILayout.Slider("Cooldown time (seconds)", item.MoveCooldown, 0.0f, 60.0f);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Description", GUILayout.Width(EditorGUIUtility.labelWidth - 5));
			EditorGUILayout.BeginVertical();
			EditorGUILayout.HelpBox("Limit to 255 charactors", MessageType.Info);
			item.Description = EditorGUILayout.TextArea(item.Description, DatabaseEditorStyles.richTextArea);
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			
		
			
			EditorGUILayout.EndVertical();
		}
		
		protected override bool IDsOutOfSync()
		{
			return false;
		}
		
		protected override void SyncIDs()
		{
			
		}
	}
}
