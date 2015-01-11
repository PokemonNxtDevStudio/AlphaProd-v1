using UnityEngine;
using System.Collections;
/// <summary>
///  UI button class to provide functions not provided by NGUI
///  TODO: Extend from UI Button
///  TODO: REview ON mouse enter etc. Currently Broken.
/// </summary>
public class UIButtonExtended : MonoBehaviour{


    public UISprite OnHoverSprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseEnter()
    {
        OnHoverSprite.enabled = true;
    }
    void OnMouseExit()
    {
        OnHoverSprite.enabled = false;
    }
}
