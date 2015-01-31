using UnityEngine;
using UnityEditor;


public class ModularBuilding : EditorWindow
{
    private int _CurvbaseDesign = 1;
    private int _vbaseDesignMin = 1;
    private int _vbaseDesignMax = 4;
    private int _vbaseTextureMinVar = 1;
    private int _vbaseTextureMaxVar = 10;
    private int _CurvbaseTexture = 1;

    private int _Curvf2Design = 1;
    private int _vf2DesignMin = 1;
    private int _vf2DesignMax = 1;
    private int _vf2TextureMinVar = 1;
    private int _vf2TextureMaxVar = 10;
    private int _Curvf2Texture = 1;

    private int _CurvroofDesign = 1;
    private int _vroofDesignMin = 1;
    private int _vroofDesignMax = 3;
    private int _vroofTextureMinVar = 1;
    private int _vroofTextureMaxVar = 12;
    private int _CurvroofTexture = 1;
    private int _CurvroofWallTexture = 1;
    private int _vroofWallTextureMin = 1;
    private int _vroofWallTextureMax = 10;


    private Rect _hButton = new Rect(0, 45, 150, 30);
    private Rect _vButton = new Rect(150, 45, 150, 30);
    private Rect _baseLayble = new Rect(0, 80, 300, 20);
    private Rect _nameforHouseLable = new Rect(0, 41, 300, 20);
    private Rect _nameForTheHouse = new Rect(0, 60, 300, 20);
   
    //Base Buttons Rects
    private Rect _previousBaseDesign = new Rect(0, 100, 75, 30);
    private Rect _nextBaseDesign = new Rect(75, 100, 75, 30);
    
    private Rect _previousBaseTexture = new Rect(160, 100, 75, 30);
    private Rect _nextBaseTexture = new Rect(235, 100, 75, 30);
    
    /*·································································*/

    //Roof Lable Rect
    private Rect _RoofLable = new Rect(0, 200, 300, 20);

    //Roof Buttons Rects
    private Rect _previousRoofDesign = new Rect(0, 220, 150, 30);
    private Rect _nextRoofDesign = new Rect(150, 220, 150, 30);


    private Rect _previousRoofWallTexure = new Rect(0, 250, 75, 30);
    private Rect _nextRoofWallTexture = new Rect(75, 250, 75, 30);

    private Rect _previousRoofTexture = new Rect(160, 250, 75, 30);
    private Rect _nextRoofTexture = new Rect(235, 250, 75, 30);
    /* ····································································*/

    private Rect _F2Bool = new Rect(0, 130, 300, 20);
    private Rect _F2Lable = new Rect(0, 150, 300, 20);

    private Rect _previousF2 = new Rect(0, 170, 150, 30);
    private Rect _nextF2 = new Rect(150, 170, 150, 30);
    

    private Rect _NMBW = new Rect(0, 0,200, 40);
    private Rect _OkB = new Rect(200, 0, 100, 40);

    private Vector3 _HPosition = Vector3.zero;
   
    private bool _hbuttonBool = false;
    private bool _vbuttonBool = false;

    
    private string houseName = "";
    private string addHouseName = "Name of the House";
    private string curVBaseString  = "Base Design: 1 / 4" + "   Texture 1 / 10" ;
   
    //Folders Path Strings
    private string houseVBasePath = "Prefabs/Environment/Buildings/ModularHouses/Base/BaseDesign_";
    private string houseVF2Path = "Prefabs/Environment/Buildings/ModularHouses/F2/F2Design_";
    private string houseVRoofPath = "Prefabs/Environment/Buildings/ModularHouses/Roof/RoofDesign_";

    /*------------------------------------------------------------------------------------------------*/

   
    private string curVRoofString = "Roof Design: 1 / 3" + "  Walls: 1 / 10" + "   Roof: 1/12";
    
    
    private string curVF2String = "F2 Design: 1 / 1" + "   Texture: 1 / 10";
    private bool _addVF2 = false;

    private GameObject _VHouseBase;
    private GameObject _VHouseF2;
    private GameObject _VHouseRoof;



    private float _2thRoofD1Heigh = 7.7f;
    private float _2thRoofD2Heigh = 0F;
    private float _2thRoofD3Heigh = 7.8F;

    private float _3thRoofD1Heigh = 13.3f;
    private float _3thRoofD2Heigh = 5.56F;
    private float _3thRoofD3Heigh = 13.43F;

    private float _F2D1Height = 0.2f;


    

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
            ClearAndMakeNew();
        }
        #endregion

        #region Confirm
        if (GUI.Button(_OkB,"Confirm") && _VHouseBase != null && _VHouseRoof != null)
        {          
            if(houseName != "")
            {
                _VHouseBase.name = houseName;
                _VHouseRoof.name = houseName + "(Roof)";
                if (_VHouseF2 != null)
                    _VHouseF2.name = houseName + "(F2)";
            }
            else
            {
                _VHouseBase.name = "Modular House";
                _VHouseRoof.name = "Modular House" + "(Roof)";
                if (_VHouseF2 != null)
                    _VHouseF2.name = "Modular House" + "(F2)";
            }

            ClearData();           

        }
        #endregion

        #region Orientation buttons
                    
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
            GUI.Label(_baseLayble,curVBaseString);
            GUI.Label(_nameforHouseLable, addHouseName);
            houseName = GUI.TextField(_nameForTheHouse, houseName,20);
            GUI.Label(_RoofLable,curVRoofString);

                

            _addVF2 = GUI.Toggle(_F2Bool, _addVF2, "Add a F2");

        #endregion

            #region F2 Part
            if (_addVF2 == true)
            {
                   
                GUI.Label(_F2Lable, "Select F2" + "   " + curVF2String);
                if(_VHouseF2 == null)
                {
                    MakeNewVF2();
                }
                    
                if(GUI.Button(_previousF2,"<--"))
                {
                    if (_Curvf2Texture == _vf2TextureMinVar)
                        _Curvf2Texture = _vf2TextureMaxVar;
                    else
                        _Curvf2Texture--;
                    
                    LablesUpdate();
                    MakeNewVF2();
                }
                if(GUI.Button(_nextF2,"-->"))
                {
                    if (_Curvf2Texture == _vf2TextureMaxVar)
                        _Curvf2Texture = _vf2TextureMinVar;
                    else
                        _Curvf2Texture++;
                    
                    LablesUpdate();
                    MakeNewVF2();
                }
            }
            else
            {
                if(_VHouseF2 != null)
                {
                    DestroyImmediate(_VHouseF2);
                    MakeNewVRoof();

                }
            }
            #endregion

            #region BaseDesign Part
            if (GUI.Button(_previousBaseDesign,"<--"))
            {
                if (_CurvbaseDesign == _vbaseDesignMin)
                    _CurvbaseDesign = _vbaseDesignMax;
                else
                    _CurvbaseDesign--;
                LablesUpdate();

                MakeNewVBase();
                    
            }
            if(GUI.Button(_nextBaseDesign,"-->"))
            {
                if (_CurvbaseDesign == _vbaseDesignMax)                    
                    _CurvbaseDesign = _vbaseDesignMin;
                
                else                
                    _CurvbaseDesign++;

                LablesUpdate();
                MakeNewVBase();                
            }
            #endregion
            #region BaseTexture Part
            if(GUI.Button(_previousBaseTexture,"<--"))
            {
                if (_CurvbaseTexture == _vbaseTextureMinVar)
                    _CurvbaseTexture = _vbaseTextureMaxVar;
                else
                    _CurvbaseTexture--;
                LablesUpdate();
                MakeNewVBase();
            }
            if(GUI.Button(_nextBaseTexture,"-->"))
            {
                if (_CurvbaseTexture == _vbaseTextureMaxVar)
                    _CurvbaseTexture = _vbaseTextureMinVar;
                else
                    _CurvbaseTexture++;

                LablesUpdate();
                MakeNewVBase();
                
            }
            #endregion

            #region RoofDesign Part
            if (GUI.Button(_previousRoofDesign , " <--"))
            {
                
                if(_CurvroofDesign == _vroofDesignMin)                
                    _CurvroofDesign = _vroofDesignMax;
                
                else                
                    _CurvroofDesign--;

                LablesUpdate();
                MakeNewVRoof();
                  
                    
            }
            if (GUI.Button(_nextRoofDesign, "-->"))
            {

                if (_CurvroofDesign == _vroofDesignMax)                
                    _CurvroofDesign = _vroofDesignMin;
                
                else                
                    _CurvroofDesign++;
                

                LablesUpdate();
                MakeNewVRoof();
                    
            }
            #endregion

            #region RoofWalls Part

            if(_CurvroofDesign != 1)
            {
                if(GUI.Button(_previousRoofWallTexure,"<--"))
                {
                    if (_CurvroofWallTexture == _vroofWallTextureMin)
                        _CurvroofWallTexture = _vroofWallTextureMax;
                    else
                        _CurvroofWallTexture--;

                    LablesUpdate();
                    MakeNewVRoof();
                }

                if(GUI.Button(_nextRoofWallTexture,"-->"))
                {
                    if (_CurvroofWallTexture == _vroofWallTextureMax)
                        _CurvroofWallTexture = _vroofWallTextureMin;
                    else
                        _CurvroofWallTexture++;

                    LablesUpdate();
                    MakeNewVRoof();
                }
            }
            #endregion

            #region Roof Textures
            
            if(GUI.Button(_previousRoofTexture,"<--"))
            {
                if (_CurvroofTexture == _vroofTextureMinVar)
                    _CurvroofTexture = _vroofTextureMaxVar;
                else
                    _CurvroofTexture--;

                LablesUpdate();
                MakeNewVRoof();
            }

            if(GUI.Button(_nextRoofTexture,"-->"))
            {
                if (_CurvroofTexture == _vroofTextureMaxVar)
                    _CurvroofTexture = _vroofTextureMinVar;
                else
                    _CurvroofTexture++;

                LablesUpdate();
                MakeNewVRoof();
            }
            

            #endregion


        }
        else if(_hbuttonBool == true)
        {
            GUI.Label(_baseLayble, "No Horizontal Houses ATM");
        }   
    }


    /// <BaseCreation>
    /// Updated January/20/2015
    /// Make it just need to nkow whats the current texture and current Design of the base
    /// </BaseCreation>
    private void MakeNewVBase()
    {

        if (_VHouseBase != null)
        {
            //if there is a roof unparent it from the current base 
            if (_VHouseRoof != null)            
                _VHouseRoof.transform.parent = null;
            
            //if there is a F2 unparent it from the current base 
            if (_VHouseF2 != null)            
                _VHouseF2.transform.parent = null;
            
            //Set the old positon of the old Base for the new base to use wene created
            _HPosition = _VHouseBase.transform.position;
            
            //Destroy the current base
            DestroyImmediate(_VHouseBase);            
            _VHouseBase = null;

            //Create the new base
            GameObject _newHouse = (GameObject)Instantiate(Resources.Load(houseVBasePath + _CurvbaseDesign + "/"+ "Base_V_"+ _CurvbaseDesign+ "_" + _CurvbaseTexture));
          
            //Set the new base to the old base position
            _newHouse.transform.position = _HPosition;

            //Set the new base to the base GameObject variable
            _VHouseBase = _newHouse;

            //Set the base as the parent of the roof if exist
            if (_VHouseRoof != null)            
                _VHouseRoof.transform.parent = _VHouseBase.transform;
            
            //Set the base as the parent of the F2 if exist
            if (_VHouseF2 != null)            
                _VHouseF2.transform.parent = _VHouseBase.transform;            

        }
    }

    /// <RoofCreation>
    /// Updated January/20/2015
    /// Make it just need to nkow whats the current texture and current Design of the Roof
    /// </RoofCreation>
    private void MakeNewVRoof()
    {
        //Check if ther is an existing Roof if there is it will destroy it
        if (_VHouseRoof != null)
        {
            DestroyImmediate(_VHouseRoof);
        }
        
        //Create the new roof
        if(_CurvroofDesign == 1)
        {  
            //if its the Design 1 that is the only one that dont have Walls
            GameObject _newRoof = (GameObject)Instantiate(Resources.Load(houseVRoofPath + _CurvroofDesign + "/" + "Roof_V_"+ _CurvroofDesign + "_" + _CurvroofTexture));
     
            //Place the new roof on under the current Base
            _newRoof.transform.parent = _VHouseBase.transform;
            //Set the GameObject to the new roof
            _VHouseRoof = _newRoof;

          
            //Check if there is not a F2 Active
            if(_VHouseF2 == null)
            {
                if(_CurvroofTexture == 11 || _CurvroofTexture == 12)
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y, _VHouseBase.transform.position.z);
                else               
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x,_VHouseBase.transform.position.y + _2thRoofD1Heigh,_VHouseBase.transform.position.z);
                
                _newRoof.transform.rotation = _VHouseBase.transform.rotation;
            }
            else
            {
                if(_CurvroofTexture == 11 || _CurvroofTexture == 12)
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD2Heigh , _VHouseBase.transform.position.z);
               else
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x,_VHouseBase.transform.position.y + _3thRoofD1Heigh,_VHouseBase.transform.position.z);
                

                _newRoof.transform.rotation = _VHouseBase.transform.rotation;
            }
            /*------------------------------------------------------------*/
        }
        else
        {
            //if the roof to create is one that has walls
            GameObject _newRoof = (GameObject)Instantiate(Resources.Load(houseVRoofPath + _CurvroofDesign + "/" + "Roof_V_"+ _CurvroofDesign + "_" + _CurvroofWallTexture + "_"+_CurvroofTexture));
       
            //Place the new roof on under the current Base
            _newRoof.transform.parent = _VHouseBase.transform;
             //Set the GameObject to the new roof
            _VHouseRoof = _newRoof;

            //Check if there is not a F2 Active
            if(_VHouseF2 == null)
            {
                if (_CurvroofDesign == 2)                
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x,_VHouseBase.transform.position.y + _2thRoofD2Heigh,_VHouseBase.transform.position.z);
                if (_CurvroofDesign == 3)
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _2thRoofD3Heigh, _VHouseBase.transform.position.z);
               
                _newRoof.transform.rotation = _VHouseBase.transform.rotation;
            }
            else
            {
                if(_CurvroofDesign == 2)                
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x,_VHouseBase.transform.position.y + _3thRoofD2Heigh,_VHouseBase.transform.position.z);
                if(_CurvroofDesign == 3)
                    _newRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD3Heigh, _VHouseBase.transform.position.z);
                
                _newRoof.transform.rotation = _VHouseBase.transform.rotation;
            }
            /*------------------------------------------------------------*/

        }    

       

    }


    private void ClearAndMakeNew()
    {
        //Destroy if not confirm
        if (_VHouseBase != null)        
            DestroyImmediate(_VHouseBase);
        
        if (_VHouseF2 != null)        
            DestroyImmediate(_VHouseF2);
        
        if (_VHouseRoof != null)        
            DestroyImmediate(_VHouseRoof);
       /*-----------------------------------------------------------------*/
        //Clean the data
        ClearData();
        
       
    }
    private void ClearData()
    {
        //Display Options to create buttons
        _hbuttonBool = false;
        _vbuttonBool = false;
        /*----------------------------------------------------------------*/

        //Clear Vertical values
        _VHouseBase = null;
        _VHouseF2 = null;
        _VHouseRoof = null;

        _CurvbaseDesign = _vbaseDesignMin;
        _Curvf2Design = _vf2DesignMin;
        _CurvbaseTexture = _vbaseTextureMinVar;
        _Curvf2Texture = _vf2TextureMinVar;
        _CurvroofDesign = _vroofDesignMin;
        _CurvroofTexture = _vroofTextureMinVar;
        _CurvroofWallTexture = _vroofWallTextureMin;

                
        houseName = "";
        LablesUpdate();
        _addVF2 = false; 
        /*--------------------------------------------------------------------*/
    }

    private void LablesUpdate()
    {
        curVBaseString = "Base Design: " + _CurvbaseDesign + " / " + _vbaseDesignMax + "   Texture " + _CurvbaseTexture + " / " + _vbaseTextureMaxVar;
       
        curVF2String = "F2 Design: "+ _vf2DesignMin + " / " + _vf2DesignMax + "   Texture: " + _Curvf2Texture + " / " + _vf2TextureMaxVar;

        if(_CurvroofDesign == 1)
        curVRoofString = "Roof Design:" + _CurvroofDesign + " / " + _vroofDesignMax + " Roof: " + _CurvroofTexture + " / " + _vroofTextureMaxVar;
        else
            curVRoofString = "Roof Design:" + _CurvroofDesign + " / " + _vroofDesignMax + " Walls: "  + _CurvroofWallTexture + " / " + _vroofWallTextureMax + " Roof: " + _CurvroofTexture + " / " + _vroofTextureMaxVar;


    }


    private void MakeNewVF2()
    {
        
        if(_VHouseF2 != null)
        {
            DestroyImmediate(_VHouseF2);
        }
        GameObject _newF2 = (GameObject)Instantiate(Resources.Load(houseVF2Path + _Curvf2Design + "/" + "F2_V_" + _Curvf2Design + "_"+_Curvf2Texture));
        _newF2.transform.parent = _VHouseBase.transform;
        _VHouseF2 = _newF2;

        if (_CurvroofDesign == 1)
        {
            if(_CurvroofTexture == 11 || _CurvroofTexture == 12)
                _VHouseRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD2Heigh, _VHouseBase.transform.position.z);
       
            else
                _VHouseRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD1Heigh, _VHouseBase.transform.position.z);
       
        }
            
        if(_CurvroofDesign == 2)
            _VHouseRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD2Heigh, _VHouseBase.transform.position.z);
       
        if(_CurvroofDesign == 3)
            _VHouseRoof.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _3thRoofD3Heigh, _VHouseBase.transform.position.z);



        _newF2.transform.position = new Vector3(_VHouseBase.transform.position.x, _VHouseBase.transform.position.y + _F2D1Height, _VHouseBase.transform.position.z);
   
        
        
    }
    
    
    private void CreateNewVerticalHouse()
    {       
        Camera sceneCamera = SceneView.currentDrawingSceneView.camera;
        Vector3 pos = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,30f));
        GameObject houseBase = (GameObject)Instantiate(Resources.Load(houseVBasePath + _CurvbaseDesign + "/" + "Base_V_"+ _CurvbaseDesign + "_"+_CurvbaseTexture));
        _HPosition = pos;
        houseBase.transform.position = _HPosition;
        sceneCamera.transform.rotation = Quaternion.identity;

        _VHouseBase = houseBase;

        GameObject houseRoof = (GameObject)Instantiate(Resources.Load(houseVRoofPath + _CurvroofDesign + "/" + "Roof_V_"+_CurvroofDesign +"_"+ _CurvbaseTexture));
        houseRoof.transform.position = new Vector3(houseBase.transform.position.x, houseBase.transform.position.y + _2thRoofD1Heigh, houseBase.transform.position.z);
        houseRoof.transform.parent = houseBase.transform;
        _VHouseRoof = houseRoof;
    }
   
}
