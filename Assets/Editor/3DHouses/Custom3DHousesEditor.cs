using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Custom3DHouses))]
public class Custom3DHousesEditor : Editor
{
    


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Custom3DHouses house = (Custom3DHouses)target;
        GUILayout.BeginVertical();
        if(GUILayout.Button("New House"))
        {
            house.DeleteCurrentHouse();            
        }
        GUILayout.Space(20);
        if(house.House.HouseNumber == HouseNumber.None)
        {
            if(GUILayout.Button("New House004"))
            {
                house.NewHouse004();
            }
            if (GUILayout.Button("New House 007"))
            {
                house.NewHouse007();
            }
            if(GUILayout.Button("New House 008"))
            {
                house.NewHouse008();
            }
            if (GUILayout.Button( "New House 009"))
            {
                house.NewHouse009();
            }
        }
        if(house.House.HouseNumber != HouseNumber.None)
        {
            #region House004 CustomUI

            if (house.House.HouseNumber == HouseNumber.House004)
            {
                GUILayout.Label("Roof v " + house.House.CurRoof + " / " + house.House.MaxRoofs14);
                if(GUILayout.Button("Next Roof"))
                {
                    house.NRoof();
                }
                if (GUILayout.Button("Previous Roof"))
                {
                    house.PRoof();
                }

                GUILayout.Label("SecondRoof v " + house.House.CurPillar + " / " + house.House.MaxRoofs24);
                if (GUILayout.Button("Next SecondRoof"))
                {
                    house.NPillars();
                }
                if (GUILayout.Button("Previous SecondRoof"))
                {
                    house.PPillars();
                }

                GUILayout.Label("OutSideWalls v " + house.House.CurOutsideWall + " / " + house.House.MaxOutSideWalls4);
                if (GUILayout.Button("Next OutSideWalls"))
                {
                    house.NOutSideWalls();
                }
                if (GUILayout.Button("Previous OutSideWalls"))
                {
                    house.POutSideWalls();
                }
                GUILayout.Label("FrontStairs v " + house.House.CurAddon1 + " / " + house.House.MaxFrontStairs4);
                if (GUILayout.Button("Next FrontStairs"))
                {
                    house.NAddon1();
                }
                if (GUILayout.Button("Previous FrontStairs"))
                {
                    house.PAddon1();
                }
                GUILayout.Label("Basement v " + house.House.CurExtraFloor + " / " + house.House.MaxExtraFloors4);
                if (GUILayout.Button("Next Basement"))
                {
                    house.NExtraFloor();
                }
                if (GUILayout.Button("Previous Basement"))
                {
                    house.PExtraFloor();
                }

                GUILayout.Label("Door v " + house.House.CurDoor1 + " / " + house.House.MaxDoors4);
                if (GUILayout.Button("Next Door"))
                {
                    house.NDoor1();
                }
                if (GUILayout.Button("Previous Door"))
                {
                    house.PDoor1();
                }

                GUILayout.Label("InsideWalls v " + house.House.CurInsideWall + " / " + house.House.MaxInsideWalls4);
                if (GUILayout.Button("Next InsideWalls"))
                {
                    house.NInsideWalls();
                }
                if (GUILayout.Button("Previous InsideWalls"))
                {
                    house.PInsideWalls();
                }
                GUILayout.Label("F1 Floor v " + house.House.CurF1Floor + " / " + house.House.MaxFloor14);
                if (GUILayout.Button("Next F1 Floor"))
                {
                    house.NF1Floor();
                }
                if (GUILayout.Button("Previous F1 Floor"))
                {
                    house.NF1Floor();
                }
                GUILayout.Label("F2 Floor v " + house.House.CurF2Floor + " / " + house.House.MaxFloor24);
                if (GUILayout.Button("Next F2 Floor"))
                {
                    house.NF2Floor();
                }
                if (GUILayout.Button("Previous F2 Floor"))
                {
                    house.NF2Floor();
                }
                
            }

            #endregion

            #region House007 Custom UI
            if (house.House.HouseNumber == HouseNumber.House007)
            {
                
                //Roofs                   
                GUILayout.Label("Roof v " + house.House.CurRoof + " / " + house.MaxRoofs7);
                if (GUILayout.Button("Next Roof"))
                {
                    house.NRoof();
                }
                if (GUILayout.Button("Previous Roof"))
                {
                    house.PRoof();
                }

                //HoutSideWalls
                    
                GUILayout.Label("OtsideWalls v " + house.House.CurOutsideWall + " / " + house.MaxoutSideWalls7);
                if (GUILayout.Button("Next OtsideWalls"))
                {
                    house.NOutSideWalls();
                }
                if (GUILayout.Button("Previous OtsideWalls"))
                {
                    house.POutSideWalls();
                }
                //InsideWalls
                GUILayout.Label("InsideWalls v " + house.House.CurInsideWall + " / " + house.MaxInsideWalls7);
                if (GUILayout.Button("Next InsideWalls"))
                {
                    house.NInsideWalls();
                }
                if (GUILayout.Button("Previous InsideWalls"))
                {
                    house.PInsideWalls();
                }
                //F1 Floor                  
                GUILayout.Label("F1Floor v " + house.House.CurF1Floor + " / " + house.MaxF1Floors7);
                if (GUILayout.Button("Next F1Floor"))
                {
                    house.NF1Floor();
                }
                if (GUILayout.Button("Previous F1Floor"))
                {
                    house.PF1Floor();
                }
                //F2 Floor                    
                GUILayout.Label("F2Floor v " + house.House.CurF2Floor + " / " + house.MaxF2Floors7);
                if (GUILayout.Button("Next F2Floor"))
                {
                    house.NF2Floor();
                }
                if (GUILayout.Button("Previous F2Floor"))
                {
                    house.PF2Floor();
                }
                //Extra Floor
              
                GUILayout.Label("Garage Floor v " + house.House.CurExtraFloor + " / " + house.MaxExtraFloor7);
                if (GUILayout.Button("Next Garage Floor"))
                {
                    house.NExtraFloor();
                }
                if (GUILayout.Button("Previous Garage Floor"))
                {
                    house.PExtraFloor();
                }

                //Door1                    
                GUILayout.Label("Door1 v " + house.House.CurDoor1 + " / " + house.MaxDoor17);
                if (GUILayout.Button("Next Door1"))
                {
                    house.NDoor1();
                }
                if (GUILayout.Button("Previous Door1"))
                {
                    house.PDoor1();
                }
                //door2
                GUILayout.Label("Door2 v " + house.House.CurDoor2 + " / " + house.MaxDoor27);
                if (GUILayout.Button("Next Door2"))
                {
                    house.NDoor2();
                }
                if (GUILayout.Button("Previous Door2"))
                {
                    house.PDoor2();
                }
                //door3
                GUILayout.Label("Door3 v " + house.House.CurDoor3 + " / " + house.MaxDoor37);
                if (GUILayout.Button("Next Door3"))
                {
                    house.NDoor3();
                }
                if (GUILayout.Button("Previous Door3"))
                {
                    house.PDoor3();
                }
                //door4
                GUILayout.Label("Garage Door v " + house.House.CurDoor4 + " / " + house.MaxDoor47);
                if (GUILayout.Button("Next Garage Door"))
                {
                    house.NDoor4();
                }
                if (GUILayout.Button("Previous Garage Door"))
                {
                    house.PDoor4();
                }
                //garage
                GUILayout.Label("Garage v " + house.House.CurAddon1 + " / " + house.MaxAddon17);
                if (GUILayout.Button("Next Garage"))
                {
                    house.NAddon1();
                }
                if (GUILayout.Button("Previous Garage"))
                {
                    house.PAddon1();
                }
            }

            #endregion

            #region House008 Custom UI
            if (house.House.HouseNumber == HouseNumber.House008)
            {               
                //Roof
                GUILayout.Label("Roof v " + house.House.CurRoof + " / " + house.House.MaxRoofs8);
                
                if(GUILayout.Button("Next Roof"))
                {
                    house.NRoof();
                }
                if(GUILayout.Button("Previous Roof"))
                {
                    house.PRoof();
                }
                //OutSideWalls
                GUILayout.Label("OutSideWalls v " + house.House.CurOutsideWall + " / " + house.House.MaxOutSideWalls8);

                if (GUILayout.Button("Next OutSideWalls"))
                {
                    house.NOutSideWalls();
                }
                if (GUILayout.Button("Previous OutSideWalls"))
                {
                    house.POutSideWalls();
                }
                //InSideWalls
                GUILayout.Label("InsideWalls v " + house.House.CurInsideWall + " / " + house.House.MaxInsideWalls8);

                if (GUILayout.Button("Next InsideWalls"))
                {
                    house.NInsideWalls();
                }
                if (GUILayout.Button("Previous InsideWalls"))
                {
                    house.PInsideWalls();
                }
                //Pillars
                GUILayout.Label("Pillars v " + house.House.CurPillar + " / " + house.House.MaxPillars8);

                if (GUILayout.Button("Next Pillars"))
                {
                    house.NPillars();
                }
                if (GUILayout.Button("Previous Pillars"))
                {
                    house.PPillars();
                }
                //Details
                GUILayout.Label("Details v " + house.House.CurAddon1 + " / " + house.House.MaxDetails8);

                if (GUILayout.Button("Next Details"))
                {
                    house.NAddon1();
                }
                if (GUILayout.Button("Previous Details"))
                {
                    house.PAddon1();
                }
                //Door
                GUILayout.Label("Door v " + house.House.CurDoor1 + " / " + house.House.MaxDoors8);

                if (GUILayout.Button("Next Door"))
                {
                    house.NDoor1();
                }
                if (GUILayout.Button("Previous Door"))
                {
                    house.PDoor1();
                }
                //Floor
                GUILayout.Label("Floor v " + house.House.CurF1Floor + " / " + house.House.MaxFloor18);

                if (GUILayout.Button("Next Floor"))
                {
                    house.NF1Floor();
                }
                if (GUILayout.Button("Previous Floor"))
                {
                    house.PF1Floor();
                }
            }

            #endregion

            #region House009 Custom UI
            if (house.House.HouseNumber == HouseNumber.House009)
            {
                //Roofs                   
                GUILayout.Label("Roof v " + house.House.CurRoof + " / " + house.MaxRoofs9);
                if (GUILayout.Button("Next Roof"))
                {
                    house.NRoof();
                }
                if (GUILayout.Button("Previous Roof"))
                {
                    house.PRoof();
                }
                //HoutSideWalls
                GUILayout.Label("OtsideWalls v " + house.House.CurOutsideWall + " / " + house.MaxoutSideWalls9);
                if (GUILayout.Button("Next OtsideWalls"))
                {
                    house.NOutSideWalls();
                }
                if (GUILayout.Button("Previous OtsideWalls"))
                {
                    house.POutSideWalls();
                }
                //InsideWalls
                GUILayout.Label("InsideWalls v " + house.House.CurInsideWall + " / " + house.MaxInsideWalls9);
                if (GUILayout.Button("Next InsideWalls"))
                {
                    house.NInsideWalls();
                }
                if (GUILayout.Button("Previous InsideWalls"))
                {
                    house.PInsideWalls();
                }
                //F1 Floor
                GUILayout.Label("F1Floor v " + house.House.CurF1Floor + " / " + house.MaxF1Floors9);
                if (GUILayout.Button("Next F1Floor"))
                {
                    house.NF1Floor();
                }
                if (GUILayout.Button("Previous F1Floor"))
                {
                    house.PF1Floor();
                }
                //F2 Floor
                GUILayout.Label("F2Floor v " + house.House.CurF2Floor + " / " + house.MaxF2Floors9);
                if (GUILayout.Button("Next F2Floor"))
                {
                    house.NF2Floor();
                }
                if (GUILayout.Button("Previous F2Floor"))
                {
                    house.PF2Floor();
                }
                //Door1
                GUILayout.Label("Door1 v " + house.House.CurDoor1 + " / " + house.MaxDoor19);
                if (GUILayout.Button("Next Door1"))
                {
                    house.NDoor1();
                }
                if (GUILayout.Button("Previous Door1"))
                {
                    house.PDoor1();
                }
                //door2
                GUILayout.Label("Door2 v " + house.House.CurDoor2 + " / " + house.MaxDoor29);
                if (GUILayout.Button("Next Door2"))
                {
                    house.NDoor2();
                }
                if (GUILayout.Button("Previous Door2"))
                {
                    house.PDoor2();
                }
                //door3
                GUILayout.Label("Door3 v " + house.House.CurDoor3 + " / " + house.MaxDoor39);
                if (GUILayout.Button("Next Door3"))
                {
                    house.NDoor3();
                }
                if (GUILayout.Button("Previous Door3"))
                {
                    house.PDoor3();
                }
                //door4
                GUILayout.Label("Door4 v " + house.House.CurDoor4 + " / " + house.MaxDoor49);
                if (GUILayout.Button("Next Door4"))
                {
                    house.NDoor4();
                }
                if (GUILayout.Button("Previous Door4"))
                {
                    house.PDoor4();
                }
                //Pillars
                GUILayout.Label("Pillar v " + house.House.CurPillar + " / " + house.MaxPillars9);
                if (GUILayout.Button("Next Pillar"))
                {
                    house.NPillars();
                }
                if (GUILayout.Button("Previous Pillar"))
                {
                    house.PPillars();
                }
            }
            #endregion


        }

        GUILayout.EndVertical();
    }
	
}
