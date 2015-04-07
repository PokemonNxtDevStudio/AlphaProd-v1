using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(HouseCustom))]
public class HouseCustomEditor : Editor
{
    
    private string _helpInfo = "help Box";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HouseCustom house = (HouseCustom)target;
 
        
        if(GUILayout.Button("New House"))
        {
            house._NewHouse = true;
            house.MakeAhouse007 = false;
            house.DeleteHouse007();
            _helpInfo = "Select House To Create";

        }

        #region new houses
        if (house._NewHouse == true)
        {
            if (GUILayout.Button("New House 001"))
            {
                house._NewHouse = false;
                

            }
            if (GUILayout.Button("New House 002"))
            {
                house._NewHouse = false;
               
            }
            if (GUILayout.Button("New House 003"))
            {
                house._NewHouse = false;
                
            }
            if (GUILayout.Button("New House 004"))
            {
                house._NewHouse = false;
               
            }
            if (GUILayout.Button("New House 005"))
            {
                house._NewHouse = false;
                
            }
            if (GUILayout.Button("New House 006"))
            {
                house._NewHouse = false;
              
            }
            if (GUILayout.Button("New House 007"))
            {
                house._NewHouse = false;
                house.MakeAhouse007 = true;
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("New House 008"))
            {
                house._NewHouse = false;

            }
            if (GUILayout.Button("New House 009"))
            {
                house._NewHouse = false;

            }
            EditorGUILayout.HelpBox(_helpInfo, MessageType.Info);

        }
        #endregion

        if(house.MakeAhouse007 == true)
        {
            //EditorGUILayout.HelpBox("Roof " + house.CurRoof + " / " + house.MaxRoof,MessageType.Info);
            GUILayout.Space(5);
            GUILayout.Label("Roof : " + house.CurRoof + " / " + house.MaxRoof);
            if (GUILayout.Button("previous Roof"))
            {
                if (house.CurRoof == 1)
                {
                    house.CurRoof = house.MaxRoof;
                }
                else
                {
                    house.CurRoof--;
                }
                house.MakeNewCustomHouse007();
            }
            if(GUILayout.Button("Next Roof"))
            {
                if (house.CurRoof == house.MaxRoof)
                {
                    house.CurRoof = 1;
                }
                else
                {
                    house.CurRoof++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("OutsideWalls :" + house.CurOutSideWalls + " / " + house.MaxOutSideWalls);
            if (GUILayout.Button("previous OutsideWalls"))
            {
                if (house.CurOutSideWalls == 1)
                {
                    house.CurOutSideWalls = house.MaxOutSideWalls;
                }
                else
                {
                    house.CurOutSideWalls--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next OutsideWalls"))
            {
                if (house.CurOutSideWalls == house.MaxOutSideWalls)
                {
                    house.CurOutSideWalls = 1;
                }
                else
                {
                    house.CurOutSideWalls++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Garage : " + house.CurGarage + " / " + house.MaxGarage);
            if (GUILayout.Button("previous Garage"))
            {
                if (house.CurGarage == 1)
                {
                    house.CurGarage = house.MaxGarage;
                }
                else
                {
                    house.CurGarage--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Garage"))
            {
                if (house.CurGarage == house.MaxGarage)
                {
                    house.CurGarage = 1;
                }
                else
                {
                    house.CurGarage++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("GarageDoor : " + house.CurGarageDoor + " / " + house.MaxGarageDoor);
            if (GUILayout.Button("previous GarageDoor"))
            {
                if (house.CurGarageDoor == 1)
                {
                    house.CurGarageDoor = house.MaxGarageDoor;
                }
                else
                {
                    house.CurGarageDoor--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next GarageDoor"))
            {
                if (house.CurGarageDoor == house.MaxGarageDoor)
                {
                    house.CurGarageDoor = 1;
                }
                else
                {
                    house.CurGarageDoor++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Door 1 : " + house.CurDoor1 + " / " + house.MaxDoor1);
            if (GUILayout.Button("previous Door 1(front)"))
            {
                if (house.CurDoor1 == 1)
                {
                    house.CurDoor1 = house.MaxDoor1;
                }
                else
                {
                    house.CurDoor1--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Door 1(front"))
            {
                if (house.CurDoor1 == house.MaxDoor1)
                {
                    house.CurDoor1 = 1;
                }
                else
                {
                    house.CurDoor1++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Door 2 : " + house.CurDoor2 + " / " + house.MaxDoor2);
            if (GUILayout.Button("previous Door 2(Back)"))
            {
                if (house.CurDoor2 == 1)
                {
                    house.CurDoor2 = house.MaxDoor2;
                }
                else
                {
                    house.CurDoor2--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Door 2(back"))
            {
                if (house.CurDoor2 == house.MaxDoor2)
                {
                    house.CurDoor2 = 1;
                }
                else
                {
                    house.CurDoor2++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Door 3 : " + house.CurDoor3 + " / " + house.MaxDoor3);
            if (GUILayout.Button("previous Door 3(Garage)"))
            {
                if (house.CurDoor3 == 1)
                {
                    house.CurDoor3 = house.MaxDoor3;
                }
                else
                {
                    house.CurDoor3--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Door 3(Garage"))
            {
                if (house.CurDoor3 == house.MaxDoor3)
                {
                    house.CurDoor3 = 1;
                }
                else
                {
                    house.CurDoor3++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Floor 1 : " + house.CurFloor1 + " / " + house.MaxFloor1);
            if (GUILayout.Button("previous Floor 1"))
            {
                if (house.CurFloor1 == 1)
                {
                    house.CurFloor1 = house.MaxFloor1;
                }
                else
                {
                    house.CurFloor1--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Floor 1"))
            {
                if (house.CurFloor1 == house.MaxFloor1)
                {
                    house.CurFloor1 = 1;
                }
                else
                {
                    house.CurFloor1++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Floor 2 : " + house.CurFloor2 + " / " + house.MaxFloor2);
            if (GUILayout.Button("previous Floor 2"))
            {
                if (house.CurFloor2 == 1)
                {
                    house.CurFloor2 = house.MaxFloor2;
                }
                else
                {
                    house.CurFloor2--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Floor 2"))
            {
                if (house.CurFloor2 == house.MaxFloor2)
                {
                    house.CurFloor2 = 1;
                }
                else
                {
                    house.CurFloor2++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Floor 3 : " + house.CurFloor3 + " / " + house.MaxFloor3);
            if (GUILayout.Button("previous Floor 3"))
            {
                if (house.CurFloor3 == 1)
                {
                    house.CurFloor3 = house.MaxFloor3;
                }
                else
                {
                    house.CurFloor3--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Floor 3"))
            {
                if (house.CurFloor3 == house.MaxFloor3)
                {
                    house.CurFloor3 = 1;
                }
                else
                {
                    house.CurFloor3++;
                }
                house.MakeNewCustomHouse007();
            }
            GUILayout.Space(5);
            GUILayout.Label("Inside Walls : " + house.CurInsideWalls + " / " + house.MaxInsideWalls);
            if (GUILayout.Button("previous Inside Walls"))
            {
                if (house.CurInsideWalls == 1)
                {
                    house.CurInsideWalls = house.MaxInsideWalls;
                }
                else
                {
                    house.CurInsideWalls--;
                }
                house.MakeNewCustomHouse007();
            }
            if (GUILayout.Button("Next Inside Walls"))
            {
                if (house.CurInsideWalls == house.MaxInsideWalls)
                {
                    house.CurInsideWalls = 1;
                }
                else
                {
                    house.CurInsideWalls++;
                }
                house.MakeNewCustomHouse007();
            }
        }

        
        
    }



}
