using UnityEngine;
using UnityEditor;
using System.Collections;

public class Custom3DHouses : MonoBehaviour
{

    #region Paths
    //House004 Paths 
    private string _h4Roof1Path = "Assets/Resources/House_004/Roof1/House_004_Roof_1_v";
    private string _h4Roof2Path = "Assets/Resources/House_004/Roof2/House_004_Roof_2_v";
    private string _h4OutsideWallsPath = "Assets/Resources/House_004/OutsideWalls/House_004_OutSideWalls_v";
    private string _h4InsideWallsPath = "Assets/Resources/House_004/InsideWalls/house_004_InsideWalls_v";
    private string _h4F1FloorPath = "Assets/Resources/House_004/F1Floor/House_004_F1Floor_v";
    private string _h4F2FloorPath = "Assets/Resources/House_004/F2Floor/House_004_F2Floor_v";
    private string _h4FrontStarisPath = "Assets/Resources/House_004/Detail/House_004_Detail_v";
    private string _h4FirePlacePath = "Assets/Resources/House_004/Detail/House_004_Detail_02_v";
    private string _h4StairsPath = "Assets/Resources/House_004/House_004_Stairs_v1";
    private string _h4CollidersPath = "Assets/Resources/House_004/House_004_Colliders";
    private string _h4BasementPath = "Assets/Resources/House_004/Basement/House_004_Basemant_v";
    private string _h4WindowsGlassesPath = "Assets/Resources/House_004/House_004_WindosGlases";
    private string _h4DoorPath = "Assets/Resources/House_004/Door/House_004_Door_v";
    //House007 Paths
    private string _House007Door1Path = "Assets/Resources/House_007/Door/Door_1_v";
    private string _House007Door2Path = "Assets/Resources/House_007/Door/Door_2_v";
    private string _House007Door3Path = "Assets/Resources/House_007/Door/Door_3_v";
    private string _House007Floor1Path = "Assets/Resources/House_007/Floor1/House_007_Floor_01_v";
    private string _house007Floor2Path = "Assets/Resources/House_007/Floor3/House_007_Floor_03_v";
    private string _house007Floor3Path = "Assets/Resources/House_007/Floor2/House_007_Floor_02_v";
    private string _house007GaragePath = "Assets/Resources/House_007/Garage/House_007_Garage_v";
    private string _house007GarageDoorPath = "Assets/Resources/House_007/GarageDoor/GarashDoor_v";
    private string _house007InsideWallsPath = "Assets/Resources/House_007/InsideWalls/House_007_InsideWalls_v";
    private string _house007OutSideWallsPath = "Assets/Resources/House_007/OutsideWalls/House_007_OutsideWalls_v";
    private string _house007RoofPath = "Assets/Resources/House_007/Roof/House_007_Roof_V";
    private string _house007FramesPath = "Assets/Resources/House_007/House_007_Frames_v";
    private string _house007StairsPath = "Assets/Resources/House_007/House_007_Inside";
    private string _house007TransparenGlassPath = "Assets/Resources/House_007/Windows_T";
    private string _house007CollidersPath = "Assets/Resources/House_007/House007_Colliders";

    //House 008 Paths
    private string _h8RoofPath = "Assets/Resources/House_008/Roof/House_008_roof_v";
    private string _h8OutsideWallsPath = "Assets/Resources/House_008/OutsideWalls/House_008_OutSideWalls_v";
    private string _h8InsideWallsPath = "Assets/Resources/House_008/InsideWalls/House_008_InSideWalls_v";
    private string _h8FloorPath = "Assets/Resources/House_008/Floor/House_008_Floor1_v";
    private string _h8PillarsPath = "Assets/Resources/House_008/Pillar/House_008_Pillars_v";
    private string _h8DetailsPath = "Assets/Resources/House_008/Details/House_008_Details_v";
    private string _h8WindowsPath = "Assets/Resources/House_008/House_008_WindowGlass";
    private string _h8CollidersPath = "Assets/Resources/House_008/House_008_Colliders";
    private string _h8DoorPath = "Assets/Resources/House_008/Door1/Door_01_v";

    //House 009 Paths
    private string _house009RoofPath = "Assets/Resources/House_009/Roof/House_009_Roofs_v";
    private string _house009OutSideWallsPath = "Assets/Resources/House_009/OutsideWall/House_009_OutSideWalls_v";
    private string _house009InsideWallsPath = "Assets/Resources/House_009/InsideWall/House_009_Insides_v";
    private string _house009PilarsPath = "Assets/Resources/House_009/Pilar/House_009_Pilars_v";
    private string _house009InteriorRoofsPath = "Assets/Resources/House_009/House_009_Inside_Roofs_v";
    private string _house009GlassesPath = "Assets/Resources/House_009/House_009_Glasses_T";
    private string _house009StairsPath = "Assets/Resources/House_009/House_009_Inside_stairs_v1";
    private string _house009F1FloorPath = "Assets/Resources/House_009/F1Floor/House_009_F1_Floor_v";
    private string _house009F2FloorPath = "Assets/Resources/House_009/F2Floor/House_009_F2_Floor_v";
    private string _house009CollidersPath = "Assets/Resources/House_009/House009_Colliders";
    private string _house009Door1Path = "Assets/Resources/House_009/Door1/Door_01_v";
    private string _house009Door2Path = "Assets/Resources/House_009/Door2/Door_02_v";
    private string _house009Door3Path = "Assets/Resources/House_009/Door3/Door_03_v";
    private string _house009Door4Path = "Assets/Resources/House_009/Door4/Door_04_v";


    #endregion

    #region Vars
    #region House007 vars
    private int _maxRoofs7 = 25;
    public int MaxRoofs7 { get { return _maxRoofs7; } }

    private int _maxOutsideWalls7 = 13;
    public int MaxoutSideWalls7 { get { return _maxOutsideWalls7; } }

    private int _maxInsideWalls7 = 10;
    public int MaxInsideWalls7 { get { return _maxInsideWalls7; } }

    private int _maxF1Floors7 = 18;
    public int MaxF1Floors7 { get { return _maxF1Floors7; } }

    private int _maxF2Floors7 = 16;
    public int MaxF2Floors7 { get { return _maxF2Floors7; } }

    private int _maxExtraFloor7 = 18;
    public int MaxExtraFloor7 { get { return _maxExtraFloor7; } }

    private int _maxDoor17 = 6;
    public int MaxDoor17 { get { return _maxDoor17; } }
   
    private int _maxDoor27 = 6;
    public int MaxDoor27 { get { return _maxDoor27; } }
   
    private int _maxDoor37 = 6;
    public int MaxDoor37 { get { return _maxDoor37; } }

    private int _maxDoor47 = 5;
    public int MaxDoor47 { get { return _maxDoor47; } }

    private int _maxAddon17 = 12;
    public int MaxAddon17 { get { return _maxAddon17; } }

    private int _maxAddon27 = 1;
    public int MaxAddon27 { get { return _maxAddon27; } }

    #endregion

    #region House009 vars
    private int _maxRoofs9 = 25;
    public int MaxRoofs9 { get { return _maxRoofs9; } }

    private int _maxOutsideWalls9 = 13;
    public int MaxoutSideWalls9 { get { return _maxOutsideWalls9; } }

    private int _maxInsideWalls9 = 10;
    public int MaxInsideWalls9 { get { return _maxInsideWalls9; } }

    private int _maxF1Floors9 = 16;
    public int MaxF1Floors9 { get { return _maxF1Floors9; } }

    private int _maxF2Floors9 = 16;
    public int MaxF2Floors9 { get { return _maxF2Floors9; } }

    private int _maxDoor19 = 6;
    public int MaxDoor19 { get { return _maxDoor19; } }

    private int _maxDoor29 = 6;
    public int MaxDoor29 { get { return _maxDoor29; } }

    private int _maxDoor39 = 6;
    public int MaxDoor39 { get { return _maxDoor39; } }

    private int _maxDoor49 = 6;
    public int MaxDoor49 { get { return _maxDoor49; } }

    private int _maxPillars9 = 3;
    public int MaxPillars9 { get { return _maxPillars9; } }

    private int _maxAddon19 = 1;
    public int MaxAddon19 { get { return _maxAddon19; } }

    #endregion


    #endregion


    [SerializeField]
    private Custom3DDB house = new Custom3DDB();

    public Custom3DDB House { get { return house; }/* set { house = value; }*/ }

    private void settup()
    {
        house.CurRoof = 1;
        house.CurOutsideWall = 1;
        house.CurInsideWall = 1;
        house.CurF1Floor = 1;
        house.CurF2Floor = 1;
        house.CurDoor1 = 1;
        house.CurDoor2 = 1;
        house.CurDoor3 = 1;
        house.CurDoor4 = 1;        
        
    }
    public void NewHouse004()
    {
        house.CurRoof = 1;
        house.CurOutsideWall = 1;
        house.CurInsideWall = 1;
        house.CurF1Floor = 1;
        house.CurF2Floor = 1;
        house.CurExtraFloor = 1;
        house.CurAddon1 = 1;
        house.CurAddon2 = 1;
        house.CurPillar = 1;
        house.CurDoor1 = 1;
        house.HouseNumber = HouseNumber.House004;
        RoofFor(4);
        OutSideWallsFor(4);
        InsideWallsFor(4);
        F1FloorFor(4);
        F2FloorFor(4);
        ExtraFloor(4);
        Addon1For(4);
        Addon2For(4);
        PillarsFor(4);
        CollidersFor(4);
        WindowsFor(4);
        StarisFor(4);
        Door1For(4);

        gameObject.name = "Custom House004";
       
    }
   // [ContextMenu("New House007")]
    public void NewHouse007()
    {
        settup();
        house.CurExtraFloor = 1;
        house.CurAddon1 = 1;
        house.CurAddon2 = 1;
        house.HouseNumber = HouseNumber.House007;
        BuildierFor2f(7);
        gameObject.name = "Custom House007";
    }

    //[ContextMenu("New House008")]
    public void NewHouse008()
    {
        house.CurRoof = 1;
        house.CurOutsideWall = 1;
        house.CurInsideWall = 1;
        house.CurF1Floor = 1;
        house.CurPillar = 1;
        house.CurAddon1 = 1;
        house.CurDoor1 = 1;
        house.HouseNumber = HouseNumber.House008;
        RoofFor(8);
        OutSideWallsFor(8);
        InsideWallsFor(8);
        F1FloorFor(8);
        PillarsFor(8);
        Addon1For(8);
        CollidersFor(8);
        Door1For(8);
        WindowsFor(8);

        gameObject.name = "Custom House008";
    }

   // [ContextMenu ("New House009")]
    public void NewHouse009()
    {
        settup();
        house.CurPillar = 1;
        house.CurAddon1 = 1;
        house.HouseNumber = HouseNumber.House009;
        BuildierFor2f(9);
        gameObject.name = "Custom House009";
    }

    public void DeleteCurrentHouse()
    {
        house.HouseNumber = HouseNumber.None;

        if (house.Roof != null)
            DestroyImmediate(house.Roof);
        if (house.OutSideWall != null)
            DestroyImmediate(house.OutSideWall);
        if (house.InsideWall != null)
            DestroyImmediate(house.InsideWall);
        if (house.F1Floor != null)
            DestroyImmediate(house.F1Floor);
        if (house.F2Floor != null)
            DestroyImmediate(house.F2Floor);
        if (house.ExtraFloor != null)
            DestroyImmediate(house.ExtraFloor);
        if (house.Stair != null)
            DestroyImmediate(house.Stair);
        if (house.WindowsGlass != null)
            DestroyImmediate(house.WindowsGlass);
        if (house.Door1 != null)
            DestroyImmediate(house.Door1);
        if (house.Door2 != null)
            DestroyImmediate(house.Door2);
        if (house.Door3 != null)
            DestroyImmediate(house.Door3);
        if (house.Door4 != null)
            DestroyImmediate(house.Door4);
        if (house.Pillar != null)
            DestroyImmediate(house.Pillar);
        if (house.Colliders != null)
            DestroyImmediate(house.Colliders);
        if (house.Addon1 != null)
            DestroyImmediate(house.Addon1);
        if (house.Addon2 != null)
            DestroyImmediate(house.Addon2);
        if (house.Addon3 != null)
            DestroyImmediate(house.Addon3);
        if (house.Addon4 != null)
            DestroyImmediate(house.Addon4);
    }
    private void BuildierFor2f(int index)
    {
        RoofFor(index);
        OutSideWallsFor(index);
        InsideWallsFor(index);
        F1FloorFor(index);
        F2FloorFor(index);
        ExtraFloor(index);
        StarisFor(index);
        WindowsFor(index);
        Door1For(index);
        Door2For(index);
        Door3For(index);
        Door4For(index);
        PillarsFor(index);
        CollidersFor(index);
        Addon1For(index);
        Addon2For(index);
    }


    #region Parts Creation
    //Roofs
    private void RoofFor(int numberOfTheHouse)
    {
        if(house.Roof != null)
        {
            DestroyImmediate(house.Roof);
        }
        string pathtoFollow = "";
        switch(numberOfTheHouse)
        {
            case 4:
                pathtoFollow = _h4Roof1Path;
                break;
            case 7:
                pathtoFollow =  _house007RoofPath;
                break;
            case 8:
                pathtoFollow = _h8RoofPath;
                break;
            case 9:
                pathtoFollow = _house009RoofPath;
                break;
        }
        if(pathtoFollow != "")        
            house.Roof = Custom3DDB.NewCustomPart(gameObject, pathtoFollow + house.CurRoof, "Roof v" + house.CurRoof);
        
    }   
    //OutSide Walls
    private void OutSideWallsFor(int HouseNum)
    {
        if (house.OutSideWall != null)
            DestroyImmediate(house.OutSideWall);
        string path = "";
        switch(HouseNum)
        {
            case 4:
                path = _h4OutsideWallsPath;
                break;
            case 7:
                path = _house007OutSideWallsPath;
                break;
            case 8:
                path = _h8OutsideWallsPath;
                break;
            case 9:
                path = _house009OutSideWallsPath;
                break;
        }
        if (path != "")
            house.OutSideWall = Custom3DDB.NewCustomPart(gameObject,path + house.CurOutsideWall, "OutsideWalls v" + house.CurOutsideWall);
        
    }   
    //Inside Walls
    private void InsideWallsFor(int index)
    {
        if (house.InsideWall != null)
            DestroyImmediate(house.InsideWall);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4InsideWallsPath;
                break;
            case 7:
                path = _house007InsideWallsPath;
                break;
            case 8:
                path = _h8InsideWallsPath;
                break;
            case 9:
                path = _house009InsideWallsPath;
                break;
        }
        if (path != "")
            house.InsideWall = Custom3DDB.NewCustomPart(gameObject, path + house.CurInsideWall, "InsideWalls v" + house.CurInsideWall);
    }
    private void F1FloorFor(int index)
    {
        if (house.F1Floor != null)
            DestroyImmediate(house.F1Floor);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4F1FloorPath;
                break;
            case 7:
                path = _House007Floor1Path;
                break;
            case 8:
                path = _h8FloorPath;
                break;
            case 9:
                path = _house009F1FloorPath;
                break;
        }
        if (path != "")
            house.F1Floor = Custom3DDB.NewCustomPart(gameObject, path + house.CurF1Floor, "F1 Floor v" + house.CurF1Floor);
    }
    private void F2FloorFor(int index)
    {
        if (house.F2Floor != null)
            DestroyImmediate(house.F2Floor);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4F2FloorPath;
                break;
            case 7:
                path = _house007Floor2Path;
                break;
            case 9:
                path = _house009F2FloorPath;
                break;
        }
        if (path != "")
            house.F2Floor = Custom3DDB.NewCustomPart(gameObject, path + house.CurF2Floor, "F2 Floor v" + house.CurF2Floor);
    }
    private void ExtraFloor(int index)
    {
        if (house.ExtraFloor != null)
            DestroyImmediate(house.ExtraFloor);
        string path = "";
        switch (index)
        {          
            case 4:
                path = _h4BasementPath;
                break;
            case 7:
                path = _house007Floor3Path;
                break;
        }
        if (path != "")
            house.ExtraFloor = Custom3DDB.NewCustomPart(gameObject, path + house.CurExtraFloor, "Extra Floor v" + house.CurExtraFloor);
    }

    private void StarisFor(int index)
    {
        if (house.Stair != null)
            DestroyImmediate(house.Stair);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4StairsPath;
                break;
            case 7:
                path = _house007StairsPath;
                break;
            case 9:
                path = _house009StairsPath;
                break;
        }
        if (path != "")
            house.Stair = Custom3DDB.NewCustomPart(gameObject, path , "Stars");
    }

    private void WindowsFor(int index)
    {
        if (house.WindowsGlass != null)
            DestroyImmediate(house.WindowsGlass);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4WindowsGlassesPath;
                break;
            case 7:
                path = _house007TransparenGlassPath;
                break;
            case 8:
                path = _h8WindowsPath;
                break;
            case 9:
                path = _house009GlassesPath;
                break;
        }
        if (path != "")
            house.WindowsGlass = Custom3DDB.NewCustomPart(gameObject, path, "Windows Glass");
    }

    private void Door1For(int index)
    {
        if (house.Door1 != null)
            DestroyImmediate(house.Door1);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4DoorPath;
                break;
            case 7:
                path = _House007Door1Path;
                break;
            case 8:
                path = _h8DoorPath;
                break;
            case 9:
                path = _house009Door1Path;
                break;
        }
        if (path != "")
            house.Door1 = Custom3DDB.NewCustomPart(gameObject, path + house.CurDoor1, "Door1 v" + house.CurDoor1);
    }

    private void Door2For(int index)
    {
        if (house.Door2 != null)
            DestroyImmediate(house.Door2);
        string path = "";
        switch (index)
        {
            case 7:
                path = _House007Door2Path;
                break;
            case 9:
                path = _house009Door2Path;
                break;
        }
        if (path != "")
            house.Door2 = Custom3DDB.NewCustomPart(gameObject, path + house.CurDoor2, "Door2 v" + house.CurDoor2);
    }
    private void Door3For(int index)
    {
        if (house.Door2 != null)
            DestroyImmediate(house.Door3);
        string path = "";
        switch (index)
        {
            case 7:
                path = _House007Door3Path;
                break;
            case 9:
                path = _house009Door3Path;
                break;
        }
        if (path != "")
            house.Door3 = Custom3DDB.NewCustomPart(gameObject, path + house.CurDoor3, "Door3 v" + house.CurDoor3);
    }

    private void Door4For(int index)
    {
        if (house.Door4 != null)
            DestroyImmediate(house.Door4);
        string path = "";
        switch (index)
        {
            case 7:
                path = _house007GarageDoorPath;
                break;
            case 9:
                path = _house009Door4Path;
                break;
        }
        if (path != "")
            house.Door4 = Custom3DDB.NewCustomPart(gameObject, path + house.CurDoor4, "Door4 v" + house.CurDoor4);
    }

    private void PillarsFor(int index)
    {
        if (house.Pillar != null)
            DestroyImmediate(house.Pillar);
        string path = "";
        string thename = "";
        switch (index)
        {
            case 4:
                path = _h4Roof2Path;
                thename = "SecondRoof v";
                break;
            case 8:
                path = _h8PillarsPath;
                thename = "Pillars v";
                break;
            case 9:
                path = _house009PilarsPath;
                thename = "Pillars v";
                break;
        }
        if (path != "")
            house.Pillar = Custom3DDB.NewCustomPart(gameObject, path + house.CurPillar,thename + house.CurPillar);
    }

    private void CollidersFor(int index)
    {
        if (house.Colliders != null)
            DestroyImmediate(house.Colliders);
        string path = "";
        switch (index)
        {
            case 4:
                path = _h4CollidersPath;
                break;
            case 7:
                path = _house007CollidersPath;
                break;
            case 8:
                path = _h8CollidersPath;
                break;
            case 9:
                path = _house009CollidersPath;
                break;
        }
        if (path != "")
            house.Colliders = Custom3DDB.NewCustomPart(gameObject, path, "House Colliders");
    }

    private void Addon1For(int index)
    {
        if (house.Addon1 != null)
            DestroyImmediate(house.Addon1);
        string path = "";
        string nameforthePart = "";
        switch (index)
        {
            case 4:
                path = _h4FrontStarisPath;
                nameforthePart = "FrontStairs v";
                break;
            case 7:
                path = _house007GaragePath;
                nameforthePart = "Garage v";
                break;
            case 8:
                path = _h8DetailsPath;
                nameforthePart = "House Deatals v";
                break;
            case 9 :
                path = _house009InteriorRoofsPath;
                nameforthePart = "Interior Roofs v";
                break;
        }
        if (path != "")
            house.Addon1 = Custom3DDB.NewCustomPart(gameObject, path + house.CurAddon1, nameforthePart + house.CurAddon1);
    }
   
    private void Addon2For(int index)
    {
        if (house.Addon2 != null)
            DestroyImmediate(house.Addon2);
        string path = "";
        string nameforthePart = "";
        switch (index)
        {
            case 4:
                path = _h4FirePlacePath;
                nameforthePart = "FirePlace v";
                break;

            case 7:
                path = _house007FramesPath;
                nameforthePart = "frames v";
                break;
        }
        if (path != "")
            house.Addon2 = Custom3DDB.NewCustomPart(gameObject, path + house.CurAddon2, nameforthePart + house.CurAddon2);
    }

    #endregion

    #region Change Parts

    


    public void NRoof()
    {
        if(house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurRoof > house.MaxRoofs14)
                house.CurRoof = house.MaxRoofs14;
            if (house.CurRoof == house.MaxRoofs14)
                house.CurRoof = 1;
            else
                house.CurRoof++;
            RoofFor(4);
        }
        
        if(house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurRoof > MaxRoofs7)
                house.CurRoof = MaxRoofs7;
            if (house.CurRoof == MaxRoofs7)
                house.CurRoof = 1;
            else
                house.CurRoof++;
            RoofFor(7);
        }
        if(house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurRoof > house.MaxRoofs8)
                house.CurRoof = house.MaxRoofs8;
            if (house.CurRoof == house.MaxRoofs8)
                house.CurRoof = 1;
            else
                house.CurRoof++;
            RoofFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurRoof > MaxRoofs9)
                house.CurRoof = MaxRoofs9;
            if (house.CurRoof == MaxRoofs9)
                house.CurRoof = 1;
            else
                house.CurRoof++;
            RoofFor(9);
        }
       
    }
    public void PRoof()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurRoof > house.MaxRoofs14)
                house.CurRoof = house.MaxRoofs14;
            if (house.CurRoof == 1)
                house.CurRoof = house.MaxRoofs14;
            else
                house.CurRoof--;
            RoofFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurRoof > MaxRoofs7)
                house.CurRoof = MaxRoofs7;
            if (house.CurRoof == 1)
                house.CurRoof = MaxRoofs7;
            else
                house.CurRoof--;
            RoofFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurRoof > house.MaxRoofs8)
                house.CurRoof = house.MaxRoofs8;
            if (house.CurRoof == 1)
                house.CurRoof = house.MaxRoofs8;
            else
                house.CurRoof--;
            RoofFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurRoof > MaxRoofs9)
                house.CurRoof = MaxRoofs9;
            if (house.CurRoof == 1)
                house.CurRoof = MaxRoofs9;
            else
                house.CurRoof--;
            RoofFor(9);
        }
    }

    public void NOutSideWalls()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurOutsideWall > house.MaxOutSideWalls4)
                house.CurOutsideWall = house.MaxOutSideWalls4;
            if (house.CurOutsideWall == house.MaxOutSideWalls4)
                house.CurOutsideWall = 1;
            else
                house.CurOutsideWall++;
            OutSideWallsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurOutsideWall > MaxoutSideWalls7)
                house.CurOutsideWall = MaxoutSideWalls7;
            if (house.CurOutsideWall == MaxoutSideWalls7)
                house.CurOutsideWall = 1;
            else
                house.CurOutsideWall++;
            OutSideWallsFor(7);
        }
        if(house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurOutsideWall > house.MaxOutSideWalls8)
                house.CurOutsideWall = house.MaxOutSideWalls8;
            if (house.CurOutsideWall == house.MaxOutSideWalls8)
                house.CurOutsideWall = 1;
            else
                house.CurOutsideWall++;
            OutSideWallsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurOutsideWall > MaxoutSideWalls9)
                house.CurOutsideWall = MaxoutSideWalls9;
            if (house.CurOutsideWall == MaxoutSideWalls9)
                house.CurOutsideWall = 1;
            else
                house.CurOutsideWall++;
            OutSideWallsFor(9);
        }
    }
    public void POutSideWalls()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurOutsideWall > house.MaxOutSideWalls4)
                house.CurOutsideWall = house.MaxOutSideWalls4;
            if (house.CurOutsideWall == 1)
                house.CurOutsideWall = house.MaxOutSideWalls4;
            else
                house.CurOutsideWall--;
            OutSideWallsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurOutsideWall > MaxoutSideWalls7)
                house.CurOutsideWall = MaxoutSideWalls7;
            if (house.CurOutsideWall == 1)
                house.CurOutsideWall = MaxoutSideWalls7;
            else
                house.CurOutsideWall--;
            OutSideWallsFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurOutsideWall > house.MaxOutSideWalls8)
                house.CurOutsideWall = house.MaxOutSideWalls8;
            if (house.CurOutsideWall == 1)
                house.CurOutsideWall = house.MaxOutSideWalls8;
            else
                house.CurOutsideWall--;
            OutSideWallsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurOutsideWall > MaxoutSideWalls9)
                house.CurOutsideWall = MaxoutSideWalls9;
            if (house.CurOutsideWall == 1)
                house.CurOutsideWall = MaxoutSideWalls9;
            else
                house.CurOutsideWall--;
            OutSideWallsFor(9);
        }
    }

    public void NInsideWalls()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurInsideWall > house.MaxInsideWalls4)
                house.CurInsideWall = house.MaxInsideWalls4;
            if (house.CurInsideWall == house.MaxInsideWalls4)
                house.CurInsideWall = 1;
            else
                house.CurInsideWall++;
            InsideWallsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurInsideWall > MaxInsideWalls7)
                house.CurInsideWall = MaxInsideWalls7;
            if (house.CurInsideWall == MaxInsideWalls7)
                house.CurInsideWall = 1;
            else
                house.CurInsideWall++;
            InsideWallsFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurInsideWall > house.MaxInsideWalls8)
                house.CurInsideWall = house.MaxInsideWalls8;
            if (house.CurInsideWall == house.MaxInsideWalls8)
                house.CurInsideWall = 1;
            else
                house.CurInsideWall++;
            InsideWallsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurInsideWall > MaxInsideWalls9)
                house.CurInsideWall = MaxInsideWalls9;
            if (house.CurInsideWall == MaxInsideWalls9)
                house.CurInsideWall = 1;
            else
                house.CurInsideWall++;
            InsideWallsFor(9);
        }
    }
    public void PInsideWalls()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurInsideWall > house.MaxInsideWalls4)
                house.CurInsideWall = house.MaxInsideWalls4;
            if (house.CurInsideWall == 1)
                house.CurInsideWall = house.MaxInsideWalls4;
            else
                house.CurInsideWall--;
            InsideWallsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurInsideWall > MaxInsideWalls7)
                house.CurInsideWall = MaxInsideWalls7;
            if (house.CurInsideWall == 1)
                house.CurInsideWall = MaxInsideWalls7;
            else
                house.CurInsideWall--;
            InsideWallsFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurInsideWall > house.MaxInsideWalls8)
                house.CurInsideWall = house.MaxInsideWalls8;
            if (house.CurInsideWall == 1)
                house.CurInsideWall = house.MaxInsideWalls8;
            else
                house.CurInsideWall--;
            InsideWallsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurInsideWall > MaxInsideWalls9)
                house.CurInsideWall = MaxInsideWalls9;
            if (house.CurInsideWall == 1)
                house.CurInsideWall = MaxInsideWalls9;
            else
                house.CurInsideWall--;
            InsideWallsFor(9);
        }
    }

    public void NF1Floor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurF1Floor > house.MaxFloor14)
                house.CurF1Floor = house.MaxFloor14;
            if (house.CurF1Floor == house.MaxFloor14)
                house.CurF1Floor = 1;
            else
                house.CurF1Floor++;
            F1FloorFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurF1Floor > MaxF1Floors7)
                house.CurF1Floor = MaxF1Floors7;
            if (house.CurF1Floor == MaxF1Floors7)
                house.CurF1Floor = 1;
            else
                house.CurF1Floor++;
            F1FloorFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurF1Floor > house.MaxFloor18)
                house.CurF1Floor = house.MaxFloor18;
            if (house.CurF1Floor == house.MaxFloor18)
                house.CurF1Floor = 1;
            else
                house.CurF1Floor++;
            F1FloorFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurF1Floor > MaxF1Floors9)
                house.CurF1Floor = MaxF1Floors9;
            if (house.CurF1Floor == MaxF1Floors9)
                house.CurF1Floor = 1;
            else
                house.CurF1Floor++;
            F1FloorFor(9);
        }
    }
    public void PF1Floor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurF1Floor > house.MaxFloor14)
                house.CurF1Floor = house.MaxFloor14;
            if (house.CurF1Floor == 1)
                house.CurF1Floor = house.MaxFloor14;
            else
                house.CurF1Floor--;
            F1FloorFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurF1Floor > MaxF1Floors7)
                house.CurF1Floor = MaxF1Floors7;
            if (house.CurF1Floor == 1)
                house.CurF1Floor = MaxF1Floors7;
            else
                house.CurF1Floor--;
            F1FloorFor(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurF1Floor > house.MaxFloor18)
                house.CurF1Floor = house.MaxFloor18;
            if (house.CurF1Floor == 1)
                house.CurF1Floor = house.MaxFloor18;
            else
                house.CurF1Floor--;
            F1FloorFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurF1Floor > MaxF1Floors9)
                house.CurF1Floor = MaxF1Floors9;
            if (house.CurF1Floor == 1)
                house.CurF1Floor = MaxF1Floors9;
            else
                house.CurF1Floor--;
            F1FloorFor(9);
        }
    }
    public void NF2Floor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurF2Floor > house.MaxFloor24)
                house.CurF2Floor = house.MaxFloor24;
            if (house.CurF2Floor == house.MaxFloor24)
                house.CurF2Floor = 1;
            else
                house.CurF2Floor++;
            F2FloorFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurF2Floor > MaxF2Floors7)
                house.CurF2Floor = MaxF2Floors7;
            if (house.CurF2Floor == MaxF2Floors7)
                house.CurF2Floor = 1;
            else
                house.CurF2Floor++;
            F2FloorFor(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurF2Floor > MaxF2Floors9)
                house.CurF2Floor = MaxF2Floors9;
            if (house.CurF2Floor == MaxF2Floors9)
                house.CurF2Floor = 1;
            else
                house.CurF2Floor++;
            F2FloorFor(9);
        }
    }
    public void PF2Floor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurF2Floor > house.MaxFloor24)
                house.CurF2Floor = house.MaxFloor24;
            if (house.CurF2Floor == 1)
                house.CurF2Floor = house.MaxFloor24;
            else
                house.CurF2Floor--;
            F2FloorFor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurF2Floor > MaxF2Floors7)
                house.CurF2Floor = MaxF2Floors7;
            if (house.CurF2Floor == 1)
                house.CurF2Floor = MaxF2Floors7;
            else
                house.CurF2Floor--;
            F2FloorFor(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurF2Floor > MaxF2Floors9)
                house.CurF2Floor = MaxF2Floors9;
            if (house.CurF2Floor == 1)
                house.CurF2Floor = MaxF2Floors9;
            else
                house.CurF2Floor--;
            F2FloorFor(9);
        }
    }
    public void NExtraFloor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurExtraFloor > house.MaxExtraFloors4)
                house.CurExtraFloor = house.MaxExtraFloors4;
            if (house.CurExtraFloor == house.MaxExtraFloors4)
                house.CurExtraFloor = 1;
            else
                house.CurExtraFloor++;
            ExtraFloor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurExtraFloor > MaxExtraFloor7)
                house.CurExtraFloor = MaxExtraFloor7;
            if (house.CurExtraFloor == MaxExtraFloor7)
                house.CurExtraFloor = 1;
            else
                house.CurExtraFloor++;
            ExtraFloor(7);
        }
        
    }
    public void PExtraFloor()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurExtraFloor > house.MaxExtraFloors4)
                house.CurExtraFloor = house.MaxExtraFloors4;
            if (house.CurExtraFloor == 1)
                house.CurExtraFloor = house.MaxExtraFloors4;
            else
                house.CurExtraFloor--;
            ExtraFloor(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurExtraFloor > MaxExtraFloor7)
                house.CurExtraFloor = MaxExtraFloor7;
            if (house.CurExtraFloor == 1)
                house.CurExtraFloor = MaxExtraFloor7;
            else
                house.CurExtraFloor--;
            ExtraFloor(7);
        }
    }

    public void NDoor1()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurDoor1 > house.MaxDoors4)
                house.CurDoor1 = house.MaxDoors4;
            if (house.CurDoor1 == house.MaxDoors4)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
            Door1For(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor1 > MaxDoor17)
                house.CurDoor1 = MaxDoor17;
            if (house.CurDoor1 == MaxDoor17)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
            Door1For(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurDoor1 > house.MaxDoors8)
                house.CurDoor1 = house.MaxDoors8;
            if (house.CurDoor1 == house.MaxDoors8)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
            Door1For(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor1 > MaxDoor19)
                house.CurDoor1 = MaxDoor19;
            if (house.CurDoor1 == MaxDoor19)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
            Door1For(9);
        }
    }
    public void PDoor1()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurDoor1 > house.MaxDoors4)
                house.CurDoor1 = house.MaxDoors4;
            if (house.CurDoor1 == 1)
                house.CurDoor1 = house.MaxDoors4;
            else
                house.CurDoor1--;
            Door1For(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor1 > MaxDoor17)
                house.CurDoor1 = MaxDoor17;
            if (house.CurDoor1 == 1)
                house.CurDoor1 = MaxDoor17;
            else
                house.CurDoor1--;
            Door1For(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurDoor1 > house.MaxDoors8)
                house.CurDoor1 = house.MaxDoors8;
            if (house.CurDoor1 == 1)
                house.CurDoor1 = house.MaxDoors8;
            else
                house.CurDoor1--;
            Door1For(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor1 > MaxDoor19)
                house.CurDoor1 = MaxDoor19;
            if (house.CurDoor1 == 1)
                house.CurDoor1 = MaxDoor19;
            else
                house.CurDoor1--;
            Door1For(9);
        }
    }

    public void NDoor2()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor2 > MaxDoor27)
                house.CurDoor2 = MaxDoor27;
            if (house.CurDoor2 == MaxDoor27)
                house.CurDoor2 = 1;
            else
                house.CurDoor2++;
            Door2For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor2 > MaxDoor29)
                house.CurDoor2 = MaxDoor29;
            if (house.CurDoor2 == MaxDoor29)
                house.CurDoor2 = 1;
            else
                house.CurDoor2++;
            Door2For(9);
        }
    }
    public void PDoor2()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor2 > MaxDoor27)
                house.CurDoor2 = MaxDoor27;
            if (house.CurDoor2 == 1)
                house.CurDoor2 = MaxDoor27;
            else
                house.CurDoor2--;
            Door2For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor2 > MaxDoor29)
                house.CurDoor2 = MaxDoor29;
            if (house.CurDoor2 == 1)
                house.CurDoor2 = MaxDoor29;
            else
                house.CurDoor2--;
            Door2For(9);
        }
    }

    public void NDoor3()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor3 > MaxDoor37)
                house.CurDoor3 = MaxDoor37;
            if (house.CurDoor3 == MaxDoor37)
                house.CurDoor3 = 1;
            else
                house.CurDoor3++;
            Door3For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor3 > MaxDoor39)
                house.CurDoor3 = MaxDoor39;
            if (house.CurDoor3 == MaxDoor39)
                house.CurDoor3 = 1;
            else
                house.CurDoor3++;
            Door3For(9);
        }
    }
    public void PDoor3()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor3 > MaxDoor37)
                house.CurDoor3 = MaxDoor37;
            if (house.CurDoor3 == 1)
                house.CurDoor3 = MaxDoor37;
            else
                house.CurDoor3--;
            Door3For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor3 > MaxDoor39)
                house.CurDoor3 = MaxDoor39;
            if (house.CurDoor3 == 1)
                house.CurDoor3 = MaxDoor39;
            else
                house.CurDoor3--;
            Door3For(9);
        }
    }
    public void NDoor4()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor4 > MaxDoor47)
                house.CurDoor4 = MaxDoor47;
            if (house.CurDoor4 == MaxDoor47)
                house.CurDoor4 = 1;
            else
                house.CurDoor4++;
            Door4For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor4 > MaxDoor49)
                house.CurDoor4 = MaxDoor49;
            if (house.CurDoor4 == MaxDoor49)
                house.CurDoor4 = 1;
            else
                house.CurDoor4++;
            Door4For(9);
        }
    }
    public void PDoor4()
    {
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurDoor4 > MaxDoor47)
                house.CurDoor4 = MaxDoor47;
            if (house.CurDoor4 == 1)
                house.CurDoor4 = MaxDoor47;
            else
                house.CurDoor4--;
            Door4For(7);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurDoor4 > MaxDoor49)
                house.CurDoor4 = MaxDoor49;
            if (house.CurDoor4 == 1)
                house.CurDoor4 = MaxDoor49;
            else
                house.CurDoor4--;
            Door4For(9);
        }
    }

    public void NPillars()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurPillar > house.MaxRoofs24)
                house.CurPillar = house.MaxRoofs24;
            if (house.CurPillar == house.MaxRoofs24)
                house.CurPillar = 1;
            else
                house.CurPillar++;
            PillarsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurPillar > house.MaxPillars8)
                house.CurPillar = house.MaxPillars8;
            if (house.CurPillar == house.MaxPillars8)
                house.CurPillar = 1;
            else
                house.CurPillar++;
            PillarsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurPillar > MaxPillars9)
                house.CurPillar = MaxPillars9;
            if (house.CurPillar == MaxPillars9)
                house.CurPillar = 1;
            else
                house.CurPillar++;
            PillarsFor(9);
        }
    }
    public void PPillars()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurPillar > house.MaxRoofs24)
                house.CurPillar = house.MaxRoofs24;
            if (house.CurPillar == 1)
                house.CurPillar = house.MaxRoofs24;
            else
                house.CurPillar--;
            PillarsFor(4);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurPillar > house.MaxPillars8)
                house.CurPillar = house.MaxPillars8;
            if (house.CurPillar == 1)
                house.CurPillar = house.MaxPillars8;
            else
                house.CurPillar--;
            PillarsFor(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurPillar > MaxPillars9)
                house.CurPillar = MaxPillars9;
            if (house.CurPillar == 1)
                house.CurPillar = MaxPillars9;
            else
                house.CurPillar--;
            PillarsFor(9);
        }
    }

    public void NAddon1()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurAddon1 > house.MaxFrontStairs4)
                house.CurAddon1 = house.MaxFrontStairs4;
            if (house.CurAddon1 == house.MaxFrontStairs4)
                house.CurAddon1 = 1;
            else
                house.CurAddon1++;
            Addon1For(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurAddon1 > MaxAddon17)
                house.CurAddon1 = MaxAddon17;
            if (house.CurAddon1 == MaxAddon17)
                house.CurAddon1 = 1;
            else
                house.CurAddon1++;
            Addon1For(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurAddon1 > house.MaxDetails8)
                house.CurAddon1 = house.MaxDetails8;
            if (house.CurAddon1 == house.MaxDetails8)
                house.CurAddon1 = 1;
            else
                house.CurAddon1++;
            Addon1For(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurAddon1 > MaxAddon19)
                house.CurAddon1 = MaxAddon19;
            if (house.CurAddon1 == MaxAddon19)
                house.CurAddon1 = 1;
            else
                house.CurAddon1++;
            Addon1For(9);
        }
    }
    public void PAddon1()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurAddon1 > house.MaxFrontStairs4)
                house.CurAddon1 = house.MaxFrontStairs4;
            if (house.CurAddon1 == 1)
                house.CurAddon1 = house.MaxFrontStairs4;
            else
                house.CurAddon1--;
            Addon1For(4);
        }
        
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurAddon1 > MaxAddon17)
                house.CurAddon1 = MaxAddon17;
            if (house.CurAddon1 == 1)
                house.CurAddon1 = MaxAddon17;
            else
                house.CurAddon1--;
            Addon1For(7);
        }
        if (house.HouseNumber == HouseNumber.House008)
        {
            if (house.CurAddon1 > house.MaxDetails8)
                house.CurAddon1 = house.MaxDetails8;
            if (house.CurAddon1 == 1)
                house.CurAddon1 = house.MaxDetails8;
            else
                house.CurAddon1--;
            Addon1For(8);
        }
        if (house.HouseNumber == HouseNumber.House009)
        {
            if (house.CurAddon1 > MaxAddon19)
                house.CurAddon1 = MaxAddon19;
            if (house.CurAddon1 == 1)
                house.CurAddon1 = MaxAddon19;
            else
                house.CurAddon1--;
            Addon1For(9);
        }
    }

    public void NAddon2()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurAddon2 > house.MaxFirePlace4)
                house.CurAddon2 = house.MaxFirePlace4;
            if (house.CurAddon2 == house.MaxFirePlace4)
                house.CurAddon2 = 1;
            else
                house.CurAddon2++;
            Addon2For(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurAddon2 > MaxAddon27)
                house.CurAddon2 = MaxAddon27;
            if (house.CurAddon2 == MaxAddon27)
                house.CurAddon2 = 1;
            else
                house.CurAddon2++;
            Addon2For(7);
        }
    }
    public void PAddon2()
    {
        if (house.HouseNumber == HouseNumber.House004)
        {
            if (house.CurAddon2 > house.MaxFirePlace4)
                house.CurAddon2 = house.MaxFirePlace4;
            if (house.CurAddon2 == 1)
                house.CurAddon2 = house.MaxFirePlace4;
            else
                house.CurAddon2--;
            Addon2For(4);
        }
        if (house.HouseNumber == HouseNumber.House007)
        {
            if (house.CurAddon2 > MaxAddon27)
                house.CurAddon2 = MaxAddon27;
            if (house.CurAddon2 == 1)
                house.CurAddon2 = MaxAddon27;
            else
                house.CurAddon2--;
            Addon2For(7);
        }
    }

    #endregion

}
