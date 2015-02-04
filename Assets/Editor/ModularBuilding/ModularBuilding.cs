using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class ModularBuilding : EditorWindow
{

    #region Houses vars
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
    private float _2thRoofD3Heigh = 1.6F;

    private float _3thRoofD1Heigh = 13.3f;
    private float _3thRoofD2Heigh = 5.56F;
    private float _3thRoofD3Heigh = 7.16F;

    private float _F2D1Height = 0.2f;


    #endregion

    #region Corner Buildings Region
    //Show Corner Button Bool
    private bool _cbuildingsButton = false;
    //Corner paths
    private string _curnerBuildingBasePath = "Prefabs/Environment/Buildings/ModularBuildings/Base/Base_";
    private string _curnerBuildignMidPath = "Prefabs/Environment/Buildings/ModularBuildings/Mid/Mid_";
    private string _curnerBuildignRoofPath = "Prefabs/Environment/Buildings/ModularBuildings/Roof/Roof_";
    private string _curnerBuildignF1DetailsPath = "Prefabs/Environment/Buildings/ModularBuildings/F1Details/f1Deco_";
    private string _curnerBuildignRoofDetailsPath = "Prefabs/Environment/Buildings/ModularBuildings/RoofDetails/RoofTop_";
    //Corner Min value
    private int _cBMin = 1;
    //Bases info and control
    private int _cBBaseDesignMax = 2;
    private int _cBBase1Textures = 5;
    //private int _cBBase2Textures = 1;
    private int _cBCurBaseDesign = 1;
    private int _cBCurBaseTexture = 1;
    //MidPart info and control    
    private int _cBMidDesingMax = 5;
    private int _cBMidTextures = 6;
    private int _cBCurMidAmount = 0;
    private int _cBCurMidDesign = 1;
    private int _cBCurMidTexture = 1;
    
   //Roof info and control
    private int _cBRoofDesignMax = 1;
    private int _cBRoofD1Textures = 3;
    private int _cBCurRoofDesign = 1;
    private int _cBCurRoofTexture = 1;
    //F1 Details info and control
    private int _cBF1DetailsMax = 2;
    private int _cBCurF1Details = 1;
    //Roof Details info and control
    private int _cBRoofDetailsMax = 2;
    private int _cBCurRoofDetails = 0;

    //Corner Building GameObjects
    private GameObject _CornerBase;
    private GameObject _CornerF1Detail;
    private GameObject _CornerRoof;
    private GameObject _CornerRoofDetail;
    private List<GameObject> _CMidParts = new List<GameObject>();

    //Corner Building Rects
    private Rect _cornerBuildingButtonRect = new Rect(0, 100, 300, 30);
   
    //Name Rect
    private Rect _CBNameLableRect = new Rect(0, 50, 300, 20);
    private Rect _CBNameStringRect = new Rect(0, 70, 300, 20);
    //Roof Rect
    private Rect _CBRoofLableRect = new Rect(0, 90, 300, 20);
    private Rect _CBPreviousRoofTextrureButtonRect = new Rect(150, 110, 80, 30);
    private Rect _CBNextRoofTextrureButtonRect = new Rect(230, 110, 80, 30);
    //Roof Details Rect
    private Rect _CBRoofDetailsLableRect = new Rect(0, 140, 300, 20);
    private Rect _CBPreviousRoofDetailButtonRect = new Rect(0, 160, 150, 30);
    private Rect _CBNextRoofDetailButtonRect = new Rect(150, 160, 150, 30);

    //Mids Rect
    private Rect _CBMidLableRect = new Rect(0, 200, 200, 20);
    private Rect _CBMidLessAmountRect = new Rect(200, 200, 50, 20);
    private Rect _CBMidMoreAmountRect = new Rect(250, 200, 50, 20);
    private Rect _CBPreviousMidDesignButtonRect = new Rect(0, 220, 75, 30);
    private Rect _CBNextMidDesignButtonRect = new Rect(75, 220, 75, 30);
    private Rect _CBPreviousMidTextureButtonRect = new Rect(160, 220, 75, 30);
    private Rect _CBNextMidTextureButtonRect = new Rect(235, 220, 75, 30);

    //f1 Details
    private Rect _CBF1DetailLable = new Rect(0, 250, 300, 20);
    private Rect _CBPreviousF1DetailRect = new Rect(0, 270, 150, 30);
    private Rect _CBNextF1DetailRect = new Rect(150, 270, 150, 30);

    //Base Rect
    private Rect _CBBaseLableRect = new Rect(0, 300, 300, 20);
    private Rect _CBPreviousBaseDesignRect = new Rect(0, 320, 80, 30);
    private Rect _CBNextBaseDesignRect = new Rect(80, 320, 80, 30);

    private Rect _CBPreviousBaseTextureRect = new Rect(170, 320, 80, 30);
    private Rect _CBNextBaseTextureRect = new Rect(250, 320, 80, 30);
    
    //Corner Building Heights

    private float cBRoofMin = -7.4f;
    private float CBMidPartsHeight = 7.4f;

    //Corner Building CurPosition 
    private Vector3 CBPostion;
    private Quaternion CBRotation = Quaternion.identity;

    #endregion


    [MenuItem("NXT/Modular Building")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ModularBuilding));
    }
    void OnGUI()
    {

        

        #region New House
        if (GUI.Button(_NMBW," New Building "))
        {
            ClearAndMakeNew();
        }
        #endregion

        #region Confirm
        if (GUI.Button(_OkB,"Confirm")/* && _VHouseBase != null && _VHouseRoof != null*/)
        {          
            if(houseName != "")
            {
                if(_VHouseBase != null)
                {
                    _VHouseBase.name = houseName;
                    _VHouseRoof.name = houseName + "(Roof)";
                    if (_VHouseF2 != null)
                        _VHouseF2.name = houseName + "(F2)";
                }
                if(_CornerBase != null)
                {
                    _CornerBase.name = houseName;
                   
                    if(_CornerRoofDetail != null)                        
                        _CornerF1Detail.name = houseName + "(F1Detail)";
                    MakeCornerMid();
                    _CornerRoof.name = houseName + "(Roof)";
                    if (_CornerRoofDetail != null)
                        _CornerRoofDetail.name = houseName + "(RoofDetail)";
                    
                }
                
            }
            else
            {
                if(_VHouseBase != null)
                {
                    _VHouseBase.name = "Modular House";
                    _VHouseRoof.name = "Modular House" + "(Roof)";
                    if (_VHouseF2 != null)
                        _VHouseF2.name = "Modular House" + "(F2)";
                }
                if (_CornerBase != null)
                {
                    _CornerBase.name = "Corner Building";

                    if (_CornerRoofDetail != null)
                        _CornerF1Detail.name = "Corner Building" + "(F1Detail)";
                    MakeCornerMid();
                    _CornerRoof.name = "Corner Building" + "(Roof)";
                    if (_CornerRoofDetail != null)
                        _CornerRoofDetail.name = "Corner Building" + "(RoofDetail)";
                }
                
            }

            ClearData();           

        }
        #endregion

        #region Orientation buttons

        if (_hbuttonBool == false && _vbuttonBool == false && _cbuildingsButton == false)
        {
            if (GUI.Button(_hButton, "Horizontal House"))
            {
                _hbuttonBool = true;
            }
            if (GUI.Button(_vButton, "Vertical House"))
            {
                _vbuttonBool = true;
                CreateNewVerticalHouse();
            }
            if(GUI.Button(_cornerBuildingButtonRect,"Corner Building"))
            {
                _cbuildingsButton = true;
                MakeBasicCornerBuilding();
            }
        }
   
        #endregion

                   
            #region Houses Region
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

        #endregion
             
        #region Corner Building Region
        if(_cbuildingsButton == true)
        {
            GUI.Label(_CBNameLableRect, "Name of the Building:");
            houseName =  GUI.TextField(_CBNameStringRect, houseName);
           #region Roof UI
           //Roof Lable
            GUI.Label(_CBRoofLableRect,"Roof :" + _cBCurRoofDesign + " / " + _cBRoofDesignMax + "                      Texture:" + _cBCurRoofTexture + " / " + _cBRoofD1Textures );
            
            //switch Roof textures
            if(GUI.Button(_CBPreviousRoofTextrureButtonRect,"<--"))
            {
                if (_cBCurRoofTexture == _cBMin)
                    _cBCurRoofTexture = _cBRoofD1Textures;
                else
                    _cBCurRoofTexture--;
                
                MakeCornerRoof();
            }
            if(GUI.Button(_CBNextRoofTextrureButtonRect,"-->"))
            {
                if (_cBCurRoofTexture == _cBRoofD1Textures)
                    _cBCurRoofTexture = _cBMin;
                else
                    _cBCurRoofTexture++;

                MakeCornerRoof();

            }
            #endregion
            #region RoofDetails UI

            GUI.Label(_CBRoofDetailsLableRect, "Roof Detail : " + _cBCurRoofDetails + " / " + _cBRoofDetailsMax);
            if(GUI.Button(_CBPreviousRoofDetailButtonRect,"<--"))
            {
                if (_cBCurRoofDetails == 0)
                    _cBCurRoofDetails = _cBRoofDetailsMax;
                else
                    _cBCurRoofDetails--;

                MakeCornerRoofDetail();
            }
            if(GUI.Button(_CBNextRoofDetailButtonRect,"-->"))
            {
                if (_cBCurRoofDetails == _cBRoofDetailsMax)
                    _cBCurRoofDetails = 0;
                else
                    _cBCurRoofDetails++;

                MakeCornerRoofDetail();
            }

            #endregion
            #region Mid UI

            if(_cBCurMidAmount == 0)
                GUI.Label(_CBMidLableRect, "Current Mid Parts = " + _cBCurMidAmount);
            else
            {
                GUI.Label(_CBMidLableRect, "Mid Design :" + _cBCurMidDesign + " / " + _cBMidDesingMax + "  Mid Parts : " + _cBCurMidAmount);
                //Mid Designs
                if(GUI.Button(_CBPreviousMidDesignButtonRect,"<--"))
                {
                    if (_cBCurMidDesign == _cBMin)
                        _cBCurMidDesign = _cBMidDesingMax;
                    else
                        _cBCurMidDesign--;

                    MakeCornerMid();
                }
                if(GUI.Button(_CBNextMidDesignButtonRect,"-->"))
                {
                    if (_cBCurMidDesign == _cBMidDesingMax)
                        _cBCurMidDesign = _cBMin;
                    else
                        _cBCurMidDesign++;

                    MakeCornerMid();
                }
                //Mid Textures
                if(GUI.Button(_CBPreviousMidTextureButtonRect,"<--"))
                {
                    if (_cBCurMidTexture == _cBMin)
                        _cBCurMidTexture = _cBMidTextures;
                    else
                        _cBCurMidTexture--;

                    MakeCornerMid();
                }
                if(GUI.Button(_CBNextMidTextureButtonRect,"-->"))
                {
                    if (_cBCurMidTexture == _cBMidTextures)
                        _cBCurMidTexture = _cBMin;
                    else
                        _cBCurMidTexture++;

                    MakeCornerMid();
                }
            }
                
            //Mid Amount
            if(GUI.Button(_CBMidLessAmountRect,"<--"))
            {
                if (_cBCurMidAmount == 0)
                    return;
                else
                    _cBCurMidAmount--;

                MakeCornerMid();
            }
            if(GUI.Button(_CBMidMoreAmountRect,"-->"))
            {
                if (_cBCurMidAmount > 15)
                    return;
                else
                    _cBCurMidAmount++;
                
                MakeCornerMid();
            }
           

            #endregion
            #region F1Details UI
            //F1 Details Label
            GUI.Label(_CBF1DetailLable, "F1 Detail : " + _cBCurF1Details + " / " + _cBF1DetailsMax);
            //F1 Details Buttons
            if(GUI.Button(_CBPreviousF1DetailRect,"<--"))
            {
                if (_cBCurF1Details == 0)
                    _cBCurF1Details = _cBF1DetailsMax;
                else
                    _cBCurF1Details--;

                MakeCornerF1Detail();
               
            }
            if(GUI.Button(_CBNextF1DetailRect,"-->"))
            {
                if (_cBCurF1Details == _cBF1DetailsMax)
                    _cBCurF1Details = 0;
                else
                    _cBCurF1Details++;
                MakeCornerF1Detail();
            }
            #endregion
            #region Base UI
            //Base Design Lable
            if(_cBCurBaseDesign == 1)
            {
                GUI.Label(_CBBaseLableRect, "Base :" + _cBCurBaseDesign + " / " + _cBBaseDesignMax + "     Texture : " + _cBCurBaseTexture + " / " + _cBBase1Textures); 
             
                if(GUI.Button(_CBPreviousBaseTextureRect,"<--"))
                {
                    if (_cBCurBaseTexture == _cBMin)
                        _cBCurBaseTexture = _cBBase1Textures;
                    else
                        _cBCurBaseTexture--;
                    MakeCornerBase();
                }
                if(GUI.Button(_CBNextBaseTextureRect,"-->"))
                {
                    if (_cBCurBaseTexture == _cBBase1Textures)
                        _cBCurBaseTexture = _cBMin;
                    else
                        _cBCurBaseTexture++;
                    MakeCornerBase();
                }
            }
                        
            if(_cBCurBaseDesign == 2)
            {
                GUI.Label(_CBBaseLableRect, "Base :" + _cBCurBaseDesign + " / " + _cBBaseDesignMax + "     Texture : " + _cBMin + " / " + _cBMin);             
            }
            //Base Design Buttons
            if (GUI.Button(_CBPreviousBaseDesignRect, "<--"))
            {
                if (_cBCurBaseDesign == _cBMin)
                    _cBCurBaseDesign = _cBBaseDesignMax;
                else
                    _cBCurBaseDesign--;

                MakeCornerBase();
            }
            if (GUI.Button(_CBNextBaseDesignRect, "-->"))
            {
                if (_cBCurBaseDesign == _cBBaseDesignMax)
                    _cBCurBaseDesign = _cBMin;
                else
                    _cBCurBaseDesign++;

                MakeCornerBase();
            }
            #endregion
        }
        #endregion

        #region Fences

        #endregion
    }

    #region Vertical Houses Creation Part

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
   


    #endregion

    #region Corner Building Creation Part

    private void MakeBasicCornerBuilding()
    {
        MakeCornerBase();
        MakeCornerRoof();
        MakeCornerF1Detail();
        Camera sceneCamera = SceneView.currentDrawingSceneView.camera;
        CBPostion = sceneCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 30f));
        _CornerBase.transform.position = CBPostion;
    }
    private void MakeCornerBase()
    {
        if(_CornerRoof != null)
        {
            _CornerRoof.transform.parent = null;

        }
        if(_CornerF1Detail != null)
        {
            _CornerF1Detail.transform.parent = null;
        }
       
        if(_CornerBase != null)
        {
            CBPostion = _CornerBase.transform.position;
            CBRotation = _CornerBase.transform.rotation;
            DestroyImmediate(_CornerBase);
        }
        
       
        if(_cBCurBaseDesign == 2)
        {

            GameObject CBase = (GameObject)Instantiate(Resources.Load(_curnerBuildingBasePath + _cBCurBaseDesign + "_" + _cBMin));
            CBase.transform.position = CBPostion;
            CBase.transform.rotation = CBRotation;

            _CornerBase = CBase;

            if(_CornerRoof != null)
            {
                _CornerRoof.transform.parent = _CornerBase.transform;
            }
            if (_CornerF1Detail != null)
            {
                _CornerF1Detail.transform.parent = _CornerBase.transform;
            }
            
           
        }
        else if(_cBCurBaseDesign == 1)
        {
            GameObject CBase = (GameObject)Instantiate(Resources.Load(_curnerBuildingBasePath + _cBCurBaseDesign + "_" + _cBCurBaseTexture));
            CBase.transform.position = CBPostion;
            CBase.transform.rotation = CBRotation;

            _CornerBase = CBase;

            if (_CornerRoof != null)
            {
                _CornerRoof.transform.parent = _CornerBase.transform;
            }
            if (_CornerF1Detail != null)
            {
                _CornerF1Detail.transform.parent = _CornerBase.transform;
            }
            
           
        }

        MakeCornerMid();
        
    }
    private void MakeCornerRoof()
    {
        if (_CornerRoofDetail != null)
            _CornerRoofDetail.transform.parent = null;
        if (_CornerRoof != null)
            DestroyImmediate(_CornerRoof);
    
        GameObject CRoof = (GameObject)Instantiate(Resources.Load(_curnerBuildignRoofPath + _cBCurRoofDesign + "_" + _cBCurRoofTexture));

        CRoof.transform.parent = _CornerBase.transform;
        CRoof.transform.rotation = _CornerBase.transform.rotation;


        if (_cBCurMidAmount == 0)
        {
            CRoof.transform.position = new Vector3(_CornerBase.transform.position.x, _CornerBase.transform.position.y + cBRoofMin, _CornerBase.transform.position.z);
        }
        else
        {
            float heigt = 0;
            for (int asd = 0; asd < _cBCurMidAmount; asd++)
            {
                heigt += CBMidPartsHeight;
            }
            CRoof.transform.position = new Vector3(_CornerBase.transform.position.x, _CornerBase.transform.position.y + heigt - CBMidPartsHeight, _CornerBase.transform.position.z);
        }
        _CornerRoof = CRoof;
        if (_CornerRoofDetail != null)
        {
            MakeCornerRoofDetail();
        }
            

    }
    private void MakeCornerMid()
    {
        //Destroy all current MidParts in case the design was change
        
        for (int asd = 0; asd < _CMidParts.Count; asd++)
        {
            DestroyImmediate(_CMidParts[asd]);
        }
        
        for(int asd = 0;asd < _cBCurMidAmount ;asd++)
        {
            GameObject MidP = (GameObject)Instantiate(Resources.Load(_curnerBuildignMidPath + _cBCurMidDesign + "_" + _cBCurMidTexture ));
            if(houseName == "")
            {
                MidP.name = " Corner Building Mid Part " + (asd +1);
            }
            else
            {
                MidP.name = houseName + "(Mid Part " + (asd + 1) + ")";
            }
            
            MidP.transform.parent = _CornerBase.transform;

            //Place it were its need to be 

            MidP.transform.position = new Vector3(_CornerBase.transform.position.x, _CornerBase.transform.position.y + CBMidPartsHeight * asd, _CornerBase.transform.position.z);
            MidP.transform.rotation = _CornerBase.transform.rotation;
            _CMidParts.Add(MidP);
        }
        MakeCornerRoof();

    }
    private void MakeCornerF1Detail()
    {
        if (_CornerF1Detail != null)
            DestroyImmediate(_CornerF1Detail);
        if (_cBCurF1Details == 0)
            return;
        GameObject f1D = (GameObject)Instantiate(Resources.Load(_curnerBuildignF1DetailsPath + _cBCurF1Details));
        _CornerF1Detail = f1D;
        f1D.transform.position = new Vector3(_CornerBase.transform.position.x, _CornerBase.transform.position.y, _CornerBase.transform.position.z);
        f1D.transform.rotation = _CornerBase.transform.rotation;
        f1D.transform.parent = _CornerBase.transform;

    }
    private void MakeCornerRoofDetail()
    {
        if(_CornerRoofDetail != null)
        {
            DestroyImmediate(_CornerRoofDetail);
        }
        if (_cBCurRoofDetails == 0)
            return;
        GameObject RoofD = (GameObject)Instantiate(Resources.Load(_curnerBuildignRoofDetailsPath + _cBCurRoofDetails));
        RoofD.transform.parent = _CornerRoof.transform;

        RoofD.transform.position = new Vector3(_CornerRoof.transform.position.x, _CornerRoof.transform.position.y + 18.22f, _CornerRoof.transform.position.z - 22f);
       // RoofD.transform.rotation = _CornerRoof.transform.rotation;

        _CornerRoofDetail = RoofD;
    }

    #endregion

    #region UICleaning
    private void ClearAndMakeNew()
    {
        //Destroy if not confirm
        if (_VHouseBase != null)        
            DestroyImmediate(_VHouseBase);
        
        if (_VHouseF2 != null)        
            DestroyImmediate(_VHouseF2);
        
        if (_VHouseRoof != null)        
            DestroyImmediate(_VHouseRoof);

        //Destroy Corner Building Base 
        if (_CornerBase != null)
            DestroyImmediate(_CornerBase);
        
       /*-----------------------------------------------------------------*/
        //Clean the data
        ClearData();
        
       
    }
    private void ClearData()
    {
        //Display Options to create buttons
        _hbuttonBool = false;
        _vbuttonBool = false;
        _cbuildingsButton = false;
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

        #region Corner Building Region

        _CornerBase = null; 

        _cBCurBaseDesign = _cBMin;
        _cBCurBaseTexture = _cBMin;
        _cBCurMidDesign = _cBMin;
        _CMidParts.Clear();
        _cBCurMidTexture = _cBMin;
        _cBCurRoofDesign = _cBMin;
        _cBCurRoofDetails = _cBMin;
        _cBCurRoofTexture = _cBMin;
        _cBCurF1Details = _cBMin;
        _cBCurMidAmount = 0;
        

        #endregion

        houseName = "";
        LablesUpdate();
        _addVF2 = false; 
        /*--------------------------------------------------------------------*/
    }

    private void LablesUpdate()
    {
        #region Vertical Houses Part
        curVBaseString = "Base Design: " + _CurvbaseDesign + " / " + _vbaseDesignMax + "   Texture " + _CurvbaseTexture + " / " + _vbaseTextureMaxVar;
       
        curVF2String = "F2 Design: "+ _vf2DesignMin + " / " + _vf2DesignMax + "   Texture: " + _Curvf2Texture + " / " + _vf2TextureMaxVar;

        if(_CurvroofDesign == 1)
        curVRoofString = "Roof Design:" + _CurvroofDesign + " / " + _vroofDesignMax + " Roof: " + _CurvroofTexture + " / " + _vroofTextureMaxVar;
        else
            curVRoofString = "Roof Design:" + _CurvroofDesign + " / " + _vroofDesignMax + " Walls: "  + _CurvroofWallTexture + " / " + _vroofWallTextureMax + " Roof: " + _CurvroofTexture + " / " + _vroofTextureMaxVar;
        #endregion

        #region Corner Building Part

        #endregion

    }

    #endregion

}
