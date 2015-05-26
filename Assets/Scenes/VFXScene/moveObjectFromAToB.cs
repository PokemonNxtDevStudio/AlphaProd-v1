using UnityEngine;
using System.Collections;

public class moveObjectFromAToB : MonoBehaviour 
{
    public Transform[] Points;
    public Transform PointB;
    private Transform mytransform;
    public float Speed = 10;
    Transform next;

	// Use this for initialization
	void Start ()
    {
        mytransform = gameObject.transform;
        next = Points[1];
	}
	
	// Update is called once per frame
	void Update () 
    {

        gameObject.transform.position = Vector3.MoveTowards(mytransform.position, next.position, Speed);
        if (mytransform.position == Points[1].position)
        {
            next = Points[2];
        }
        if (mytransform.position == Points[2].position)
        {
            next = Points[3];
        }
        if (mytransform.position == Points[3].position)
        {
            next = Points[0];
        }
        if (mytransform.position == Points[0].position)
        {
            next = Points[1];
        }
	
	}
}
