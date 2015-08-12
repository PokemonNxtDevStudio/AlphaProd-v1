using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairMonitor : MonoBehaviour {

	// Use this for initialization


    private Image crossHair;
    public LayerMask layerMask;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit; 
          var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

          if (Physics.Raycast(ray, out hit, layerMask))
          {
              Debug.Log("The layer is " + hit.transform.gameObject.layer);
          }
	}
}
