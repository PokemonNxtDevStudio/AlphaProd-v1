using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

#if UNITY_EDITOR
public class Custom3DHouses : MonoBehaviour
{
    
    #region Paths
    private const string _foldersPath = "Assets/Resources/Prefabs/Environment/Buildings/";
    #region House 3 Paths
    //House003 Paths
    private string _h3RoofPath = _foldersPath + "House_003/Roof/House_003_Roof_";
    private string _h3InsideRoofPath = _foldersPath + "House_003/InsideRoof/House_003_InteriorRoof_";
    private string _h3OutsideWallsPath = _foldersPath + "House_003/OutsideWalls/House_003_OutsideWalls_";
    private string _h3InsideWallsPath = _foldersPath + "House_003/InsideWalls/House_003_InsideWalls_";
    private string _h3F1FloorPath = _foldersPath + "House_003/F1Floor/House_003_F1Floor_v";
    private string _h3F2FloorPath = _foldersPath + "House_003/F2Floor/House_003_F2Floor_v";
    private string _h3FirePlacePath = _foldersPath + "House_003/Fireplace/House_003_FirePlace_";
    private string _h3StairsPath = _foldersPath + "House_003/House_003_Stairs_v1";
    private string _h3CollidersPath = _foldersPath + "House_003/House_003_Colliders";
    private string _h3WindowsGlassesPath = _foldersPath + "House_003/House_003_WindowsGlass";
    private string _h3DoorPath = _foldersPath + "House_003/Door/House_003_Door_";
    #endregion
    #region House 4 Paths
    //House004 Paths 
    private string _h4Roof1Path = _foldersPath + "House_004/Roof1/House_004_Roof_1_v";
    private string _h4Roof2Path = _foldersPath + "House_004/Roof2/House_004_Roof_2_v";
    private string _h4OutsideWallsPath = _foldersPath + "House_004/OutsideWalls/House_004_OutSideWalls_v";
    private string _h4InsideWallsPath = _foldersPath + "House_004/InsideWalls/house_004_InsideWalls_v";
    private string _h4F1FloorPath = _foldersPath + "House_004/F1Floor/House_004_F1Floor_v";
    private string _h4F2FloorPath = _foldersPath + "House_004/F2Floor/House_004_F2Floor_v";
    private string _h4FrontStarisPath = _foldersPath + "House_004/Detail/House_004_Detail_v";
    private string _h4FirePlacePath = _foldersPath + "House_004/Detail/House_004_Detail_02_v";
    private string _h4StairsPath = _foldersPath + "House_004/House_004_Stairs_v1";
    private string _h4CollidersPath = _foldersPath + "House_004/House_004_Colliders";
    private string _h4BasementPath = _foldersPath + "House_004/Basement/House_004_Basemant_v";
    private string _h4WindowsGlassesPath = _foldersPath + "House_004/House_004_WindosGlases";
    private string _h4DoorPath = _foldersPath + "House_004/Door/House_004_Door_v";
    #endregion

    #region House 5 Paths
    //House005 Paths 
    private string _h5RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/Roof/House_005_Roof_";
    private string _h5OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/OutsideWalls/House_005_OutSideWalls_";
    private string _h5InsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/InsideWalls/House_005_InsideWalls_";
    private string _h5F1FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/F1Floor/House_005_F1Floor_";
    private string _h5F2FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/F2Floor/House_005_F2Floor_";
    private string _h5StairsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/House_005_Stairs";
    private string _h5WindowsGlassesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/House_005_WindowsGlass";
    private string _h5DoorPath = "Assets/Resources/House_005/Prefabs/Environment/Buildings/Door/House_005_Door_v";
    private string _h5InsideExtraWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/F2Walls/House_005_F2InsideWalls_";
    private string _h5PillarsPath = "Assets/Resources/House_005/Prefabs/Environment/Buildings/Pillar/House_005_Pillars_";
    private string _h5InsideRoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_005/InsideRoof/House_005_InsideRoof_";
    #endregion

    #region House 6 Paths
    //House006 Paths 
    private string _h6RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/Roof/House_006_Roof_";
    private string _h6OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/OutsideWalls/House_006_OutSideWalls_";
    private string _h6InsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/InsideWalls/House_006_InsideWalls_";
    private string _h6F1FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/F1Floor/House_006_F1Floor_";
    private string _h6F2FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/F2Floor/House_006_F2Floor_";
    private string _h6StairsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/House_006_Stairs";
    private string _h6WindowsGlassesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/House_006_WindowsGlass";
    private string _h6DoorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/Door/House_006_Door_v";
    private string _h6FramesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/Frame/House_006_Frames_";
    private string _h6FrontStairsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/FrontStairs/House_006_FrontStairs_";
    private string _h6InsideRoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_006/InteriorRoof/House_006_InsideRoof_";
    #endregion

    #region House 7 Paths
    //House007 Paths
    private string _h7Door1Path = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Door/Door_3_v";
    private string _h7Door2Path = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Door/Door_2_v";
    private string _h7Door3Path = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Door/Door_1_v";
    private string _h7F1FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Floor1/House_007_Floor_01_v";
    private string _h7F2FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Floor3/House_007_Floor_03_v";
    private string _h7ExtraFloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Floor2/House_007_Floor_02_v";
    private string _h7GaragePath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Garage/House_007_Garage_v";
    private string _h7GarageDoorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/GarageDoor/GarashDoor_v";
    private string _h7InsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/InsideWalls/House_007_InsideWalls_v";
    private string _h7OutSideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/OutsideWalls/House_007_OutsideWalls_v";
    private string _h7RoofsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Roof/House_007_Roof_V";
    private string _h7FramesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/House_007_Frames_v";
    private string _h7StairsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/House_007_Inside";
    private string _h7WindowsGlassPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/Windows_T";
    private string _h7CollidersPath = "Assets/Resources/Prefabs/Environment/Buildings/House_007/House007_Colliders";
    #endregion
    #region House 8 Paths
    //House 008 Paths
    private string _h8RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/Roof/House_008_roof_v";
    private string _h8OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/OutsideWalls/House_008_OutSideWalls_v";
    private string _h8InsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/InsideWalls/House_008_InSideWalls_v";
    private string _h8FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/Floor/House_008_Floor1_v";
    private string _h8PillarsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/Pillar/House_008_Pillars_v";
    private string _h8FramesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/Details/House_008_Details_v";
    private string _h8WindowsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/House_008_WindowGlass";
    private string _h8CollidersPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/House_008_Colliders";
    private string _h8DoorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_008/Door1/Door_01_v";
    #endregion
    #region House 9 Paths
    //House 009 Paths
    private string _h9RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Roof/House_009_Roofs_v";
    private string _h9OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/OutsideWall/House_009_OutSideWalls_v";
    private string _h9InsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/InsideWall/House_009_Insides_v";
    private string _h9PillarsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Pilar/House_009_Pilars_v";
    private string _h9InsideRoofsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/House_009_Inside_Roofs_v";
    private string _h9WindowGlassesPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/House_009_Glasses_T";
    private string _h9StairsPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/House_009_Inside_stairs_v1";
    private string _h9F1FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/F1Floor/House_009_F1_Floor_v";
    private string _h9F2FloorPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/F2Floor/House_009_F2_Floor_v";
    private string _h9CollidersPath = "Assets/Resources/Prefabs/Environment/Buildings/House_009/House009_Colliders";
    private string _h9Door1Path = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Door1/Door_01_v";
    private string _h9Door2Path = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Door2/Door_02_v";
    private string _h9Door3Path = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Door3/Door_03_v";
    private string _h9Door4Path = "Assets/Resources/Prefabs/Environment/Buildings/House_009/Door4/Door_04_v";
    #endregion

    #region LongHouse Paths 
    private string _LBaseL1Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Left/1/Base_L_1_";
    private string _LBaseL1FramePath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Left/1F/Frame_L_1_";

    private string _LBaseM1Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Middle/1/Base_M_1_";
    private string _LBaseM1FramePath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Middle/1F/Frame_M_1_";
    private string _LBaseM2Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Middle/2/Base_M_2_";
    private string _LBaseM2FramePath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Middle/2F/Frame_M_2_";

    private string _LBaseR1Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Right/1/Base_R_1_";
    private string _LBaseR1FramePath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Base/Right/1F/Frame_R_";

    private string _LDoorRPath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Door/Base_R_Door_";
    private string _LDoorMPath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Door/Base_M_Door_";
    private string _LDoorM2Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Door/Base_M2_Door_";
    private string _LDoorLPath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Door/Base_L_Door_";

    private string _LF21Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/F2/1/F2_1_";

    private string _LRoof1Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Roof/1/Roof_1_";
    private string _LRoof1PartPath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Roof/1P/Roof_P_1_";
    private string _LRoof2Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Roof/2/Roof_2_";
    private string _LRoof3Path = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Roof/3/Roof_3_";
    private string _LRoof3PartPath = "Assets/Resources/Prefabs/Environment/Buildings/LongHouse/Roof/3P/Roof_P_3_";


    #endregion

    #region WideHouse1 Paths
    private string _W1Addon1Path = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/Addon_1/Addon_1_";
    private string _W1Door1Path = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/Door/Door_1_";
    private string _W1DoorFramePath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/DoorFrame/FrameDoor_1_";
    private string _W1OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/OutsideWalls/OutsideWalls_1_";
    private string _W1RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/Roof/Roof_1_";
    private string _W1WindowFrame = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/WindowFrame/FramesWindows_1_";

    #endregion

    #region WideHouse2 Paths
    private string _W2Door1Path = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/Door/Door_1_";
    private string _W2DoorFramePath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse/DoorFrame/FrameDoor_1_";
    private string _W2OutsideWallsPath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse2/OutsideWalls/OutsideWalls_2_";
    private string _W2RoofPath = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse2/Roof/Roof_2_";
    private string _W2WindowFrame = "Assets/Resources/Prefabs/Environment/Buildings/WideHouse2/WindowsFrame/WindowFrames_2_";

    #endregion
    #endregion

    [SerializeField]
    private Custom3DDB house = new Custom3DDB();
    public Custom3DDB House { get { return house; }/* set { house = value; }*/ }
    public void DeleteCurrentHouse()
    {
        house.HouseNumber = HouseNumber.None;
        house.HouseFiller = HouseFiller.None;

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
        if (house.InsideRoof != null)
            DestroyImmediate(house.InsideRoof);
        if (house.FirePlace != null)
            DestroyImmediate(house.FirePlace);
        if (house.Colliders != null)
            DestroyImmediate(house.Colliders);
        if (house.Basement != null)
            DestroyImmediate(house.Basement);
        if (house.FrontStairs != null)
            DestroyImmediate(house.FrontStairs);
        if (house.ExtraRoof != null)
            DestroyImmediate(house.ExtraRoof);
        if (house.Garage != null)
            DestroyImmediate(house.Garage);
        if (house.GarageDoor != null)
            DestroyImmediate(house.GarageDoor);
        if (house.Frames != null)
            DestroyImmediate(house.Frames);
        if (house.InsideExtraWalls != null)
            DestroyImmediate(house.InsideExtraWalls);
        if (house.WindowFrames != null)
            DestroyImmediate(house.WindowFrames);

        if (house.Addon1 != null)
            DestroyImmediate(house.Addon1);
        if (house.Addon2 != null)
            DestroyImmediate(house.Addon2);
        if (house.Addon3 != null)
            DestroyImmediate(house.Addon3);
        if (house.Addon4 != null)
            DestroyImmediate(house.Addon4);
        if (house.Base != null)
            DestroyImmediate(house.Base);
        if (house.F2 != null)
            DestroyImmediate(house.F2);
        if (house.RoofPart != null)
            DestroyImmediate(house.RoofPart);


    }  
    public void HouseBuilder(int num)
    {
        #region Vars Part
        string roof = "";
        string extraroof = "";
        string outsidewall = "";
        string insidewall = "";
        string insideroof = "";
        string insideextrawalls = "";
        string f1floor = "";
        string f2floor = "";
        string frontstairs = "";
        string extrafloor = "";
        string windowglass = "";
        string thecollider = "";
        string stairs = "";
        string pillar = "";
        string basement = "";
        string fireplace = "";
        string door1 = "";
        string door2 = "";
        string door3 = "";
        string door4 = "";
        string garagedoor = "";
        string garage = "";
        string frames = "";
        string windowframes = "";
        string addon1 = "";
        string addon2 = "";
        string addon3 = "";
        string addon4 = "";
        string addon5 = "";
        string addon6 = "";
        string addon7 = "";
       
        string nameforthehouse ="";
        #endregion
        #region AddHouse and set values for Atms
        switch (num)
        {
            case 3:
                roof = _h3RoofPath;
                outsidewall = _h3OutsideWallsPath;
                insidewall = _h3InsideWallsPath;
                insideroof = _h3InsideRoofPath;
                f1floor = _h3F1FloorPath;
                f2floor = _h3F2FloorPath;
                windowglass = _h3WindowsGlassesPath;
                stairs = _h3StairsPath;
                fireplace = _h3FirePlacePath;
                thecollider = _h3CollidersPath;
                door1 = _h3DoorPath;
                //set Atm values
                house.MaxRoofAtm = house.MaxRoofs3;
                house.MaxOutSideWallsAtm = house.MaxOutSideWalls3;
                house.MaxInsideWallsAtm = house.MaxInsideWalls3;
                house.MaxInsideRoofAtm = house.MaxInsideRoofs3;
                house.MaxFirePlaceAtm = house.MaxFirePlaces3;
                house.MaxF1FloorAtm = house.MaxFloor13;
                house.MaxF2FloorAtm = house.MaxFloor23;
                house.MaxDoor1Atm = house.MaxDoors3;
                //name gameobject
                nameforthehouse = "House 003 Custom";
                house.HouseNumber = HouseNumber.House003;
                break;
            case 4:
                roof = _h4Roof1Path;
                extraroof = _h4Roof2Path;
                outsidewall = _h4OutsideWallsPath;
                insidewall = _h4InsideWallsPath;
                f1floor = _h4F1FloorPath;
                f2floor = _h4F2FloorPath;
                frontstairs = _h4FrontStarisPath;
                fireplace = _h4FirePlacePath;
                stairs = _h4StairsPath;
                thecollider = _h4CollidersPath;
                basement = _h4BasementPath;
                windowglass = _h4WindowsGlassesPath;
                door1 = _h4DoorPath;
                nameforthehouse = "House 004 Custom";
                house.HouseNumber = HouseNumber.House004;
                //set Atm values
                house.MaxRoofAtm = house.MaxRoofs4;
                house.MaxExtraRoofAtm = house.MaxExtraRoof4;
                house.MaxOutSideWallsAtm = house.MaxOutSideWalls4;
                house.MaxInsideWallsAtm = house.MaxInsideWalls4;
                house.MaxF1FloorAtm = house.MaxFloor14;
                house.MaxF2FloorAtm = house.MaxFloor24;
                house.MaxFirePlaceAtm = house.MaxFirePlace4;
                house.MaxFrontStairsAtm = house.MaxFrontStairs4;
                house.MaxBasementAtm = house.MaxBasements4;
                house.MaxDoor1Atm = house.MaxDoors4;
                break;
            case 5:
                roof = _h5RoofPath;
                outsidewall = _h5OutsideWallsPath;
                insidewall = _h5InsideWallsPath;
                insideroof = _h5InsideRoofPath;
                insideextrawalls = _h5InsideExtraWallsPath;
                f1floor = _h5F1FloorPath;
                f2floor = _h5F2FloorPath;
                stairs = _h5StairsPath;
                pillar = _h5PillarsPath;
                door1 = _h5DoorPath;
                windowglass = _h5WindowsGlassesPath;
                nameforthehouse = "House 005 Custom";
                house.HouseNumber = HouseNumber.House005;
                //Set Atm values
                house.MaxRoofAtm = house.MaxRoofs5;
                house.MaxOutSideWallsAtm = house.MaxOutsideWalls5;
                house.MaxInsideExtraWallsAtm = house.MaxInsideExtraWalls5;
                house.MaxInsideWallsAtm = house.MaxInsideWalls5;
                house.MaxInsideRoofAtm = house.MaxInsideRoofs5;
                house.MaxF1FloorAtm = house.MaxF1Floor5;
                house.MaxF2FloorAtm = house.MaxF2Floor5;
                house.MaxPillarsAtm = house.MaxPillars5;
                house.MaxDoor1Atm = house.MaxDoors5;
                break;
            case 6:
                roof = _h6RoofPath;
                outsidewall = _h6OutsideWallsPath;
                insidewall = _h6InsideWallsPath;
                insideroof = _h6InsideRoofPath;
                f1floor = _h6F1FloorPath;
                f2floor = _h6F2FloorPath;
                stairs = _h6StairsPath;
                door1 = _h6DoorPath;
                frames = _h6FramesPath;
                frontstairs = _h6FrontStairsPath;
                windowglass = _h6WindowsGlassesPath;
                nameforthehouse = "House 006 Custom";
                house.HouseNumber = HouseNumber.House006;
                house.MaxRoofAtm = house.MaxRoofs6;
                house.MaxOutSideWallsAtm = house.MaxOutsideWalls6;
                house.MaxInsideWallsAtm = house.MaxInsideWalls6;
                house.MaxInsideRoofAtm = house.MaxInsideRoofs6;
                house.MaxF1FloorAtm = house.MaxF1Floor6;
                house.MaxF2FloorAtm = house.MaxF2Floor6;
                house.MaxDoor1Atm = house.MaxDoors6;
                house.MaxFramesAtm = house.MaxFrames6;
                house.MaxFrontStairsAtm = house.MaxFrontStairs6;
                break;
            case 7:
                roof = _h7RoofsPath;
                outsidewall = _h7OutSideWallsPath;
                insidewall = _h7InsideWallsPath;
                f1floor = _h7F1FloorPath;
                f2floor = _h7F2FloorPath;
                extrafloor = _h7ExtraFloorPath;
                door1 = _h7Door1Path;
                door2 = _h7Door2Path;
                door3 = _h7Door3Path;
                garagedoor = _h7GarageDoorPath;
                garage = _h7GaragePath;
                frames = _h7FramesPath;
                stairs = _h7StairsPath;
                windowglass = _h7WindowsGlassPath;
                thecollider = _h7CollidersPath;
                nameforthehouse = "House 007 Custom";
                house.HouseNumber = HouseNumber.House007;
                //set Atm values
                house.MaxRoofAtm = house.MaxRoofs7;
                house.MaxOutSideWallsAtm = house.MaxOutsideWalls7;
                house.MaxInsideWallsAtm = house.MaxInsideWalls7;
                house.MaxF1FloorAtm = house.MaxF1Floors7;
                house.MaxF2FloorAtm = house.MaxF2Floors7;
                house.MaxExtraFloorAtm = house.MaxExtraFloor7;
                house.MaxDoor1Atm = house.MaxDoor17;
                house.MaxDoor2Atm = house.MaxDoor27;
                house.MaxDoor3Atm = house.MaxDoor37;
                house.MaxGarageDoorAtm = house.MaxGarageDoor7;
                house.MaxGarageAtm = house.MaxGarage7;
                house.MaxFramesAtm = house.MaxFrames7;
                break;
            case 8:
                roof = _h8RoofPath;
                outsidewall = _h8OutsideWallsPath;
                insidewall = _h8InsideWallsPath;
                f1floor = _h8FloorPath;
                pillar = _h8PillarsPath;
                frames = _h8FramesPath;
                windowglass = _h8WindowsPath;
                thecollider = _h8CollidersPath;
                door1 = _h8DoorPath;
                nameforthehouse = "House 008 Custom";
                house.HouseNumber = HouseNumber.House008;
                //Set Atm values
                house.MaxRoofAtm = house.MaxRoofs8;
                house.MaxOutSideWallsAtm = house.MaxOutSideWalls8;
                house.MaxInsideWallsAtm = house.MaxInsideWalls8;
                house.MaxF1FloorAtm = house.MaxFloor18;
                house.MaxFramesAtm = house.MaxFrames8;
                house.MaxPillarsAtm = house.MaxPillars8;
                house.MaxDoor1Atm = house.MaxDoors8;
                break;
            case 9:
                roof = _h9RoofPath;
                outsidewall = _h9OutsideWallsPath;
                insidewall = _h9InsideWallsPath;
                insideroof = _h9InsideRoofsPath;
                f1floor = _h9F1FloorPath;
                f2floor = _h9F2FloorPath;
                pillar = _h9PillarsPath;
                windowglass = _h9WindowGlassesPath;
                stairs = _h9StairsPath;
                thecollider = _h9CollidersPath;
                door1 = _h9Door1Path;
                door2 = _h9Door2Path;
                door3 = _h9Door3Path;
                door4 = _h9Door4Path;
                nameforthehouse = "House 009 Custom";
                house.HouseNumber = HouseNumber.House009;
                //Set Atm values
                house.MaxRoofAtm = house.MaxRoofs9;
                house.MaxOutSideWallsAtm = house.MaxOutsideWalls9;
                house.MaxInsideRoofAtm = house.MaxInsideRoofs9;
                house.MaxInsideWallsAtm = house.MaxInsideWalls9;
                house.MaxF1FloorAtm = house.MaxF1Floors9;
                house.MaxF2FloorAtm = house.MaxF2Floors9;
                house.MaxDoor1Atm = house.MaxDoor19;
                house.MaxDoor2Atm = house.MaxDoor29;
                house.MaxDoor3Atm = house.MaxDoor39;
                house.MaxDoor4Atm = house.MaxDoor49;
                house.MaxPillarsAtm = house.MaxPillars9;                           
                break;

            case 101:
                roof = _W1RoofPath;
                outsidewall = _W1OutsideWallsPath;
                door1 = _W1Door1Path;
                frames = _W1DoorFramePath;
                windowframes = _W1WindowFrame;
                addon1 = _W1Addon1Path;
                nameforthehouse = "Wide House Custom";
                house.HouseNumber = HouseNumber.House101;
                //Set Atm values
                house.MaxRoofAtm = house.MaxRoofsW1;
                house.MaxOutSideWallsAtm = house.MaxOutsideWallsW1;
                house.MaxDoor1Atm = house.MaxDoor1W1;
                house.MaxFramesAtm = house.MaxDoorFrameW1;
                house.MaxWindowFramesAtm = house.MaxWindowsFrameW1;
                house.MaxAddon1Atm = house.MaxAddon1W1;
                break;
            case 102:
                roof = _W2RoofPath;
                outsidewall = _W2OutsideWallsPath;
                door1 = _W2Door1Path;
                frames = _W2DoorFramePath;
                windowframes = _W2WindowFrame;
                nameforthehouse = "Wide House Custom 2";
                house.HouseNumber = HouseNumber.House102;
                //Set Atm values
                house.MaxRoofAtm = house.MaxRoofsW2;
                house.MaxOutSideWallsAtm = house.MaxOutsideWallsW2;
                house.MaxDoor1Atm = house.MaxDoor1W1;
                house.MaxFramesAtm = house.MaxDoorFrameW1;
                house.MaxWindowFramesAtm = house.MaxWindowFramesW2;
                break;

        }
        #endregion
        #region Creation Part
        if (roof != "")
        {
            if (house.Roof != null)
                DestroyImmediate(house.Roof);
            if (house.CurRoof > house.MaxRoofAtm)
                house.CurRoof = house.MaxRoofAtm;
            house.Roof = Custom3DDB.NewCustomPart(gameObject, roof + house.CurRoof,nameforthehouse + " Roof v" + house.CurRoof);
        }
        if(extraroof != "")
        {
            if (house.ExtraRoof != null)
                DestroyImmediate(house.ExtraRoof);
            if (house.CurExtraRoof > house.MaxExtraFloorAtm)
                house.CurExtraRoof = house.MaxExtraFloorAtm;
            house.ExtraRoof = Custom3DDB.NewCustomPart(gameObject, extraroof + house.CurExtraRoof, nameforthehouse + " Extra Roof v" + house.CurExtraRoof);
        }
        if (outsidewall != "")
        {
            if (house.OutSideWall != null)
                DestroyImmediate(house.OutSideWall);
            if (house.CurOutsideWall > house.MaxOutSideWallsAtm)
                house.CurOutsideWall = house.MaxOutSideWallsAtm;
            house.OutSideWall = Custom3DDB.NewCustomPart(gameObject, outsidewall + house.CurOutsideWall, nameforthehouse + " OutSideWalls v" + house.CurOutsideWall);
        }
        if (frames != "")
        {
            if (house.Frames != null)
                DestroyImmediate(house.Frames);
            if (house.CurFrames > house.MaxFramesAtm)
                house.CurFrames = house.MaxFramesAtm;
            house.Frames = Custom3DDB.NewCustomPart(gameObject, frames + house.CurFrames, nameforthehouse + " Frames v" + house.CurFrames);
        }
        if(windowframes != null)
        {
            if (house.WindowFrames != null)
                DestroyImmediate(house.WindowFrames);
            if (house.CurWindowFrames > house.MaxWindowFramesAtm)
                house.CurWindowFrames = house.MaxWindowFramesAtm;
            house.WindowFrames = Custom3DDB.NewCustomPart(gameObject, windowframes + house.CurWindowFrames, nameforthehouse + "Window Frames v" + house.CurWindowFrames);
       
        }
        if (insidewall != "")
        {
            if (house.InsideWall != null)
                DestroyImmediate(house.InsideWall);
            if (house.CurInsideWall > house.MaxInsideWallsAtm)
                house.CurInsideWall = house.MaxInsideWallsAtm;
            house.InsideWall = Custom3DDB.NewCustomPart(gameObject, insidewall + house.CurInsideWall, nameforthehouse + " InSideWalls v" + house.CurInsideWall);
        }
        if(insideroof != "")
        {
            if (house.InsideRoof != null)
                DestroyImmediate(house.InsideRoof);
            if (house.CurInsideRoof > house.MaxInsideRoofAtm)
                house.CurInsideRoof = house.MaxInsideRoofAtm;
            house.InsideRoof = Custom3DDB.NewCustomPart(gameObject, insideroof + house.CurInsideRoof, nameforthehouse + " InsideRoof v" + house.CurInsideRoof);
        }
        if (insideextrawalls != "")
        {
            if (house.InsideExtraWalls != null)
                DestroyImmediate(house.InsideExtraWalls);
            if (house.CurInsideExtraWalls > house.MaxInsideExtraWallsAtm)
                house.CurInsideExtraWalls = house.MaxInsideExtraWallsAtm;
            house.InsideExtraWalls = Custom3DDB.NewCustomPart(gameObject, insideextrawalls + house.CurInsideExtraWalls, nameforthehouse + " Inside Extra Walls v" + house.CurInsideExtraWalls);
        }
        if (f1floor != "")
        {
            if (house.F1Floor != null)
                DestroyImmediate(house.F1Floor);
            if (house.CurF1Floor > house.MaxF1FloorAtm)
                house.CurF1Floor = house.MaxF1FloorAtm;
            house.F1Floor = Custom3DDB.NewCustomPart(gameObject, f1floor + house.CurF1Floor, nameforthehouse + " F1Floor v" + house.CurF1Floor);
        }
        if (f2floor != "")
        {
            if (house.F2Floor != null)
                DestroyImmediate(house.F2Floor);
            if (house.CurF2Floor > house.MaxF2FloorAtm)
                house.CurF2Floor = house.MaxF2FloorAtm;
            house.F2Floor = Custom3DDB.NewCustomPart(gameObject, f2floor + house.CurF2Floor, nameforthehouse + " F2Floor v" + house.CurF2Floor);
        }
        if (frontstairs != "")
        {            
            if (house.FrontStairs != null)
                DestroyImmediate(house.FrontStairs);
            if (house.CurFrontStairs > house.MaxFrontStairsAtm)
                house.CurFrontStairs = house.MaxFrontStairsAtm;
            house.FrontStairs = Custom3DDB.NewCustomPart(gameObject, frontstairs + house.CurFrontStairs, nameforthehouse + " FrontStairs v" + house.CurFrontStairs);
        }
        if (windowglass != "")
        {
            if (house.WindowsGlass != null)
                DestroyImmediate(house.WindowsGlass);
            house.WindowsGlass = Custom3DDB.NewCustomPart(gameObject, windowglass, nameforthehouse + " Windows Glass");
        }
        if (thecollider != "")
        {
            if (house.Colliders != null)
                DestroyImmediate(house.Colliders);
            house.Colliders = Custom3DDB.NewCustomPart(gameObject, thecollider, nameforthehouse + " Colliders");
        }
        if (stairs != "")
        {
            if (house.Stair != null)
                DestroyImmediate(house.Stair);
            house.Stair = Custom3DDB.NewCustomPart(gameObject, stairs, nameforthehouse + " Stais");
        }
        if(basement != "")
        {
            if (house.Basement != null)
                DestroyImmediate(house.Basement);
            if (house.CurBasement > house.MaxBasementAtm)
                house.CurBasement = house.MaxBasementAtm;
            house.Basement = Custom3DDB.NewCustomPart(gameObject, basement + house.CurBasement, nameforthehouse + " Basement v" + house.CurBasement);
        }
        if (pillar != "")
        {
            if (house.Pillar != null)
                DestroyImmediate(house.Pillar);
            if (house.CurPillar > house.MaxPillarsAtm)
                house.CurPillar = house.MaxPillarsAtm;
            house.Pillar = Custom3DDB.NewCustomPart(gameObject, pillar + house.CurPillar, nameforthehouse + " Pillar v" + house.CurPillar); 
        }
        if (fireplace != "")
        {
            if (house.FirePlace != null)
                DestroyImmediate(house.FirePlace);
            if (house.CurFirePlace > house.MaxFirePlaceAtm)
                house.CurFirePlace = house.MaxFirePlaceAtm;
            house.FirePlace = Custom3DDB.NewCustomPart(gameObject, fireplace + house.CurFirePlace, nameforthehouse + " FirePlace v" + house.CurFirePlace);
        }
        if (door1 != "")
        {
            if (house.Door1 != null)
                DestroyImmediate(house.Door1);
            if (house.CurDoor1 > house.MaxDoor1Atm)
                house.CurDoor1 = house.MaxDoor1Atm;
            house.Door1 = Custom3DDB.NewCustomPart(gameObject, door1 + house.CurDoor1, nameforthehouse + " Door 1 v" + house.CurDoor1); 
        }
        if (door2 != "")
        {
            if (house.Door2 != null)
                DestroyImmediate(house.Door2);
            if (house.CurDoor2 > house.MaxDoor2Atm)
                house.CurDoor2 = house.MaxDoor2Atm;
            house.Door2 = Custom3DDB.NewCustomPart(gameObject, door2 + house.CurDoor2, nameforthehouse + " Door 2 v" + house.CurDoor2);
        }
        if(door3 != "")
        {
            if (house.Door3 != null)
                DestroyImmediate(house.Door3);
            if (house.CurDoor3 > house.MaxDoor3Atm)
                house.CurDoor3 = house.MaxDoor3Atm;
            house.Door3 = Custom3DDB.NewCustomPart(gameObject, door3 + house.CurDoor3, nameforthehouse + " Door 3 v" + house.CurDoor3);
        }
        if(door4 != "")
        {
            if (house.Door4 != null)
                DestroyImmediate(house.Door4);
            if (house.CurDoor4 > house.MaxDoor4Atm)
                house.CurDoor4 = house.MaxDoor4Atm;
            house.Door4 = Custom3DDB.NewCustomPart(gameObject, door4 + house.CurDoor4, nameforthehouse + " Door 4 v" + house.CurDoor4);
        }
        if (garagedoor != "")
        {
            if (house.GarageDoor != null)
                DestroyImmediate(house.GarageDoor);
            if (house.CurGarageDoor > house.MaxGarageDoorAtm)
                house.CurGarageDoor = house.MaxGarageDoorAtm;
            house.GarageDoor = Custom3DDB.NewCustomPart(gameObject, garagedoor + house.CurGarageDoor, nameforthehouse + " Garage Door v" + house.CurGarageDoor);
        }
        if (garage != "")
        {
            if (house.Garage != null)
                DestroyImmediate(house.Garage);
            if (house.CurGarage > house.MaxGarageAtm)
                house.CurGarage = house.MaxGarageAtm;
            house.Garage = Custom3DDB.NewCustomPart(gameObject, garage + house.CurGarage, nameforthehouse + " Garage v" + house.CurGarage);
        }
        if(extrafloor != "")
        {
            if (house.ExtraFloor != null)
                DestroyImmediate(house.ExtraFloor);
            if (house.CurExtraFloor > house.MaxExtraFloorAtm)
                house.CurExtraFloor = house.MaxExtraFloorAtm;
            house.ExtraFloor = Custom3DDB.NewCustomPart(gameObject, extrafloor + house.CurExtraFloor, nameforthehouse + " Extra Floor v" + house.CurExtraFloor);            
        }
        //Addons
        if(addon1 != "")
        {
            if (house.Addon1 != null)
                DestroyImmediate(house.Addon1);
            if (house.CurAddon1 > house.MaxAddon1Atm)
                house.CurAddon1 = house.MaxAddon1Atm;
            house.Addon1 = Custom3DDB.NewCustomPart(gameObject, addon1 + house.CurAddon1, nameforthehouse + " Addon 1 v" + house.CurAddon1);
        }
        if(addon2 != "")
        {
            if (house.Addon2 != null)
                DestroyImmediate(house.Addon2);
            if (house.CurAddon2 > house.MaxAddon2Atm)
                house.CurAddon2 = house.MaxAddon2Atm;
            house.Addon2 = Custom3DDB.NewCustomPart(gameObject, addon2 + house.CurAddon2, nameforthehouse + " Addon 2 v" + house.CurAddon2);
        } 
        if(addon3 != "")
        {
            if (house.Addon3 != null)
                DestroyImmediate(house.Addon3);
            if (house.CurAddon3 > house.MaxAddon3Atm)
                house.CurAddon3 = house.MaxAddon3Atm;
            house.Addon3 = Custom3DDB.NewCustomPart(gameObject, addon3 + house.CurAddon3, nameforthehouse + " Addon 3 v" + house.CurAddon3);
        }
        if (addon4 != "")
        {
            if (house.Addon4 != null)
                DestroyImmediate(house.Addon4);
            if (house.CurAddon4 > house.MaxAddon4Atm)
                house.CurAddon4 = house.MaxAddon4Atm;
            house.Addon4 = Custom3DDB.NewCustomPart(gameObject, addon4 + house.CurAddon4, nameforthehouse + " Addon 4 v" + house.CurAddon4);
        }
        if (addon5 != "")
        {
            if (house.Addon5 != null)
                DestroyImmediate(house.Addon5);
            if (house.CurAddon5 > house.MaxAddon5Atm)
                house.CurAddon5 = house.MaxAddon5Atm;
            house.Addon5 = Custom3DDB.NewCustomPart(gameObject, addon5 + house.CurAddon5, nameforthehouse + " Addon 5 v" + house.CurAddon5);
        }
        if (addon6 != "")
        {
            if (house.Addon6 != null)
                DestroyImmediate(house.Addon6);
            if (house.CurAddon6 > house.MaxAddon6Atm)
                house.CurAddon6 = house.MaxAddon6Atm;
            house.Addon6 = Custom3DDB.NewCustomPart(gameObject, addon6 + house.CurAddon6, nameforthehouse + " Addon 6 v" + house.CurAddon6);
        }
        if (addon7 != "")
        {
            if (house.Addon7 != null)
                DestroyImmediate(house.Addon7);
            if (house.CurAddon7 > house.MaxAddon7Atm)
                house.CurAddon7 = house.MaxAddon7Atm;
            house.Addon7 = Custom3DDB.NewCustomPart(gameObject, addon7 + house.CurAddon7, nameforthehouse + " Addon 7 v" + house.CurAddon7);
        }
        if (nameforthehouse != "")
        {
            gameObject.name = nameforthehouse;
        }
        gameObject.isStatic = true;
        #endregion
    }
    public void HouseChanger(int num, PartOfHouse part, bool more)
    {
        #region Vars
        int maxroof = house.MaxRoofAtm;
        int maxextraroof = house.MaxExtraRoofAtm;
        int maxoutsidewall = house.MaxOutSideWallsAtm;
        int maxinsidewall = house.MaxInsideWallsAtm;
        int maxinsideextrawall = house.MaxInsideExtraWallsAtm;
        int maxinsideroof = house.MaxInsideRoofAtm;
        int maxf1floor = house.MaxF1FloorAtm;
        int maxf2floor = house.MaxF2FloorAtm;
        int maxextrafloor = house.MaxExtraFloorAtm;
        int maxpillar = house.MaxPillarsAtm;
        int maxbasement = house.MaxBasementAtm;
        int maxfireplace = house.MaxFirePlaceAtm;
        int maxfrontstairs = house.MaxFrontStairsAtm;
        int maxdoor1 = house.MaxDoor1Atm;
        int maxdoor2 = house.MaxDoor2Atm;
        int maxdoor3 = house.MaxDoor3Atm;
        int maxdoor4 = house.MaxDoor4Atm;
        int maxgaragedoor = house.MaxGarageDoorAtm;
        int maxgarage = house.MaxGarageAtm;
        int maxframes = house.MaxFramesAtm;
        int maxwindowsframes = house.MaxWindowFramesAtm;
        
        int maxaddon1 = house.MaxAddon1Atm;
        /*
        int maxaddon2 = 1;
        int maxaddon3 = 1;
        int maxaddon4 = 1;
        int maxaddon5 = 1;
        int maxaddon6 = 1;
        int maxaddon7 = 1;
         * */
        #endregion
        
        
        #region Changing parts
        //Roof
        if (part == PartOfHouse.Roof && more == true)
        {
            
            if (house.CurRoof >= maxroof)
                house.CurRoof = 1;
            else
                house.CurRoof++;
        }
        else if(part == PartOfHouse.Roof && more == false)
        {
            if (house.CurRoof == 1)
                house.CurRoof = maxroof;
            else
                house.CurRoof--;
        }
        //ExtraRoof
        if (part == PartOfHouse.ExtraRoof && more == true)
        {

            if (house.CurExtraRoof >= maxextraroof)
                house.CurExtraRoof = 1;
            else
                house.CurExtraRoof++;
        }
        else if (part == PartOfHouse.ExtraRoof && more == false)
        {
            if (house.CurExtraRoof == 1)
                house.CurExtraRoof = maxextraroof;
            else
                house.CurExtraRoof--;
        }
        //Outsidewalls
        if (part == PartOfHouse.OutsideWall && more == true)
        {

            if (house.CurOutsideWall >= maxoutsidewall)
                house.CurOutsideWall = 1;
            else
                house.CurOutsideWall++;
        }
        else if (part == PartOfHouse.OutsideWall && more == false)
        {
            if (house.CurOutsideWall == 1)
                house.CurOutsideWall = maxoutsidewall;
            else
                house.CurOutsideWall--;
        }
        //Frames
        if (part == PartOfHouse.Frames && more == true)
        {

            if (house.CurFrames >= maxframes)
                house.CurFrames = 1;
            else
                house.CurFrames++;
        }
        else if (part == PartOfHouse.Frames && more == false)
        {
            if (house.CurFrames == 1)
                house.CurFrames = maxframes;
            else
                house.CurFrames--;
        }
        //WindowFrames
        if (part == PartOfHouse.WindowFrames && more == true)
        {

            if (house.CurWindowFrames >= maxwindowsframes)
                house.CurWindowFrames = 1;
            else
                house.CurWindowFrames++;
        }
        else if (part == PartOfHouse.WindowFrames && more == false)
        {
            if (house.CurWindowFrames == 1)
                house.CurWindowFrames = maxwindowsframes;
            else
                house.CurWindowFrames--;
        }
        //insidewalls
        if (part == PartOfHouse.InsideWall && more == true)
        {

            if (house.CurInsideWall >= maxinsidewall)
                house.CurInsideWall = 1;
            else
                house.CurInsideWall++;
        }
        else if (part == PartOfHouse.InsideWall && more == false)
        {
            if (house.CurInsideWall == 1)
                house.CurInsideWall = maxinsidewall;
            else
                house.CurInsideWall--;
        }
        //insideroof
        if (part == PartOfHouse.InsideRoof && more == true)
        {

            if (house.CurInsideRoof >= maxinsideroof)
                house.CurInsideRoof = 1;
            else
                house.CurInsideRoof++;
        }
        else if (part == PartOfHouse.InsideRoof && more == false)
        {
            if (house.CurInsideRoof == 1)
                house.CurInsideRoof = maxinsideroof;
            else
                house.CurInsideRoof--;
        }
        //insideextrawalls
        if (part == PartOfHouse.InsideExtraWall && more == true)
        {

            if (house.CurInsideExtraWalls >= maxinsideextrawall)
                house.CurInsideExtraWalls = 1;
            else
                house.CurInsideExtraWalls++;
        }
        else if (part == PartOfHouse.InsideExtraWall && more == false)
        {
            if (house.CurInsideExtraWalls == 1)
                house.CurInsideExtraWalls = maxinsideextrawall;
            else
                house.CurInsideExtraWalls--;
        }
        //F1Floor
        if (part == PartOfHouse.F1Floor && more == true)
        {

            if (house.CurF1Floor >= maxf1floor)
                house.CurF1Floor = 1;
            else
                house.CurF1Floor++;
        }
        else if (part == PartOfHouse.F1Floor && more == false)
        {
            if (house.CurF1Floor == 1)
                house.CurF1Floor = maxf1floor;
            else
                house.CurF1Floor--;
        }
        //F2Floor
        if (part == PartOfHouse.F2Floor && more == true)
        {

            if (house.CurF2Floor >= maxf2floor)
                house.CurF2Floor = 1;
            else
                house.CurF2Floor++;
        }
        else if (part == PartOfHouse.F2Floor && more == false)
        {
            if (house.CurF2Floor == 1)
                house.CurF2Floor = maxf2floor;
            else
                house.CurF2Floor--;
        }
        //Extra floor
        if (part == PartOfHouse.ExtraFloor && more == true)
        {

            if (house.CurExtraFloor >= maxextrafloor)
                house.CurExtraFloor = 1;
            else
                house.CurExtraFloor++;
        }
        else if (part == PartOfHouse.ExtraFloor && more == false)
        {
            if (house.CurExtraFloor == 1)
                house.CurExtraFloor = maxextrafloor;
            else
                house.CurExtraFloor--;
        }
        //FirePlace
        if (part == PartOfHouse.Fireplace && more == true)
        {

            if (house.CurFirePlace >= maxfireplace)
                house.CurFirePlace = 1;
            else
                house.CurFirePlace++;
        }
        else if (part == PartOfHouse.Fireplace && more == false)
        {
            if (house.CurFirePlace == 1)
                house.CurFirePlace = maxfireplace;
            else
                house.CurFirePlace--;
        }
        //Door1
        if (part == PartOfHouse.Door1 && more == true)
        {

            if (house.CurDoor1 >= maxdoor1)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
        }
        else if (part == PartOfHouse.Door1 && more == false)
        {
            if (house.CurDoor1 == 1)
                house.CurDoor1 = maxdoor1;
            else
                house.CurDoor1--;
        }
        //Door2
        if (part == PartOfHouse.Door2 && more == true)
        {

            if (house.CurDoor2 >= maxdoor2)
                house.CurDoor2 = 1;
            else
                house.CurDoor2++;
        }
        else if (part == PartOfHouse.Door2 && more == false)
        {
            if (house.CurDoor2 == 1)
                house.CurDoor2 = maxdoor2;
            else
                house.CurDoor2--;
        }
        //Door3
        if (part == PartOfHouse.Door3 && more == true)
        {

            if (house.CurDoor3 >= maxdoor3)
                house.CurDoor3 = 1;
            else
                house.CurDoor3++;
        }
        else if (part == PartOfHouse.Door3 && more == false)
        {
            if (house.CurDoor3 == 1)
                house.CurDoor3 = maxdoor3;
            else
                house.CurDoor3--;
        }
        //Door4
        if (part == PartOfHouse.Door4 && more == true)
        {

            if (house.CurDoor4 >= maxdoor4)
                house.CurDoor4 = 1;
            else
                house.CurDoor4++;
        }
        else if (part == PartOfHouse.Door4 && more == false)
        {
            if (house.CurDoor4 == 1)
                house.CurDoor4 = maxdoor4;
            else
                house.CurDoor4--;
        }
        //Garagedoor
        if (part == PartOfHouse.GargageDoor && more == true)
        {

            if (house.CurGarageDoor >= maxgaragedoor)
                house.CurGarageDoor = 1;
            else
                house.CurGarageDoor++;
        }
        else if (part == PartOfHouse.GargageDoor && more == false)
        {
            if (house.CurGarageDoor == 1)
                house.CurGarageDoor = maxgaragedoor;
            else
                house.CurGarageDoor--;
        }
        //Garage
        if (part == PartOfHouse.Garage && more == true)
        {

            if (house.CurGarage >= maxgarage)
                house.CurGarage = 1;
            else
                house.CurGarage++;
        }
        else if (part == PartOfHouse.Garage && more == false)
        {
            if (house.CurGarage == 1)
                house.CurGarage = maxgarage;
            else
                house.CurGarage--;
        }
        //Pillar
        if (part == PartOfHouse.Pillar && more == true)
        {

            if (house.CurPillar >= maxpillar)
                house.CurPillar = 1;
            else
                house.CurPillar++;
        }
        else if (part == PartOfHouse.Pillar && more == false)
        {
            if (house.CurPillar == 1)
                house.CurPillar = maxpillar;
            else
                house.CurPillar--;
        }
        //FronstStairs
        if (part == PartOfHouse.FrontStairs && more == true)
        {

            if (house.CurFrontStairs >= maxfrontstairs)
                house.CurFrontStairs = 1;
            else
                house.CurFrontStairs++;
        }
        else if (part == PartOfHouse.FrontStairs && more == false)
        {
            if (house.CurFrontStairs == 1)
                house.CurFrontStairs = maxfrontstairs;
            else
                house.CurFrontStairs--;
        }
        //Basement
        if (part == PartOfHouse.Basement && more == true)
        {

            if (house.CurBasement >= maxbasement)
                house.CurBasement = 1;
            else
                house.CurBasement++;
        }
        else if (part == PartOfHouse.Basement && more == false)
        {
            if (house.CurBasement == 1)
                house.CurBasement = maxbasement;
            else
                house.CurBasement--;
        }
        //Addon1
        if (part == PartOfHouse.Addon1 && more == true)
        {

            if (house.CurAddon1 >= maxaddon1)
                house.CurAddon1 = 1;
            else
                house.CurAddon1++;
        }
        else if (part == PartOfHouse.Addon1 && more == false)
        {
            if (house.CurAddon1 == 1)
                house.CurAddon1 = maxaddon1;
            else
                house.CurAddon1--;
        }



        #endregion

        HouseBuilder(num);

    }

    public void LongHouseBuilder(int Base,int F2,int Roof)
    {
        house.HouseFiller = HouseFiller.LongHouse;
        string thebase = "";
        string theframe = "";
        string theRoof = "";
        string theRoofPart = "";
        string thedoor = "";
        string thef2 = "";
        #region SetPaths
        switch (Base)
        {
            case 1:
                thebase = _LBaseL1Path;
                theframe = _LBaseL1FramePath;
                thedoor = _LDoorLPath;
                break;
            case 2:
                thebase = _LBaseM1Path;
                theframe = _LBaseM1FramePath;
                thedoor = _LDoorMPath;
                break;
            case 3:
                thebase = _LBaseM2Path;
                theframe = _LBaseM2FramePath;
                thedoor = _LDoorM2Path;
                break;
            case 4:
                thebase = _LBaseR1Path;
                theframe = _LBaseR1FramePath;
                thedoor = _LDoorRPath;
                break;              
        }
        switch (F2)
        {
            case 0:
               // Debug.Log("There is no F2");
                break;
            case 1:
                thef2 = _LF21Path;
                break;
        }
        switch (Roof)
        {
            case 1:
                theRoof = _LRoof1Path;
                theRoofPart = _LRoof1PartPath;
                break;
            case 2 :
                theRoof = _LRoof2Path;
                break;
            case 3:
                theRoof = _LRoof3Path;
                theRoofPart = _LRoof3PartPath;
                break;
        }
#endregion

        #region CreateParts

        if(thebase != "")
        {
            if (house.Base != null)
                DestroyImmediate(house.Base);
            house.Base = Custom3DDB.NewCustomPart(gameObject, thebase + house.CurBaseVersion, "base " + house.CurBase + " version " + house.CurBaseVersion);
        }
        if (theframe != "")
        {
            if (house.Frames != null)
                DestroyImmediate(house.Frames);
            if (house.CurFrames > house.MaxFramesL)
                house.CurFrames = house.MaxFramesL;
            house.Frames = Custom3DDB.NewCustomPart(gameObject, theframe + house.CurFrames, "frames v" + house.CurFrames);
        }
        if(thedoor != "")
        {
            if (house.Door1 != null)
                DestroyImmediate(house.Door1);
            if (house.CurDoor1 > house.MaxDoorsL)
                house.CurDoor1 = house.MaxDoorsL;
            house.Door1 = Custom3DDB.NewCustomPart(gameObject, thedoor + house.CurDoor1, "door v" + house.CurDoor1);
        }
        if(thef2 != "")
        {
            if (house.F2 != null)
                DestroyImmediate(house.F2);
            house.F2 = Custom3DDB.NewCustomPart(gameObject, thef2 + house.CurF2Version, "F2 " + house.CurF2Version + " version " + house.CurF2Version);
        }
        else
        {
            if (house.F2 != null)
                DestroyImmediate(house.F2);
        }
        if(theRoof != "")
        {
            if (house.Roof != null)
                DestroyImmediate(house.Roof);
            if (house.CurRoof > house.MaxRoofsVersionsL)
                house.CurRoof = house.MaxRoofsVersionsL;
            house.Roof = Custom3DDB.NewCustomPart(gameObject, theRoof + house.CurRoof, "Roof " + house.CurRoofDesign + " version " + house.CurRoof);
            if (house.F2 != null)
            {
                house.Roof.transform.position = new Vector3( gameObject.transform.position.x,gameObject.transform.position.y + 4.27f,gameObject.transform.position.z); 
            }
        }
        if(theRoofPart != "")
        {
            if (house.RoofPart != null)
                DestroyImmediate(house.RoofPart);
            house.RoofPart = Custom3DDB.NewCustomPart(house.Roof.transform.gameObject, theRoofPart + house.CurRoofPartVersion, "Roof Part" + house.CurRoofPartVersion);
        }
        #endregion
                    
        gameObject.name = "LongHouse Custom";        
        gameObject.isStatic = true;
       
    }

    public void ChangeFillHouses(HousePart part , bool next)
    {
        //Roof
        if(part == HousePart.TheRoofVersion && next == true)
        {            
            if (house.CurRoof >= house.MaxRoofsVersionsL)
                house.CurRoof = 1;
            else
            house.CurRoof++;
        }
        else if (part == HousePart.TheRoofVersion && next == false)
        {           
            if (house.CurRoof == 1)
                house.CurRoof = house.MaxRoofsVersionsL;
            else
                house.CurRoof--;
        }
        //RoofParts
        if(part == HousePart.TheRoofParts && next == true)
        {           
            if (house.CurRoofPartVersion >= house.MaxRoofPartsVersion)
                house.CurRoofPartVersion = 1;
            else
                house.CurRoofPartVersion++;
        }
        else if (part == HousePart.TheRoofParts && next == false)
        {
            if (house.CurRoofPartVersion == 1)
                house.CurRoofPartVersion = house.MaxRoofPartsVersion;
            else
                house.CurRoofPartVersion--;
        }
        //RoofDesign
        if (part == HousePart.TheRoof && next == true)
        {           
            if (house.CurRoofDesign >= house.MaxRoofDesignsL)
                house.CurRoofDesign = 1;
            else
                house.CurRoofDesign++;
        }
        else if (part == HousePart.TheRoof && next == false)
        {            
            if (house.CurRoofDesign == 1)
                house.CurRoofDesign = house.MaxRoofDesignsL;
            else
                house.CurRoofDesign--;
        }
        //F2 Version
        if (part == HousePart.TheF2Versions && next == true)
        {            
            if (house.CurF2Version >= house.MaxF2sVersionsL)
                house.CurF2Version = 1;
            else
                house.CurF2Version++;
        }
        else if (part == HousePart.TheF2Versions && next == false)
        {
            if (house.CurF2Version == 1)
                house.CurF2Version = house.MaxF2sVersionsL;
            else
                house.CurF2Version--;
        }
        //F2 Design
        if (part == HousePart.TheF2Designs && next == true)
        {            
            if (house.CurF2 >= house.MaxF2DesignsL)
                house.CurF2 = 0;
            else
                house.CurF2++;
        }
        else if (part == HousePart.TheF2Designs && next == false)
        {
            if (house.CurF2 == 0)
                house.CurF2 = house.MaxF2DesignsL;
            else
                house.CurF2--;
        }
        //BaseDesign
        if (part == HousePart.BaseDesign && next == true)
        {            
            if (house.CurBase >= house.MaxBaseDesignsL)
                house.CurBase = 1;
            else
                house.CurBase++;
        }
        else if (part == HousePart.BaseDesign && next == false)
        {
            if (house.CurBase == 1)
                house.CurBase = house.MaxBaseDesignsL;
            else
                house.CurBase--;
        }
        //Base Versions
        if (part == HousePart.TheBaseWalls && next == true)
        {            
            if (house.CurBaseVersion >= house.MaxBaseVL)
                house.CurBaseVersion = 1;
            else
                house.CurBaseVersion++;
        }
        else if (part == HousePart.TheBaseWalls && next == false)
        {
            if (house.CurBaseVersion == 1)
                house.CurBaseVersion = house.MaxBaseVL;
            else
                house.CurBaseVersion--;
        }
        //Base Frames
        if (part == HousePart.TheBaseFrames && next == true)
        {
            if (house.CurFrames >= house.MaxFramesL)
                house.CurFrames = 1;
            else
                house.CurFrames++;
        }
        else if (part == HousePart.TheBaseFrames && next == false)
        {
            if (house.CurFrames == 1)
                house.CurFrames = house.MaxFramesL;
            else
                house.CurFrames--;
        }
        //Base Door
        if (part == HousePart.TheBaseDoor && next == true)
        {
            if (house.CurDoor1 >= house.MaxDoorsL)
                house.CurDoor1 = 1;
            else
                house.CurDoor1++;
        }
        else if (part == HousePart.TheBaseDoor && next == false)
        {
            if (house.CurDoor1 == 1)
                house.CurDoor1 = house.MaxDoorsL;
            else
                house.CurDoor1--;
        }
        LongHouseBuilder(house.CurBase, house.CurF2, house.CurRoofDesign);
    }


    public void SetToGoundLevel()
    {
        if(gameObject.transform.position.y <= 0)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit))
        {
            Vector3 targetPosition = hit.point;

            gameObject.transform.position = targetPosition; 
        }

    }

    #region old code
    /*
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
                pathtoFollow =  _h7RoofsPath;
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
                path = _h7OutSideWallsPath;
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
                path = _h7InsideWallsPath;
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
                path = _h7F1FloorPath;
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
                path = _h7F2FloorPath;
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
                path = _h7ExtraFloorPath;
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
                path = _h7StairsPath;
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
                path = _h7WindowsGlassPath;
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
                path = _h7Door1Path;
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
                path = _h7Door2Path;
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
                path = _h7Door3Path;
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
                path = _h7GarageDoorPath;
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
                path = _h7CollidersPath;
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
                path = _h7GaragePath;
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
                path = _h7FramesPath;
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
            if (house.CurExtraFloor > house.MaxBasements4)
                house.CurExtraFloor = house.MaxBasements4;
            if (house.CurExtraFloor == house.MaxBasements4)
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
            if (house.CurExtraFloor > house.MaxBasements4)
                house.CurExtraFloor = house.MaxBasements4;
            if (house.CurExtraFloor == 1)
                house.CurExtraFloor = house.MaxBasements4;
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
    */
    #endregion

}
#endif