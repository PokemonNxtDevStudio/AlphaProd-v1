using UnityEngine;
using System.Collections;
//using UnityEditor;

public class HouseCustom : MonoBehaviour
{
    public enum TypeOfHouse
    {
        None,
        House007,
        House008,
        House009
    }
    #region Paths

    //house007
    private string _House007Door1Path = "House_007/Door/Door_1_v";
    private string _House007Door2Path = "House_007/Door/Door_2_v";
    private string _House007Door3Path = "House_007/Door/Door_3_v";
    private string _House007Floor1Path = "House_007/Floor1/House_007_Floor_01_v";
    private string _house007Floor2Path = "House_007/Floor2/House_007_Floor_02_v";
    private string _house007Floor3Path = "House_007/Floor3/House_007_Floor_03_v";
    private string _house007GaragePath = "House_007/Garage/House_007_Garage_v";
    private string _house007GarageDoorPath = "House_007/GarageDoor/GarashDoor_v";
    private string _house007InsideWallsPath = "House_007/InsideWalls/House_007_InsideWalls_v";
    private string _house007OutSideWallsPath = "House_007/OutsideWalls/House_007_OutsideWalls_v";
    private string _house007RoofPath = "House_007/Roof/House_007_Roof_V";
    private string _house007FramesPath = "House_007/House_007_Frames_v";
    private string _house007InsidePath = "House_007/House_007_Inside";
    private string _house007TransparenGlassPath = "House_007/Windows_T";
    private string _house007CollidersPath = "House_007/House007_Colliders";

    private string _house009RoofPath = "House_009/Roof/House_009_Roofs_v";
    private string _house009OutSideWallsPath = "House_009/OutsideWall/House_009_OutSideWalls_v";
    private string _house009InsideWallsPath = "House_009/InsideWall/House_009_Insides_v";
    private string _house009PilarsPath = "House_009/Pilar/House_009_Pilars_v";
    private string _house009InteriorRoofsPath = "House_009/House_009_Inside_Roofs_v1";
    private string _house009GlassesPath = "House_009/House_009_Glasses_T";
    private string _house009StairsPath = "House_009/House_009_Inside_stairs_v1";
    private string _house009F1FloorPath = "House_009/F1Floor/House_009_F1_Floor_v";
    private string _house009F2FloorPath = "House_009/F2Floor/House_009_F2_Floor_v";
    private string _house009CollidersPath = "House_009/House009_Colliders";
    private string _house009Door1Path = "House_009/Door1/Door_01_v";
    private string _house009Door2Path = "House_009/Door2/Door_02_v";
    private string _house009Door3Path = "House_009/Door3/Door_03_v";
    private string _house009Door4Path = "House_009/Door4/Door_04_v";
    #endregion

    #region GOsParts

    //house007

    [SerializeField]
    private GameObject _House007Door1;
    [SerializeField]
    private GameObject _House007Door2;
    [SerializeField]
    private GameObject _House007Door3;
    [SerializeField]
    private GameObject _House007Floor1 ;
    [SerializeField]
    private GameObject _house007Floor2 ;
    [SerializeField]
    private GameObject _house007Floor3 ;
    [SerializeField]
    private GameObject _house007Garage ;
    [SerializeField]
    private GameObject _house007GarageDoor;
    [SerializeField]
    private GameObject _house007InsideWalls ;
    [SerializeField]
    private GameObject _house007OutSideWalls ;
    [SerializeField]
    private GameObject _house007Roof;
    [SerializeField]
    private GameObject _house007TransparenGlass ;
    [SerializeField]
    private GameObject _house007Frames;
    [SerializeField]
    private GameObject _house007InsideParts;
    [SerializeField]
    private GameObject _house007Colliders;

    private TypeOfHouse tyHouse;


    private GameObject _GoHouse009Roof ;
    private GameObject _GoHouse009OutSideWall;
    private GameObject _GoHouse009InsideWall ;
    private GameObject _GoHouse009Pilar ;
    private GameObject _GoHouse009F1Floor;
    private GameObject _GoHouse009F2Floor;
    private GameObject _GoHouse009Colliders;
    private GameObject _GoHouse009Stairs;
    private GameObject _GoHouse009InteriorRoofs;
    private GameObject _GoHouse007Glass;



    private GameObject _Door1Go;
    private GameObject _Door2Go;
    private GameObject _Door3Go;
    private GameObject _Door4Go;


    #endregion

    #region Vars
    //door 1
    private int _MaxDoor1 = 6;
    private int _CurDoor1 ;
    //door 2
    private int _MaxDoor2 = 6;
    private int _CurDoor2 ;
    //door 3
    private int _MaxDoor3 = 6;
    private int _CurDoor3 ;
    //floor 1
    private int _MaxFloor1 = 18;
    private int _CurFloor1 ;
    //floor 2
    private int _MaxFloor2 = 18;
    private int _CurFloor2 ;
    //floor 3
    private int _MaxFloor3 = 16;
    private int _CurFloor3 ;
    //garage
    private int _MaxGarage = 12;
    private int _CurGarage ;
    //garage door
    private int _MaxGarageDoor = 5;
    private int _CurGarageDoor ;
    //Inside Walls
    private int _MaxInsideWalls = 10;
    private int _CurInsideWalls ;
    //Outside Walls
    private int _MaxOutSideWalls = 13;
    private int _CurOutSideWalls ;
    //Roofs
    private int _MaxRoof = 25;
    private int _CurRoof ;
    //Frames
    private int _CurFrames = 1;

    //House 009 F1 floor
    private int _h9MaxFloor1 = 16;
    private int _h9CurFloor1 ;
    //House 009 F2 Floor
    private int _h9MaxFloor2 = 16;
    private int _h9CurFloor2 ;
    //House 009 Inside Walls
    private int _h9MaxInsideWalls = 10;
    private int _h9CurInsideWalls ;
    //House 009 OutSide Walls
    private int _h9MaxOutSideWalls = 13;
    private int _h9CurOutSideWalls ;
    //House 009 Pillars
    private int _h9MaxPillars = 3;
    private int _h9CurPillars ;
    //House 009 Roofs
    private int _h9MaxRoofs = 25;
    private int _h9CurRoofs ;
    //House 009 door1
    private int _h9MaxDoor1 = 6;
    private int _h9CurDoor1 ;
    //House 009 door2
    private int _h9MaxDoor2 = 6;
    private int _h9CurDoor2 ;
    //House 009 door3
    private int _h9MaxDoor3 = 6;
    private int _h9CurDoor3 ;
    //House 009 door4
    private int _h9MaxDoor4 = 6;
    private int _h9CurDoor4 ;

    #region geters and setters
    #region House007 gets ands sets
    //doors
    public int MaxDoor1 { get { return _MaxDoor1; } set { _MaxDoor1 = value; } }
    public int CurDoor1 { get { return _CurDoor1; } set { _CurDoor1 = value; } }
    public int MaxDoor2 { get { return _MaxDoor2; } set { _MaxDoor2 = value; } }
    public int CurDoor2 { get { return _CurDoor2; } set { _CurDoor2 = value; } }
    public int MaxDoor3 { get { return _MaxDoor3; } set { _MaxDoor3 = value; } }
    public int CurDoor3 { get { return _CurDoor3; } set { _CurDoor3 = value; } }
    //floors
    public int MaxFloor1 { get { return _MaxFloor1; } set { _MaxFloor1 = value; } }
    public int CurFloor1 { get { return _CurFloor1; } set { _CurFloor1 = value; } }
    public int MaxFloor2 { get { return _MaxFloor2; } set { _MaxFloor2 = value; } }
    public int CurFloor2 { get { return _CurFloor2; } set { _CurFloor2 = value; } }
    public int MaxFloor3 { get { return _MaxFloor3; } set { _MaxFloor3 = value; } }
    public int CurFloor3 { get { return _CurFloor3; } set { _CurFloor3 = value; } }
    //garage
    public int MaxGarage { get { return _MaxGarage; } set { _MaxGarage = value; } }
    public int CurGarage { get { return _CurGarage; } set { _CurGarage = value; } }
    //garage door
    public int MaxGarageDoor { get { return _MaxGarageDoor; } set { _MaxGarageDoor = value; } }
    public int CurGarageDoor { get { return _CurGarageDoor; } set { _CurGarageDoor = value; } }
    //Inside Walls
    public int MaxInsideWalls { get { return _MaxInsideWalls; } set { _MaxInsideWalls = value; } }
    public int CurInsideWalls { get { return _CurInsideWalls; } set { _CurInsideWalls = value; } }
    //OutSide Walls
    public int MaxOutSideWalls { get { return _MaxOutSideWalls; } set { _MaxOutSideWalls = value; } }
    public int CurOutSideWalls { get { return _CurOutSideWalls; } set { _CurOutSideWalls = value; } }
    //Roofs
    public int MaxRoof { get { return _MaxRoof; } set { _MaxRoof = value; } }
    public int CurRoof { get { return _CurRoof; } set { _CurRoof = value; } }
    #endregion

    
    #region House009 Gets and Sets 

    public int H9MaxFloor1 { get { return _h9MaxFloor1; } }
    public int H9CurFloor1 { get { return _h9CurFloor1; } set { _h9CurFloor1 = value; } }

    public int H9MaxFloor2 { get { return _h9MaxFloor2; } }
    public int H9CurFloor2 { get { return _h9CurFloor2; } set { _h9CurFloor2 = value; } }
    
    public int H9MaxInsideWalls { get { return _h9MaxInsideWalls; } }
    public int H9CurInsideWalls { get { return _h9CurInsideWalls; } set { _h9CurInsideWalls = value; } }

    public int H9MaxOutSideWalls { get { return _h9MaxOutSideWalls; } }
    public int H9CurOutSideWalls { get { return _h9CurOutSideWalls; } set { _h9CurOutSideWalls = value; } }

    public int H9MaxPillars { get { return _h9MaxPillars; } }
    public int H9CurPillars { get { return _h9CurPillars; } set { _h9CurPillars = value; } }

    public int H9MaxRoofs { get { return _h9MaxRoofs; } }
    public int H9CurRoofs { get { return _h9CurRoofs; } set { _h9CurRoofs = value; } }

    public int H9MaxDoor1 { get { return _h9MaxDoor1; } }
    public int H9CurDoor1 { get { return _h9CurDoor1; } set { _h9CurDoor1 = value; } }

    public int H9MaxDoor2 { get { return _h9MaxDoor2; } }
    public int H9CurDoor2 { get { return _h9CurDoor2; } set { _h9CurDoor2 = value; } }

    public int H9MaxDoor3 { get { return _h9MaxDoor3; } }
    public int H9CurDoor3 { get { return _h9CurDoor3; } set { _h9CurDoor3 = value; } }

    public int H9MaxDoor4 { get { return _h9MaxDoor4; } }
    public int H9CurDoor4 { get { return _h9CurDoor4; } set { _h9CurDoor4 = value; } }


    #endregion
     
    public TypeOfHouse TheTypeOfHouse { get { return tyHouse; } set { tyHouse = value; } }



    
    

    #endregion

    private Vector3 _Door1Pos = new Vector3(-7.6f, 0.25f, 5.25f);
    private Vector3 _Door2Pos = new Vector3(1.55f, 0.25f, -5.7f);
    private Vector3 _Door3Pos = new Vector3(3.86f, 0.25f, -2.69f);
    private Vector3 _GarageDoorPos = new Vector3(2.07f, 3.34f, 4.91f);

   


    #endregion


    [HideInInspector]
    public bool _NewHouse = false;
	

    //[ContextMenu("New House 007")]
    public void MakeNewCustomHouse007()
    {
        
        TheTypeOfHouse = TypeOfHouse.House007;
        gameObject.name = "House 007";
        CreateHouse007OutsideWalls();
        CreateHouse007InsideWalls();
        CreateHouse007Floor1();
        CreateHouse007Floor2();
        CreateHouse007Floor3();
        CreateHouse007Frames();        
        CreateHouse007Door1();
        CreateHouse007Door2();
        CreateHouse007Door3();
        CreateHouse007Garage();
        CreateHouse007GarageDoor();
        CreateHouse007Roof();
        CreateHouse007TWindows();
        CreateHouse007InsideParts();
        CreateHouse007Colliders();

       
    }
    public void minCurSeting()
    {
        _CurDoor1 = 1;
        _CurDoor2 = 1;
        _CurDoor3 = 1;
        _CurFloor1 = 1;
        _CurFloor2 = 1;
        _CurFloor3 = 1;
        _CurFrames = 1;
        _CurGarage = 1;
        _CurGarageDoor = 1;
        _CurInsideWalls = 1;
        _CurOutSideWalls = 1;
        _CurRoof = 1;
        _h9CurDoor1 = 1;
        _h9CurDoor2 = 1;
        _h9CurDoor3 = 1;
        _h9CurDoor4 = 1;
        _h9CurFloor1 = 1;
        _h9CurFloor2 = 1;
        _h9CurInsideWalls = 1;
        _h9CurOutSideWalls = 1;
        _h9CurPillars = 1;
        _h9CurRoofs = 1;
      
    }

    public void DeleteHouse007()
    {
        if(_House007Door1 != null)
        {
            DestroyImmediate(_House007Door1);
        }
        if(_House007Door2 != null)
        {
            DestroyImmediate(_House007Door2);
        }
        if(_House007Door3 != null)
        {
            DestroyImmediate(_House007Door3);
        }
        if(_House007Floor1 != null)
        {
            DestroyImmediate(_House007Floor1);
        }
        if(_house007Floor2 != null)
        {
            DestroyImmediate(_house007Floor2);
        }
        if(_house007Floor3 != null)
        {
            DestroyImmediate(_house007Floor3);
        }
        if(_house007Frames != null)
        {
            DestroyImmediate(_house007Frames);
        }
        if(_house007Garage != null)
        {
            DestroyImmediate(_house007Garage);
        }
        if(_house007GarageDoor != null)
        {
            DestroyImmediate(_house007GarageDoor);
        }
        if(_house007InsideParts != null)
        {
            DestroyImmediate(_house007InsideParts);
        }
        if(_house007InsideWalls != null)
        {
            DestroyImmediate(_house007InsideWalls);
        }
        if(_house007OutSideWalls != null)
        {
            DestroyImmediate(_house007OutSideWalls);
        }
        if(_house007Roof != null)
        {
            DestroyImmediate(_house007Roof);
        }
        if(_house007TransparenGlass != null)
        {
            DestroyImmediate(_house007TransparenGlass);
        }
        if(_house007Colliders != null)
        {
            DestroyImmediate(_house007Colliders);
        }
    }


    #region House007 Parts

   

    private void CreateHouse007Door1()
    {
        if (_House007Door1 != null)
        {

            DestroyImmediate(_House007Door1);
            _House007Door1 = (GameObject)Instantiate(Resources.Load(_House007Door1Path + _CurDoor1.ToString()), gameObject.transform.position + _Door1Pos, gameObject.transform.rotation);
            _House007Door1.name = "Door 1";
            _House007Door1.transform.parent = gameObject.transform;



        }
        else
        {

            _House007Door1 = (GameObject)Instantiate(Resources.Load(_House007Door1Path + _CurDoor1.ToString()), gameObject.transform.position + _Door1Pos, gameObject.transform.rotation);
            _House007Door1.name = "Door 1";
            _House007Door1.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007Door2()
    {
        if (_House007Door2 != null)
        {

            DestroyImmediate(_House007Door2);
            _House007Door2 = (GameObject)Instantiate(Resources.Load(_House007Door2Path + _CurDoor2.ToString()), gameObject.transform.position + _Door2Pos, gameObject.transform.rotation);
            _House007Door2.name = "Door 2";
            _House007Door2.transform.parent = gameObject.transform;



        }
        else
        {

            _House007Door2 = (GameObject)Instantiate(Resources.Load(_House007Door2Path + _CurDoor2.ToString()), gameObject.transform.position + _Door2Pos, gameObject.transform.rotation);
            _House007Door2.name = "Door 2";
            _House007Door2.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007Door3()
    {
        if (_House007Door3 != null)
        {

            DestroyImmediate(_House007Door3);
            _House007Door3 = (GameObject)Instantiate(Resources.Load(_House007Door3Path + _CurDoor3.ToString()), gameObject.transform.position + _Door3Pos, gameObject.transform.rotation);
            _House007Door3.name = "Door 3";
            _House007Door3.transform.parent = gameObject.transform;



        }
        else
        {

            _House007Door3 = (GameObject)Instantiate(Resources.Load(_House007Door3Path + _CurDoor3.ToString()), gameObject.transform.position + _Door3Pos, gameObject.transform.rotation);
            _House007Door3.name = "Door 3";
            _House007Door3.transform.parent = gameObject.transform;
        }
    }
   
    private void CreateHouse007Frames()
    {
        if (_house007Frames != null)
        {

            DestroyImmediate(_house007Frames);
            _house007Frames = (GameObject)Instantiate(Resources.Load(_house007FramesPath + _CurFrames.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Frames.name = "House007_Frames";
            _house007Frames.transform.parent = gameObject.transform;



        }
        else
        {

            _house007Frames = (GameObject)Instantiate(Resources.Load(_house007FramesPath + _CurFrames.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Frames.name = "House007_Frames";
            _house007Frames.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007Floor1()
    {
        if (_House007Floor1 != null)
        {

            DestroyImmediate(_House007Floor1);
            _House007Floor1 = (GameObject)Instantiate(Resources.Load(_House007Floor1Path + _CurFloor1.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _House007Floor1.name = "House_007_Floor1";
            _House007Floor1.transform.parent = gameObject.transform;



        }
        else
        {

            _House007Floor1 = (GameObject)Instantiate(Resources.Load(_House007Floor1Path + _CurFloor1.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _House007Floor1.name = "House_007_Floor1";
            _House007Floor1.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007Floor2()
    {
        if (_house007Floor2 != null)
        {

            DestroyImmediate(_house007Floor2);
            _house007Floor2 = (GameObject)Instantiate(Resources.Load(_house007Floor2Path + _CurFloor2.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Floor2.name = "House_007_Floor2";
            _house007Floor2.transform.parent = gameObject.transform;



        }
        else
        {

            _house007Floor2 = (GameObject)Instantiate(Resources.Load(_house007Floor2Path + _CurFloor2.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Floor2.name = "House_007_Floor2";
            _house007Floor2.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007Floor3()
    {
        if (_house007Floor3 != null)
        {

            DestroyImmediate(_house007Floor3);
            _house007Floor3 = (GameObject)Instantiate(Resources.Load(_house007Floor3Path + _CurFloor3.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Floor3.name = "House_007_Floor3";
            _house007Floor3.transform.parent = gameObject.transform;



        }
        else
        {

            _house007Floor3 = (GameObject)Instantiate(Resources.Load(_house007Floor3Path + _CurFloor3.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Floor3.name = "House_007_Floor3";
            _house007Floor3.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007OutsideWalls()
    {
        if (_house007OutSideWalls != null)
        {

            DestroyImmediate(_house007OutSideWalls);
            _house007OutSideWalls = (GameObject)Instantiate(Resources.Load(_house007OutSideWallsPath + _CurOutSideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007OutSideWalls.name = "House_007_OutsideWalls";
            _house007OutSideWalls.transform.parent = gameObject.transform;



        }
        else
        {

            _house007OutSideWalls = (GameObject)Instantiate(Resources.Load(_house007OutSideWallsPath + _CurOutSideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007OutSideWalls.name = "House_007_OutsideWalls";
            _house007OutSideWalls.transform.parent = gameObject.transform;
        }
    }
    private void CreateHouse007InsideWalls()
    {
        if (_house007InsideWalls != null)
        {

            DestroyImmediate(_house007InsideWalls);
            _house007InsideWalls = (GameObject)Instantiate(Resources.Load(_house007InsideWallsPath + _CurInsideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007InsideWalls.name = "House_007_InsideWalls";
            _house007InsideWalls.transform.parent = gameObject.transform;



        }
        else
        {

            _house007InsideWalls = (GameObject)Instantiate(Resources.Load(_house007InsideWallsPath + _CurInsideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007InsideWalls.name = "House_007_InsideWalls";
            _house007InsideWalls.transform.parent = gameObject.transform;
        }
    }

    private void CreateHouse007Garage()
    {
        if (_house007Garage != null)
        {
            DestroyImmediate(_house007Garage);
            _house007Garage = (GameObject)Instantiate(Resources.Load(_house007GaragePath + _CurGarage.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Garage.name = "House_007_GarageWalls";
            _house007Garage.transform.parent = gameObject.transform;



        }
        else
        {
            _house007Garage = (GameObject)Instantiate(Resources.Load(_house007GaragePath + _CurGarage.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Garage.name = "House_007_GarageWalls";
            _house007Garage.transform.parent = gameObject.transform;
        }
    }

    private void CreateHouse007GarageDoor()
    {
        if (_house007GarageDoor != null)
        {
            DestroyImmediate(_house007GarageDoor);
            _house007GarageDoor = (GameObject)Instantiate(Resources.Load(_house007GarageDoorPath + _CurGarageDoor.ToString()), gameObject.transform.position + _GarageDoorPos, gameObject.transform.rotation);
            _house007GarageDoor.name = "House_007_GarageDoor";
            _house007GarageDoor.transform.parent = gameObject.transform;



        }
        else
        {
            _house007GarageDoor = (GameObject)Instantiate(Resources.Load(_house007GarageDoorPath + _CurGarageDoor.ToString()), gameObject.transform.position + _GarageDoorPos, gameObject.transform.rotation);
            _house007GarageDoor.name = "House_007_GarageDoor";
            _house007GarageDoor.transform.parent = gameObject.transform;
        }
    }

    private void CreateHouse007Roof()
    {
        if (_house007Roof != null)
        {
            DestroyImmediate(_house007Roof);
            _house007Roof = (GameObject)Instantiate(Resources.Load(_house007RoofPath + _CurRoof.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Roof.name = "House_007_Roof";
            _house007Roof.transform.parent = gameObject.transform;



        }
        else
        {
            _house007Roof = (GameObject)Instantiate(Resources.Load(_house007RoofPath + _CurRoof.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            _house007Roof.name = "House_007_Roof";
            _house007Roof.transform.parent = gameObject.transform;
        }
    }

    private void CreateHouse007TWindows()
    {
        if (_house007TransparenGlass != null)
        {
            DestroyImmediate(_house007TransparenGlass);
            _house007TransparenGlass = (GameObject)Instantiate(Resources.Load(_house007TransparenGlassPath), gameObject.transform.position, gameObject.transform.rotation);
            _house007TransparenGlass.name = "House_007_TransparentWindows";
            _house007TransparenGlass.transform.parent = gameObject.transform;



        }
        else
        {
            _house007TransparenGlass = (GameObject)Instantiate(Resources.Load(_house007TransparenGlassPath), gameObject.transform.position, gameObject.transform.rotation);
            _house007TransparenGlass.name = "House_007_TransparentWindows";
            _house007TransparenGlass.transform.parent = gameObject.transform;
        }
    }

    private void CreateHouse007InsideParts()
    {
        if (_house007InsideParts != null)
        {
            DestroyImmediate(_house007InsideParts);
            _house007InsideParts = (GameObject)Instantiate(Resources.Load(_house007InsidePath), gameObject.transform.position, gameObject.transform.rotation);
            _house007InsideParts.name = "House_007_InsideParts";
            _house007InsideParts.transform.parent = gameObject.transform;



        }
        else
        {
            _house007InsideParts = (GameObject)Instantiate(Resources.Load(_house007InsidePath), gameObject.transform.position, gameObject.transform.rotation);
            _house007InsideParts.name = "House_007_InsideParts";
            _house007InsideParts.transform.parent = gameObject.transform;
        }
        
    }

    private void CreateHouse007Colliders()
    {
        if (_house007Colliders != null)
        {
            DestroyImmediate(_house007Colliders);
            _house007Colliders = (GameObject)Instantiate(Resources.Load(_house007CollidersPath), gameObject.transform.position, gameObject.transform.rotation);
            _house007Colliders.name = "House_007_Colliders";
            _house007Colliders.transform.parent = gameObject.transform;



        }
        else
        {
            _house007Colliders = (GameObject)Instantiate(Resources.Load(_house007CollidersPath), gameObject.transform.position, gameObject.transform.rotation);
            _house007Colliders.name = "House_007_Colliders";
            _house007Colliders.transform.parent = gameObject.transform;
        }
    }

    

    #endregion

    public void MakeNewCustomHouse009()
    {
        minCurSeting();
        TheTypeOfHouse = TypeOfHouse.House009;
        gameObject.name = "House009";
        H9Roof();
        H9OutSideWall();
        H9InSideWall();
        H9Pillar();
        H9F1Floor();
        H9F2Floor();
        H9InsideRoofs();
        H9Stairs();
        H9Glass();
        H9Colliders();
        H9Door1();
        H9Door2();
        H9Door3();
        H9Door4();
        
    }

    public void DeleteCustomHouse009()
    {
        if (_GoHouse007Glass != null)
            DestroyImmediate(_GoHouse007Glass);
        if (_GoHouse009Colliders != null)
            DestroyImmediate(_GoHouse009Colliders);
        if (_GoHouse009F1Floor != null)
            DestroyImmediate(_GoHouse009F1Floor);
        if (_GoHouse009F2Floor != null)
            DestroyImmediate(_GoHouse009F2Floor);
        if (_GoHouse009InsideWall != null)
            DestroyImmediate(_GoHouse009InsideWall);
        if (_GoHouse009InteriorRoofs != null)
            DestroyImmediate(_GoHouse009InteriorRoofs);
        if (_GoHouse009OutSideWall != null)
            DestroyImmediate(_GoHouse009OutSideWall);
        if (_GoHouse009Pilar != null)
            DestroyImmediate(_GoHouse009Pilar);
        if (_GoHouse009Roof != null)
            DestroyImmediate(_GoHouse009Roof);
        if (_GoHouse009Stairs != null)
            DestroyImmediate(_GoHouse009Stairs);
        if (_Door1Go != null)
            DestroyImmediate(_Door1Go);
        if (_Door2Go != null)
            DestroyImmediate(_Door2Go);
        if (_Door3Go != null)
            DestroyImmediate(_Door3Go);
        if (_Door4Go != null)
            DestroyImmediate(_Door4Go);
    }

    private void H9Door1()
    {
        if (_Door1Go != null)
        {
            DestroyImmediate(_Door1Go);
        }
        _Door1Go = (GameObject)Instantiate(Resources.Load(_house009Door1Path + H9CurDoor1.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _Door1Go.name = "Door1 v" + H9CurDoor1;
        _Door1Go.transform.parent = gameObject.transform;
    }
    private void H9Door2()
    {
        if (_Door2Go != null)
        {
            DestroyImmediate(_Door2Go);
        }
        _Door2Go = (GameObject)Instantiate(Resources.Load(_house009Door2Path + H9CurDoor2.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _Door2Go.name = "Door2 v" + H9CurDoor2;
        _Door2Go.transform.parent = gameObject.transform;
    }
    private void H9Door3()
    {
        if (_Door3Go != null)
        {
            DestroyImmediate(_Door3Go);
        }
        _Door3Go = (GameObject)Instantiate(Resources.Load(_house009Door3Path + H9CurDoor3.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _Door3Go.name = "Door3 v" + H9CurDoor3;
        _Door3Go.transform.parent = gameObject.transform;
    }
    private void H9Door4()
    {
        if (_Door4Go != null)
        {
            DestroyImmediate(_Door4Go);
        }
        _Door4Go = (GameObject)Instantiate(Resources.Load(_house009Door4Path + H9CurDoor4.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _Door4Go.name = "Door4 v" + H9CurDoor4;
        _Door4Go.transform.parent = gameObject.transform;
    }


    public void H9PreviousDoor1()
    {
        if(H9CurDoor1 == 1)
        {
            H9CurDoor1 = H9MaxDoor1;
        }
        else
        {
            H9CurDoor1--;
        }
        H9Door1();
    }
    public void H9PreviousDoor2()
    {
        if (H9CurDoor2 == 1)
        {
            H9CurDoor2 = H9MaxDoor2;
        }
        else
        {
            H9CurDoor2--;
        }
        H9Door2();
    }
    public void H9PreviousDoor3()
    {
        if (H9CurDoor3 == 1)
        {
            H9CurDoor3 = H9MaxDoor3;
        }
        else
        {
            H9CurDoor3--;
        }
        H9Door3();
    }
    public void H9PreviousDoor4()
    {
        if (H9CurDoor4 == 1)
        {
            H9CurDoor4 = H9MaxDoor4;
        }
        else
        {
            H9CurDoor4--;
        }
        H9Door4();
    }

    public void H9NextDoor1()
    {
        if (H9CurDoor1 == H9MaxDoor1)
        {
            H9CurDoor1 = 1;
        }
        else
        {
            H9CurDoor1++;
        }
        H9Door1();
    }
    public void H9NextDoor2()
    {
        if (H9CurDoor2 == H9MaxDoor2)
        {
            H9CurDoor2 = 1;
        }
        else
        {
            H9CurDoor2++;
        }
        H9Door2();
    }
    public void H9NextDoor3()
    {
        if (H9CurDoor3 == H9MaxDoor3)
        {
            H9CurDoor3 = 1;
        }
        else
        {
            H9CurDoor3++;
        }
        H9Door3();

    }
    public void H9NextDoor4()
    {
        if (H9CurDoor4 == H9MaxDoor4)
        {
            H9CurDoor4 = 1;
        }
        else
        {
            H9CurDoor4++;
        }
        H9Door4();
    }

    private void H9Roof()
    {
        if (_GoHouse009Roof != null)
        {
            DestroyImmediate(_GoHouse009Roof);
        }
        _GoHouse009Roof = (GameObject)Instantiate(Resources.Load(_house009RoofPath + H9CurRoofs.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009Roof.name = "Roof v" + H9CurRoofs;
        _GoHouse009Roof.transform.parent = gameObject.transform;
    }
    
    public void H9PreviousRoof()
    {
        if(H9CurRoofs == 1)
        {
            H9CurRoofs = H9MaxRoofs;
        }
        else
        {
            H9CurRoofs--;
        }
        H9Roof();
    }
    public void H9NextRoof()
    {
        if (H9CurRoofs == H9MaxRoofs)
        {
            H9CurRoofs = 1;
        }
        else
        {
            H9CurRoofs++;
        }
        H9Roof();
    }

    private void H9OutSideWall()
    {
        if (_GoHouse009OutSideWall != null)
        {
            DestroyImmediate(_GoHouse009OutSideWall);
        }
        _GoHouse009OutSideWall = (GameObject)Instantiate(Resources.Load(_house009OutSideWallsPath + H9CurOutSideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009OutSideWall.name = "OutSideWalls v" + H9CurOutSideWalls;
        _GoHouse009OutSideWall.transform.parent = gameObject.transform;
    }
    public void H9PreviousOutSideWall()
    {
        if(H9CurOutSideWalls == 1)
        {
            H9CurOutSideWalls = H9MaxOutSideWalls;
        }
        else
        {
            H9CurOutSideWalls--;
        }
        H9OutSideWall();
    }
    public void H9NextOutSideWall()
    {
        if (H9CurOutSideWalls == H9MaxOutSideWalls)
        {
            H9CurOutSideWalls = 1;
        }
        else
        {
            H9CurOutSideWalls++;
        }
        H9OutSideWall();
    }
    private void H9InSideWall()
    {
        if (_GoHouse009InsideWall != null)
        {
            DestroyImmediate(_GoHouse009InsideWall);
        }
        _GoHouse009InsideWall = (GameObject)Instantiate(Resources.Load(_house009InsideWallsPath + H9CurInsideWalls.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009InsideWall.name = "InsideWalls v" + H9CurInsideWalls;
        _GoHouse009InsideWall.transform.parent = gameObject.transform;
    }
    
    public void H9PreviousInSideWall()
    {
        if(H9CurInsideWalls == 1)
        {
            H9CurInsideWalls = H9MaxInsideWalls;
        }
        else
        {
            H9CurInsideWalls--;
        }
        H9InSideWall();
    }
    public void H9NextInSideWall()
    {
        if (H9CurInsideWalls == H9MaxInsideWalls)
        {
            H9CurInsideWalls = 1;
        }
        else
        {
            H9CurInsideWalls++;
        }
        H9InSideWall();
    }
    private void H9Pillar()
    {
        if(_GoHouse009Pilar != null)
        {
            DestroyImmediate(_GoHouse009Pilar);
        }
        _GoHouse009Pilar = (GameObject)Instantiate(Resources.Load(_house009PilarsPath + H9CurPillars.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009Pilar.name = "Pillars v" + H9CurPillars;
        _GoHouse009Pilar.transform.parent = gameObject.transform;
    }
    
    public void H9PreviousPillar()
    {
        if(H9CurPillars == 1)
        {
            H9CurPillars = H9MaxPillars;
        }
        else
        {
            H9CurPillars--;
        }
        H9Pillar();
    }
    public void H9NextPillar()
    {
        if (H9CurPillars == H9MaxPillars)
        {
            H9CurPillars = 1;
        }
        else
        {
            H9CurPillars++;
        }
        H9Pillar();
    }
    private void H9F1Floor()
    {
        if (_GoHouse009F1Floor != null)
        {
            DestroyImmediate(_GoHouse009F1Floor);
        }
        _GoHouse009F1Floor = (GameObject)Instantiate(Resources.Load(_house009F1FloorPath + H9CurFloor1.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009F1Floor.name = "F1 Floor v" + H9CurFloor1;
        _GoHouse009F1Floor.transform.parent = gameObject.transform;
    }
    
    public void H9PreviousF1Floor()
    {
        if(H9CurFloor1 == 1)
        {
            H9CurFloor1 = H9MaxFloor1;
        }
        else
        {
            H9CurFloor1--;
        }
        H9F1Floor();
    }
    public void H9NextF1Floor()
    {
        if (H9CurFloor1 == H9MaxFloor1)
        {
            H9CurFloor1 = 1;
        }
        else
        {
            H9CurFloor1++;
        }
        H9F1Floor();
    }
    private void H9F2Floor()
    {
        if (_GoHouse009F2Floor != null)
        {
            DestroyImmediate(_GoHouse009F2Floor);
        }
        _GoHouse009F2Floor = (GameObject)Instantiate(Resources.Load(_house009F2FloorPath + H9CurFloor2.ToString()), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009F2Floor.name = "F2 Floor v" + H9CurFloor1;
        _GoHouse009F2Floor.transform.parent = gameObject.transform;
    }

    
    public void H9PreviousF2Floor()
    {
        if (H9CurFloor2 == 1)
        {
            H9CurFloor2 = H9MaxFloor2;
        }
        else
        {
            H9CurFloor2--;
        }
        H9F2Floor();
    }
    public void H9NextF2Floor()
    {
        if (H9CurFloor2 == H9MaxFloor2)
        {
            H9CurFloor2 = 1;
        }
        else
        {
            H9CurFloor2++;
        }
        H9F2Floor();
    }


    private void H9InsideRoofs()
    {
        _GoHouse009InteriorRoofs = (GameObject)Instantiate(Resources.Load(_house009InteriorRoofsPath), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009InteriorRoofs.name = "Interior Roofs";
        _GoHouse009InteriorRoofs.transform.parent = gameObject.transform;
    }
    private void H9Stairs()
    {
        _GoHouse009Stairs = (GameObject)Instantiate(Resources.Load(_house009StairsPath), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009Stairs.name = "Stairs";
        _GoHouse009Stairs.transform.parent = gameObject.transform;
    }
    private void H9Glass()
    {
        _GoHouse007Glass = (GameObject)Instantiate(Resources.Load(_house009GlassesPath), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse007Glass.name = "Windows Glass";
        _GoHouse007Glass.transform.parent = gameObject.transform;
    }
    private void H9Colliders()
    {
        _GoHouse009Colliders = (GameObject)Instantiate(Resources.Load(_house009CollidersPath), gameObject.transform.position, gameObject.transform.rotation);
        _GoHouse009Colliders.name = "House009 Colliders";
        _GoHouse009Colliders.transform.parent = gameObject.transform;
    }
   

    
    
    
}
