using UnityEngine;
using System.Collections;

public class AnimateTexture2 : MonoBehaviour {
    //vars for the whole sheet
    public int colCount = 4;
    public int rowCount = 4;

    //vars for animation
    public int rowNumber = 0; //Zero Indexed
    public int colNumber = 0; //Zero Indexed
    public int totalCells = 4;
    public int fps = 10;
    //Maybe this should be a private var
    private Vector2 offset;
    private Renderer rd;
    private bool canPlay = false;
    public float coold = 1f;

    public Transform user;
    public Transform targe;

    LineRenderer ld;

    void Start()
    {
        rd = GetComponent<Renderer>();
        ld = GetComponent<LineRenderer>();
        ld.enabled = false;
        
    }
    //Update
    void Update()
    { 
        if(Input.GetMouseButtonUp(0) && canPlay == false)
        {
            
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 dir = ray.direction.normalized;
            RaycastHit hit; 
            
            if (Physics.Raycast(ray ,out hit))
            {

                ld.SetPosition(1, hit.point);
               /* float sizeX = 1.0f / colCount;
                float sizeY = 1.0f / rowCount;
                Vector2 size = new Vector2(sizeX, sizeY);
                rd.material.SetTextureOffset("_MainTex", Vector2.zero);
                rd.material.SetTextureScale("_MainTex", size); */
                rd.material.mainTextureOffset = Vector2.zero;
                canPlay = true;
                ld.enabled = true;
                StartCoroutine("cooldown");
            }
            
        }
        if (canPlay)
        {
            SetSpriteAnimation(colCount, rowCount, rowNumber, colNumber, totalCells, fps);
        }
    }

    //SetSpriteAnimation
    void SetSpriteAnimation(int colCount, int rowCount, int rowNumber, int colNumber, int totalCells, int fps)
    {
        ld.SetPosition(0, user.transform.position);

       // Calculate index
            int index = (int)(Time.time * fps);
            // Repeat when exhausting all cells
            index = index % totalCells;

            // Size of every cell
            float sizeX = 1.0f / colCount;
            float sizeY = 1.0f / rowCount;
            Vector2 size = new Vector2(sizeX, sizeY);

            // split into horizontal and vertical index
            var uIndex = index % colCount;
            var vIndex = index / colCount;

            // build offset
            // v coordinate is the bottom of the image in opengl so we need to invert.
            float offsetX = (uIndex + colNumber) * size.x;
            float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
            Vector2 offset = new Vector2(offsetX, offsetY);

            rd.material.SetTextureOffset("_MainTex", offset);
            rd.material.SetTextureScale("_MainTex", size);      
    }
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(coold);
        canPlay = false;
        StopCoroutine("cooldown");
        ld.enabled = false;
    }


}
