using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class ModularBuilding : EditorWindow
{
    


    private bool _horizontal;
    private bool _vertical;
    private Rect _hButton = new Rect(0, 45, 150, 30);
    private Rect _vButton = new Rect(150, 45, 150, 30);
    private Rect _baseLayble = new Rect(0, 80, 300, 20);
    private Rect _nameforHouseLable = new Rect(0, 41, 300, 20);
    private Rect _nameForTheHouse = new Rect(0, 60, 300, 20);
    private Rect _nextBase = new Rect(150, 100, 150, 30);
    private Rect _previousBase = new Rect(0, 100, 150, 30);

    private Rect _RoofLable = new Rect(0, 200, 300, 20);
    private Rect _nextRoof = new Rect(150, 220, 150, 30);
    private Rect _previousRoof = new Rect(0, 220, 150, 30);

    private Rect _F2Bool = new Rect(0, 130, 300, 20);
    private Rect _F2Lable = new Rect(0, 150, 300, 20);
    private Rect _nextF2 = new Rect(150, 170, 150, 30);
    private Rect _previousF2 = new Rect(0, 170, 150, 30);

    private Rect _NMBW = new Rect(0, 0,200, 40);
    private Rect _OkB = new Rect(200, 0, 100, 40);

    private Vector3 _HPosition = Vector3.zero;
    private bool _new = false;
    private bool _hbuttonBool = false;
    private bool _vbuttonBool = false;

    private int minVerBases = 1;
    private int maxVerBases = 28;
    private int curVerBase = 1;
    private string houseName = "";
    private string addHouseName = "Name of the House";
    private string curBaseString  = "Current Base: 1 / 28"  ;
    private string houseBasePath = "Prefabs/Environment/Buildings/ModularHouses/Base/Base_H_0";
    private string houseF2Path = "Prefabs/Environment/Buildings/ModularHouses/F2/F2_H_0";
    private string houseRoofPath = "Prefabs/Environment/Buildings/ModularHouses/Roof/Roof_H_0";

    private int minVerRoofs = 1;
    private int maxVerRoofs= 15;
    private int curVerRoof = 1;
    private string curRoofString = "Current Base: 1 / 15";
    
    private int minVerF2s = 1;
    private int maxVerF2s = 7;
    private int curVerF2 = 1;
    private string curF2String = "Current F2: 1 / 7";
    private bool _addF2 = false;

    private GameObject _HouseBase;
    private GameObject _HouseF2;
    private GameObject _HouseRoof;

    private float _3thFloorLow = 10.5f;
    private float _3thFloorHeigt = 13.15f;
    private float _2thFloorHeighLow = 4.8f;
    private float _2thFloorHeighHigh = 7f;
    private float _F2Height = 5.75f;


    private List<int> lows = new List<int> { 2, 3, 5, 7, 9, 11, 13, 15 };

    [MenuItem("NXT/Modular Building")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ModularBuilding));
    }
    void OnGUI()
    {

        #region New House
        if (GUI.Button(_NMBW," New House "))
        {
            _new = true;
            _hbuttonBool = false;
            _vbuttonBool = false;
            if(_HouseBase != null)
            {
                DestroyImmediate(_HouseBase);
            }
            if(_HouseF2 != null)
            {
                DestroyImmediate(_HouseF2);
            }
            if(_HouseRoof != null)
            {
                DestroyImmediate(_HouseRoof);
            }
            _HouseBase = null;
            _HouseF2 = null;
            _HouseRoof = null;
            curVerBase = minVerBases;
            curBaseString = "Current Base " + curVerBase + " / " + maxVerBases;
            houseName = "";
            curVerRoof = minVerRoofs;
            curRoofString = "Current Roof :" + curVerRoof + " / " + maxVerRoofs;
            _addF2 = false;
            curVerF2 = minVerF2s;
            curF2String = "Current F2 :" + curVerF2 + " / " + maxVerF2s;
        }
        #endregion

        #region Confirm
        if (GUI.Button(_OkB,"Confirm") && _HouseBase != null && _HouseRoof != null)
        {
            
            curVerRoof = minVerRoofs;
            curVerBase = minVerBases;
            curVerF2 = minVerF2s;
            _addF2 = false;
            _new = false;
            _vbuttonBool = false;
            if(houseName != "")
            {
                _HouseBase.name = houseName;
                _HouseRoof.name = houseName + "(Roof)";
                if (_HouseF2 != null)
                    _HouseF2.name = houseName + "(F2)";
            }
            else
            {
                _HouseBase.name = "Modular House";
                _HouseRoof.name = "Modular House" + "(Roof)";
                if (_HouseF2 != null)
                    _HouseF2.name = "Modular House" + "(F2)";
            }
            _HouseBase = null;
            _HouseF2 = null;
            _HouseRoof = null;

        }
        #endregion

        #region Orientation buttons
        if (_new == true)
        {             
            if(_hbuttonBool == false && _vbuttonBool == false)
            {
                if (GUI.Button(_hButton, "Horizontal"))
                {
                    _hbuttonBool = true;
                }
                if (GUI.Button(_vButton, "Vertical"))
                {
                    _vbuttonBool = true;
                    CreateNewVerticalHouse();
                }
            }
        #endregion

            #region Vertical Buttons
            else if (_vbuttonBool == true)
            {
                GUI.Label(_baseLayble, "Select Base" + "   " + curBaseString);
                GUI.Label(_nameforHouseLable, addHouseName);
                houseName = GUI.TextField(_nameForTheHouse, houseName,20);
                GUI.Label(_RoofLable, "Select Roof" + "   " + curRoofString);

                

                _addF2 = GUI.Toggle(_F2Bool, _addF2, "Add a F2");

            #endregion

                #region F2 Part
                if (_addF2 == true)
                {
                   
                    GUI.Label(_F2Lable, "Select F2" + "   " + curF2String);
                    if(_HouseF2 == null)
                    {
                        MakeVerticalF2(curVerF2);
                        if (lows.Contains(curVerRoof))
                        {
                            _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _3thFloorLow, _HouseBase.transform.position.z);

                        }
                        else
                            _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _3thFloorHeigt, _HouseBase.transform.position.z);

                        //Debug.Log("F2 was Created and the roof was moved");
                    }
                    
                    if(GUI.Button(_previousF2,"<--"))
                    { 
                        if(curVerF2 == minVerF2s)
                        {
                            curVerF2 = maxVerF2s;
                        }
                        else
                        {
                            curVerF2--;
                        }
                        curF2String = "Current F2 " + curVerF2 + " / " + maxVerF2s;
                        MakeVerticalF2(curVerF2);
                    }
                    if(GUI.Button(_nextF2,"-->"))
                    {
                        if (curVerF2 == maxVerF2s)
                        {
                            curVerF2 = minVerF2s;
                        }
                        else
                        {
                            curVerF2++;
                        }
                        curF2String = "Current F2 " + curVerF2 + " / " + maxVerF2s;
                        MakeVerticalF2(curVerF2);
                    }
                }
                else
                {
                   if(_HouseF2 != null)
                   {
                       DestroyImmediate(_HouseF2);
                       
                       if (lows.Contains(curVerRoof))
                       {
                           _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _2thFloorHeighLow, _HouseBase.transform.position.z);

                       }
                       else
                           _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _2thFloorHeighHigh, _HouseBase.transform.position.z);

                     //  Debug.Log("F2 was destroy and the roof was moved");
                   }
                }
                #endregion

                #region Base Part
                if (GUI.Button(_previousBase,"<--"))
                {
                    if(curVerBase == minVerBases)
                    {
                        curVerBase = maxVerBases;
                    }
                    else
                    {
                        curVerBase--;
                    }
                    curBaseString = "Current Base " + curVerBase + " / " + maxVerBases;
                    MakeVerticalBase(curVerBase);
                    
                }
                if(GUI.Button(_nextBase,"-->"))
                {
                    if (curVerBase == maxVerBases)
                    {
                        curVerBase = minVerBases;
                    }
                    else
                    {
                        curVerBase++;
                    }
                    curBaseString = "Current Base " + curVerBase + " / " + maxVerBases;
                    MakeVerticalBase(curVerBase);
                }
                #endregion

                #region Roof Part
                if (GUI.Button(_previousRoof , " <--"))
                {
                    if(curVerRoof == minVerRoofs)
                    {
                        curVerRoof = maxVerRoofs;
                    }
                    else
                    {
                        curVerRoof--;
                    }
                    curRoofString = "Current Roof :" + curVerRoof + " / " + maxVerRoofs;
                    MakeVerticalRoof(curVerRoof);
                    
                }
                if (GUI.Button(_nextRoof, "-->"))
                {
                    if (curVerRoof == maxVerRoofs)
                    {
                        curVerRoof = minVerRoofs;
                    }
                    else
                    {
                        curVerRoof++;
                    }
                    curRoofString = "Current Roof :" + curVerRoof + " / " + maxVerRoofs;
                    MakeVerticalRoof(curVerRoof);
                }
                #endregion


            }
            else if(_hbuttonBool == true)
            {
                GUI.Label(_baseLayble, "No Horizontal Houses ATM");
            }
            
            
        }

        
      
       

    }

    private void MakeVerticalBase(int index)
    {
        if (_HouseBase != null)
        {
            if(_HouseRoof != null)
            {
                _HouseRoof.transform.parent = null;
            }
            if(_HouseF2 != null)
            {
                _HouseF2.transform.parent = null;
            }
            _HPosition = _HouseBase.transform.position;
            DestroyImmediate(_HouseBase);
            _HouseBase = null;
            GameObject _newHouse = (GameObject)Instantiate(Resources.Load(houseBasePath + index));
            
            _newHouse.transform.position = _HPosition;
            

            _HouseBase = _newHouse;

            if(_HouseRoof != null)
            {
                _HouseRoof.transform.parent = _HouseBase.transform;
            }
            if(_HouseF2 != null)
            {
                _HouseF2.transform.parent = _HouseBase.transform;
            }

        }
        
    }

    private void MakeVerticalRoof(int index)
    {
        if(_HouseRoof != null)
        {
            DestroyImmediate(_HouseRoof);
        }
        GameObject _newRoof = (GameObject)Instantiate(Resources.Load(houseRoofPath + index));
        
        _newRoof.transform.parent = _HouseBase.transform;

        _HouseRoof = _newRoof;
        if(_addF2 == false)
        {
            DestroyImmediate(_HouseF2);
        }
        if(_HouseF2 == null)
        {
            if(lows.Contains (index))
            {
                _newRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _2thFloorHeighLow, _HouseBase.transform.position.z);
        
            }
            else
            _newRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _2thFloorHeighHigh, _HouseBase.transform.position.z);
        }
        else
        {
            if (lows.Contains(index))
            {
                _newRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _3thFloorLow, _HouseBase.transform.position.z);

            }
            else
            _newRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _3thFloorHeigt, _HouseBase.transform.position.z);
     
        }

    }

    private void MakeVerticalF2(int index)
    {
        if(_HouseF2 != null)
        {
            DestroyImmediate(_HouseF2);
        }
        GameObject _newF2 = (GameObject)Instantiate(Resources.Load(houseF2Path + index));
        _newF2.transform.parent = _HouseBase.transform;
        _HouseF2 = _newF2;
       
        /*if(lows.Contains(curVerRoof))
        {
            _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x,_HouseBase.transform.position.y + _3thFloorLow,_HouseBase.transform.position.z);
        }
        else
        {
            _HouseRoof.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _3thFloorHeigt, _HouseBase.transform.position.z);
        }
         * */
        _newF2.transform.position = new Vector3(_HouseBase.transform.position.x, _HouseBase.transform.position.y + _F2Height, _HouseBase.transform.position.z);
    }
    private void CreateNewVerticalHouse()
    {       
        Camera sceneCamera = SceneView.currentDrawingSceneView.camera;
        Vector3 pos = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,30f));
        GameObject houseBase = (GameObject)Instantiate(Resources.Load(houseBasePath + minVerBases));
        _HPosition = pos;
        houseBase.transform.position = _HPosition;
        sceneCamera.transform.rotation = Quaternion.identity;
        _HouseBase = houseBase;

        GameObject houseRoof = (GameObject)Instantiate(Resources.Load(houseRoofPath + minVerRoofs));
        houseRoof.transform.position = new Vector3(houseBase.transform.position.x, houseBase.transform.position.y + _2thFloorHeighHigh, houseBase.transform.position.z);
        houseRoof.transform.parent = houseBase.transform;
        _HouseRoof = houseRoof;
    }
   

    private void MakeHorizontalBase(int index)
    {

    }
    private void CreateNewHorizontalHouse()
    {

    }
}
