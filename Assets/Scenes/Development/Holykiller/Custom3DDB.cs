using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class Custom3DDB 
{
    public GameObject Roof;
    public GameObject OutSideWall;
    public GameObject InsideWall;
    public GameObject F1Floor;
    public GameObject F2Floor;
    public GameObject ExtraFloor;
    public GameObject Stair;
    public GameObject WindowsGlass;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject Pillar;
    public GameObject Colliders;
    public GameObject Addon1;
    public GameObject Addon2;
    public GameObject Addon3;
    public GameObject Addon4;
    public GameObject Addon5;
    public GameObject Addon6;
    public GameObject Addon7;

    public int CurRoof;
    public int CurOutsideWall;
    public int CurInsideWall;
    public int CurF1Floor;
    public int CurF2Floor;
    public int CurExtraFloor;
    public int CurDoor1;
    public int CurDoor2;
    public int CurDoor3;
    public int CurDoor4;
    public int CurPillar;
    public int CurAddon1;
    public int CurAddon2;
    public int CurAddon3;
    public int CurAddon4;
    public int CurAddon5;
    public int CurAddon6;
    public int CurAddon7;

    public int MaxRoofs14 = 25;
    public int MaxRoofs24 = 25;
    public int MaxOutSideWalls4 = 13;
    public int MaxInsideWalls4 = 10;
    public int MaxFloor14 = 14;
    public int MaxFloor24 = 13;
    public int MaxFirePlace4 = 1;
    public int MaxFrontStairs4 = 2;
    public int MaxExtraFloors4 = 12;
    public int MaxDoors4 = 6;

    public int MaxRoofs8 = 25;
    public int MaxOutSideWalls8 = 13;
    public int MaxInsideWalls8 = 10;
    public int MaxFloor18 = 18;
    public int MaxDetails8 = 10;
    public int MaxPillars8 = 10;
    public int MaxDoors8 = 6;

    


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

public enum HouseNumber
{
    None,
    House004,
    House007,
    House008,
    House009
}
