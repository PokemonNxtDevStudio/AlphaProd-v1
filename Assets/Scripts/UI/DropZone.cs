using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null)
        {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) 
    {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) 
        {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) 
    {
		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        
        //We can add to check if player wants to drop the item to the world
        //Or check if the item can be set in this other type of drop zone
        //or to add something to a equipment window and check for the type
        //dragable d = evventdata.pointerdrag.getComponent<Dragable> 
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null)
        {
			d.parentToReturnTo = this.transform;
		}

	}
}
