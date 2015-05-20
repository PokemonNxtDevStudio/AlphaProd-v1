using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MoveAssetDatabase : ScriptableObject 
{

  //  public MoveData[] Moves;
    [SerializeField]
    private List<MoveData> m_MovesList = new List<MoveData>();
   [HideInInspector]
    public List<MoveData> MovesList { get { return m_MovesList; } /*set { m_MovesList = value; } */}
/*
    /// <summary>
    /// Get the specified SpellInfo by index.
    /// </summary>
    /// <param name="index">Index.</param>
    public MoveData Get(int index)
    {
        return (Moves[index]);
    }
    /// <summary>
    /// Gets the specified SpellInfo by ID.
    /// </summary>
    /// <returns>The SpellInfo or NULL if not found.</returns>
    /// <param name="ID">The spell ID.</param>
    public MoveData GetByID(int ID)
    {
        for (int i = 0; i < Moves.Length; i++)
        {
            if (Moves[i].ID == ID)
                return Moves[i];
        }
        return null;
    }*/
    public void AddToList(MoveData move)
    {
        if(!MovesList.Contains(move))
        {
            MovesList.Add(move);
            EditorUtility.SetDirty(this);
        }
    }
    public MoveData GetByIDInList(int ID)
    {
        MoveData tem = null;
        for(int i = 0;i < MovesList.Count ; i++)
        {
            if(MovesList[i].ID == ID)
            {
                tem = MovesList[i];
            }
        }
        return tem;
    }
}
