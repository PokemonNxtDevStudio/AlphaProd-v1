using UnityEngine;
using UnityEngine.UI;
using System;
using NXT.Inventory;
using TeamName.Inventory.Models;
using TeamName.Inventory;

[Serializable]
public class Entity
{

   


    [SerializeField]
    protected string m_name;
    public string Name { get { return m_name; } set { m_name = value; } }
    [SerializeField]
    protected int m_id;
    public int ID { get { return m_id; } set { m_id = value; } }
    [SerializeField]
    protected Sprite m_icon;
    public Sprite Icon { get { return m_icon; } set { m_icon = value; } }
    [SerializeField]
    protected string m_description;
    public string Description { get { return m_description; } set { m_description = value; } }
    /// <summary>
    /// categoryID
    /// </summary>
    public uint categoryID;

   


}