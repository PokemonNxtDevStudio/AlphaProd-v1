using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IconSlot : MonoBehaviour
{

    // Use this for initialization

    public enum InternalType
    {
        Normal,
        Temporary,
    }

    public Image iconSprite;

    private Object sourceSlot;
    void Start()
    {


        if (this.iconSprite == null)
        {
            Debug.LogWarning(this.GetType() + " requires that you define a icon sprite in order to work.", this);
            this.enabled = false;
            return;
        }
        this.OnStart();
    }

    /// <summary>
    /// Raises the start event.
    /// </summary>
    public virtual void OnStart() { }

      /// <summary>
    /// Sets the source slot, used by the temporary slot.
    /// </summary>
    /// <param name="slot">Slot.</param>
    public void SetSource(Object slot)
    {
        this.sourceSlot = slot;
    }

    public IconSlot GetSource()
    {
        return this.sourceSlot as IconSlot;
    }

    public virtual bool IsAssigned()
    {
        return (this.GetIcon() != null);
    }

    public bool Assign(Sprite icon)
    {
        if (icon == null)
            return false;

        // Unassign this slot
        this.Unassign();

        // Set the icon
        this.SetIcon(icon);

        return true;
    }

    /// <summary>
    /// Assign the specified slot by source object.
    /// </summary>
    /// <param name="source">Source.</param>
    public virtual bool Assign(Object source)
    {
        if (source is Sprite)
        {
            return this.Assign(source as Sprite);
        }
        else if (source is IconSlot)
        {
            IconSlot sourceSlot = source as IconSlot;

            if (sourceSlot != null)
                return this.Assign(sourceSlot.GetIconSprite());
        }

        return false;
    }

    /// <summary>
    /// Unassign this slot.
    /// </summary>
    public virtual void Unassign()
    {
        // Remove the icon
        this.SetIcon(null);
    }

    /// <returns>The icon.</returns>
    public Texture GetIcon()
    {
        return this.iconSprite.mainTexture;
    }

    /// <returns>The icon.</returns>
    public Sprite GetIconSprite()
    {
        return this.iconSprite.sprite;
    }

    /// <summary>
    /// Sets the icon of this slot.
    /// </summary>
    /// <param name="iconTex">The icon texture.</param>
    public void SetIcon(Sprite iconTex)
    {
        if (iconTex != null && !this.iconSprite.enabled)
        {
            this.iconSprite.enabled = true;

        }

        if (iconTex == null && this.iconSprite.enabled)
        {
            this.iconSprite.enabled = false;
        }

        this.iconSprite.sprite = iconTex;

    }


}
