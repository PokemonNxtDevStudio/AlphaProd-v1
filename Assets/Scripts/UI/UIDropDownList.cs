using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum UIDropDownListType{ 

    FriendList
}
/// <summary>
/// Currently Only handles vertical lists.
/// </summary>
public class UIDropDownList : MonoBehaviour{

	// Use this for initialization
    /// <summary>
    /// Width of item
    /// </summary>
    public int width;
    /// <summary>
    /// Height of item
    /// </summary>
    public int height;
    public  List<UIDropDownSlot> dropDownList = new List< UIDropDownSlot>();
    public UIGrid grid;
    public GameObject itemPrefab;
    private UIScrollView scrollView;

    void Awake()
    {
        grid.cellWidth = width;
        grid.cellHeight = height;
        scrollView = GetComponent<UIScrollView>();
    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddItemOnClick()
    {
        GameObject go = (GameObject)Instantiate(itemPrefab);
        dropDownList.Add(go.GetComponent<UIDropDownSlot>());
        go.transform.parent = grid.gameObject.transform;
        go.transform.localPosition = new Vector3(0, -height * (dropDownList.Count - 1), 0);
        go.transform.localScale =new Vector3(1, 1, 1);
        scrollView.UpdateScrollbars(true);
        
    }
    public void AddItem( Object Data)
    {
        GameObject go = (GameObject)Instantiate(itemPrefab);
        go.transform.parent = grid.gameObject.transform;
        //go.transform.position = 
       // go.GetComponent<UISprite>().
        //ddl.Add(ddl.Count, item);
    }
    public void AddItem(GameObject go,Object Data)
    {

        //ddl.Add(ddl.Count, item);
    }
}
