using UnityEngine;
using System.Collections;

public class MoveAssetDatabase : ScriptableObject 
{

    public MoveData[] Moves;

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
    }
}
