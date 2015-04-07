using UnityEngine;
using System.Collections;
//using UnityEditor;

public class HouseCustom : MonoBehaviour
{
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

    #endregion

    #region GOsParts

    //house007

    //[SerializeField]
    private GameObject _House007Door1;
   // [SerializeField]
    private GameObject _House007Door2;
    //[SerializeField]
    private GameObject _House007Door3;
   // [SerializeField]
    private GameObject _House007Floor1 ;
   // [SerializeField]
    private GameObject _house007Floor2 ;
   // [SerializeField]
    private GameObject _house007Floor3 ;
    //[SerializeField]
    private GameObject _house007Garage ;
   // [SerializeField]
    private GameObject _house007GarageDoor;
   // [SerializeField]
    private GameObject _house007InsideWalls ;
   // [SerializeField]
    private GameObject _house007OutSideWalls ;
   // [SerializeField]
    private GameObject _house007Roof;
   // [SerializeField]
    private GameObject _house007TransparenGlass ;
  //  [SerializeField]
    private GameObject _house007Frames;
   // [SerializeField]
    private GameObject _house007InsideParts;
   // [SerializeField]
    private GameObject _house007Colliders;
    

    #endregion

    #region Vars
    //door 1
    private int _MaxDoor1 = 6;
    private int _CurDoor1 = 1;
    //door 2
    private int _MaxDoor2 = 6;
    private int _CurDoor2 = 1;
    //door 3
    private int _MaxDoor3 = 6;
    private int _CurDoor3 = 1;
    //floor 1
    private int _MaxFloor1 = 18;
    private int _CurFloor1 = 1;
    //floor 2
    private int _MaxFloor2 = 18;
    private int _CurFloor2 = 1;
    //floor 3
    private int _MaxFloor3 = 16;
    private int _CurFloor3 = 1;
    //garage
    private int _MaxGarage = 12;
    private int _CurGarage = 1;
    //garage door
    private int _MaxGarageDoor = 5;
    private int _CurGarageDoor = 1;
    //Inside Walls
    private int _MaxInsideWalls = 10;
    private int _CurInsideWalls = 1;
    //Outside Walls
    private int _MaxOutSideWalls = 13;
    private int _CurOutSideWalls = 1;
    //Roofs
    private int _MaxRoof = 25;
    private int _CurRoof = 1;
    //Frames
    private int _CurFrames = 1;

    #region geters and setters
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
    //Bools
    public bool MakeAhouse007 { get { return _MakeAhouse007; } set { _MakeAhouse007 = value; } }



    
    

    #endregion

    private Vector3 _Door1Pos = new Vector3(-7.6f, 0.25f, 5.25f);
    private Vector3 _Door2Pos = new Vector3(1.55f, 0.25f, -5.7f);
    private Vector3 _Door3Pos = new Vector3(3.86f, 0.25f, -2.69f);
    private Vector3 _GarageDoorPos = new Vector3(2.07f, 3.34f, 4.91f);

    private bool _MakeAhouse007 = false;
 
    #endregion


    [HideInInspector]
    public bool _NewHouse = false;
	
	void Update () 
    {
	
	}
    void OnGui()
    {
        /*if (GUILayout.Button("New House"))
        {
            Debug.Log("Make new house 007");
        }
         * */
        
                
    }
    [ContextMenu("New House 007")]
    public void MakeNewCustomHouse007()
    {
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

    /*
    private void CreateNewPart(GameObject goRef, string path, int cur, string nametoShow)
    {
        if (goRef != null)
        {
            
            DestroyImmediate(goRef);
            GameObject GO = (GameObject)Instantiate(Resources.Load(path + cur.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            goRef = GO;
            GO.name = nametoShow;
            GO.transform.parent = gameObject.transform;
            Debug.Log(goRef);
            

        }
        else
        {
            
            GameObject GO = (GameObject)Instantiate(Resources.Load(path + cur.ToString()), gameObject.transform.position, gameObject.transform.rotation);
            goRef = GO;
            GO.name = nametoShow;
            GO.transform.parent = gameObject.transform;
            Debug.Log(goRef);

        }
    }
     * 
     * */
    
    
}
