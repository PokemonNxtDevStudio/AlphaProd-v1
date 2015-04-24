using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class Custom3DDB 
{
    public GameObject Roof;
    public GameObject ExtraRoof;
    public GameObject OutSideWall;
    public GameObject InsideWall;
    public GameObject InsideRoof;
    public GameObject InsideExtraWalls;
    public GameObject F1Floor;
    public GameObject F2Floor;
    public GameObject FrontStairs;
    public GameObject ExtraFloor;
    public GameObject Stair;
    public GameObject WindowsGlass;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject GarageDoor;
    public GameObject Garage;
    public GameObject Pillar;
    public GameObject Colliders;
    public GameObject Basement;
    public GameObject FirePlace;
    public GameObject Frames;
    public GameObject Addon1;
    public GameObject Addon2;
    public GameObject Addon3;
    public GameObject Addon4;
    public GameObject Addon5;
    public GameObject Addon6;
    public GameObject Addon7;

    public int CurRoof = 1;
    public int CurExtraRoof = 1;
    public int CurOutsideWall = 1;
    public int CurInsideWall = 1;
    public int CurInsideRoof = 1;
    public int CurInsideExtraWalls = 1;
    public int CurBasement = 1;
    public int CurF1Floor = 1;
    public int CurF2Floor = 1;
    public int CurExtraFloor = 1;
    public int CurDoor1 = 1;
    public int CurDoor2 = 1;
    public int CurDoor3 = 1;
    public int CurDoor4 = 1;
    public int CurGarageDoor = 1;
    public int CurGarage = 1;
    public int CurPillar = 1;
    public int CurFirePlace = 1;
    public int CurFrontStairs = 1;
    public int CurFrames = 1;
    public int CurAddon1 = 1;
    public int CurAddon2 = 1;
    public int CurAddon3 = 1;
    public int CurAddon4 = 1;
    public int CurAddon5 = 1;
    public int CurAddon6 = 1;
    public int CurAddon7 = 1;

    public int MaxRoofAtm = 1;
    public int MaxExtraRoofAtm = 1;
    public int MaxOutSideWallsAtm = 1;
    public int MaxInsideWallsAtm = 1;
    public int MaxInsideRoofAtm = 1;
    public int MaxInsideExtraWallsAtm = 1;
    public int MaxF1FloorAtm = 1;
    public int MaxF2FloorAtm = 1;
    public int MaxExtraFloorAtm = 1;
    public int MaxDoor1Atm = 1;
    public int MaxDoor2Atm = 1;
    public int MaxDoor3Atm = 1;
    public int MaxDoor4Atm = 1;
    public int MaxGarageDoorAtm = 1;
    public int MaxGarageAtm = 1;
    public int MaxFirePlaceAtm = 1;
    public int MaxPillarsAtm = 1;
    public int MaxBasementAtm = 1;
    public int MaxFrontStairsAtm = 1;
    public int MaxFramesAtm = 1;

    public int MaxAddon1Atm = 1;
    public int MaxAddon2Atm = 1;
    public int MaxAddon3Atm = 1;
    public int MaxAddon4Atm = 1;
    public int MaxAddon5Atm = 1;
    public int MaxAddon6Atm = 1;
    public int MaxAddon7Atm = 1;

    //House 3 Parts
    public int MaxRoofs3 = 25;
    public int MaxOutSideWalls3 = 13;
    public int MaxInsideWalls3 = 10;
    public int MaxInsideRoofs3 = 2;
    public int MaxFloor13 = 14;
    public int MaxFloor23 = 14;
    public int MaxDoors3 = 6;
    public int MaxFirePlaces3 = 2;

    //House 4 Parts
    public int MaxRoofs4 = 25;
    public int MaxExtraRoof4 = 25;
    public int MaxOutSideWalls4 = 13;
    public int MaxInsideWalls4 = 10;
    public int MaxFloor14 = 14;
    public int MaxFloor24 = 13;
    public int MaxFirePlace4 = 1;
    public int MaxFrontStairs4 = 2;
    public int MaxBasements4 = 12;
    public int MaxDoors4 = 6;

    //House 5 Parts
    public int MaxF1Floor5 = 13;
    public int MaxF2Floor5 = 13;
    public int MaxInsideWalls5 = 10;
    public int MaxInsideRoofs5 = 3;
    public int MaxInsideExtraWalls5 = 10;
    public int MaxOutsideWalls5 = 11;
    public int MaxPillars5 = 10;
    public int MaxRoofs5 = 25;
    public int MaxDoors5 = 6;

    //House 6 Parts
    public int MaxF1Floor6 = 12;
    public int MaxF2Floor6 = 12;
    public int MaxInsideWalls6 = 10;
    public int MaxInsideRoofs6 = 2;
    public int MaxOutsideWalls6 = 13;
    public int MaxFrames6 = 12;
    public int MaxFrontStairs6 = 5;
    public int MaxRoofs6 = 25;
    public int MaxDoors6 = 6;

    //House 7 Parts
    public int MaxRoofs7 = 25;
    public int MaxOutsideWalls7 = 13;
    public int MaxInsideWalls7 = 10;
    public int MaxF1Floors7 = 18;
    public int MaxF2Floors7 = 16;
    public int MaxExtraFloor7 = 18;
    public int MaxDoor17 = 6;
    public int MaxDoor27 = 6;
    public int MaxDoor37 = 6;
    public int MaxGarageDoor7 = 5;
    public int MaxGarage7 = 12;
    public int MaxFrames7 = 1;
    
    //House 8 Parts
    public int MaxRoofs8 = 25;
    public int MaxOutSideWalls8 = 13;
    public int MaxInsideWalls8 = 10;
    public int MaxFloor18 = 18;
    public int MaxFrames8 = 10;
    public int MaxPillars8 = 10;
    public int MaxDoors8 = 6;

    //House 9 Parts
    public int MaxRoofs9 = 25;
    public int MaxOutsideWalls9 = 13;
    public int MaxInsideWalls9 = 10;
    public int MaxF1Floors9 = 16;
    public int MaxF2Floors9 = 16;
    public int MaxDoor19 = 6;
    public int MaxDoor29 = 6;
    public int MaxDoor39 = 6;
    public int MaxDoor49 = 6;
    public int MaxPillars9 = 3;
    public int MaxInsideRoofs9 = 1;


    public HouseNumber HouseNumber;

    public static GameObject NewCustomPart(GameObject goparent,string path,string nameforthego)
    {

        GameObject go;
        go = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(path + ".prefab",typeof(GameObject)));
        go.transform.position = goparent.transform.position;
        go.transform.rotation = goparent.transform.rotation;
        go.name = nameforthego;
        go.transform.parent = goparent.transform;
        return go;       
    }   
}

public enum PartOfHouse
{
    Roof,
    ExtraRoof,
    OutsideWall,
    InsideWall,
    InsideRoof,
    InsideExtraWall,
    F1Floor,
    F2Floor,
    ExtraFloor,
    Fireplace,
    Door1,
    Door2,
    Door3,
    Door4,
    GargageDoor,
    Garage,
    FrontStairs,
    Pillar,
    Basement,
    Frames,
    Addon1,
    Addon2,
    Addon3,
    Addon4,
    Addon5,
    Addon6,
    Addon7
}
public enum HouseNumber
{
    None,
    House003 = 3,
    House004 = 4,
    House005 = 5,
    House006 = 6,
    House007 = 7,
    House008 = 8,
    House009 = 9
}
