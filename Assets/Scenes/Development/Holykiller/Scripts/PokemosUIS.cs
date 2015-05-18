using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PokemosUIS : MonoBehaviour , IDropHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Pokemon poke;


    public void OnBeginDrag(PointerEventData eventData)
    {
        /*
        //Debug.Log ("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*
        //Debug.Log ("OnDrag");		
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent);

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.y < placeholderParent.GetChild(i).position.y)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    //	newSiblingIndex--;
                    break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);*/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*
        //Debug.Log ("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (placeholder != null)
            Destroy(placeholder);*/
    }
    public void OnPointerEnter(PointerEventData eventData)
    {/*
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        /*
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }*/
    }

    public void OnDrop(PointerEventData eventData)
    {
        /*
        //Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        //We can add to check if player wants to drop the item to the world
        //Or check if the item can be set in this other type of drop zone
        //or to add something to a equipment window and check for the type
        //dragable d = evventdata.pointerdrag.getComponent<Dragable> 
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }*/

    }


}
