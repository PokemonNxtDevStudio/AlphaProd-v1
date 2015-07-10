﻿using UnityEngine;
using System.Collections;

public class TextureAnimator : MonoBehaviour 
{

    public int _uvTieX = 1;
    public int _uvTieY = 1;
    public int _fps = 10;

    private Vector2 _size;
    private Renderer _myRenderer;
    private int _lastIndex = -1;

    private bool canPlay = false;
    private int atm = 0;
    private int total;

    void Start()
    {
        total = _uvTieX * _uvTieY;
        _size = new Vector2(1.0f / _uvTieX, 1.0f / _uvTieY);
        _myRenderer = GetComponent<Renderer>();
        if (_myRenderer == null)
            enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonUp(0))
        {
            canPlay = true;
        }
        if(canPlay)
        {*/
            ChangeTexture();
        //}
    }

    private void ChangeTexture()
    {
        if(atm == total)
        {
            canPlay = false;
            atm = 0;
            return;
        }
        else
        {
            
            // Calculate index
            int index = (int)(Time.timeSinceLevelLoad * _fps) % (_uvTieX * _uvTieY);
            if (index != _lastIndex)
            {
                // split into horizontal and vertical index
                int uIndex = index % _uvTieX;
                int vIndex = index / _uvTieY;

                // build offset
                // v coordinate is the bottom of the image in opengl so we need to invert.
                Vector2 offset = new Vector2(uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);

                _myRenderer.material.SetTextureOffset("_MainTex", offset);
                _myRenderer.material.SetTextureScale("_MainTex", _size);

                _lastIndex = index;
                atm++;

            }
        }        
        
    }
}