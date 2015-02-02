using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour 
{
   // [SerializeField]
    private Transform StartOfLaser;
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private float MovementSpeed = 0.1f;

    [SerializeField]
    private bool _callPoke = false;

    private LineRenderer _lightrender;
    private Vector3 _curPos;

    void Start ()
    {
        StartOfLaser = gameObject.transform;
        _lightrender = GetComponent<LineRenderer>();
        _lightrender.SetPosition(0, StartOfLaser.position);
        _curPos = transform.position;
        _lightrender.SetPosition(1, _curPos);
	}
	

	void Update () 
    {
        if(_callPoke == false)
        {
           // _lightrender.enabled = false;

            _curPos = transform.position;               
            _lightrender.SetPosition(1, _curPos);
        }
        else
        {
          //  _lightrender.enabled = true;
            if(_curPos.z < Target.position.z)
            {
                _curPos = Vector3.MoveTowards(_curPos, Target.position, MovementSpeed * Time.deltaTime);
                _lightrender.SetPosition(1, _curPos);
            }
            else
            {
                //_callPoke = false;
                
                _callPoke = false;
            }
        }
	
	}

    private void SetTarget(Transform target)
    {
        _curPos = StartOfLaser.position;
        _lightrender.SetPosition(1, _curPos);
        Target = target;
        _callPoke = true;
    }
}
