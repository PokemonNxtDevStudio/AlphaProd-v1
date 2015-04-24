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
            if (GUILayout.Button("New House003"))
            {
                house.HouseBuilder(3);
            }
            if(GUILayout.Button("New House004"))
            {               
                house.HouseBuilder(4);
            }
            if (GUILayout.Button("New House005"))
            {
                house.HouseBuilder(5);
            }
            if (GUILayout.Button("New House006"))
            {
                house.HouseBuilder(6);
            }
            if (GUILayout.Button("New House 007"))
            {
                house.HouseBuilder(7);
            }
            if(GUILayout.Button("New House 008"))
            {
                house.HouseBuilder(8);
            }
            if (GUILayout.Button( "New House 009"))
            {
                house.HouseBuilder(9);
            }
        }
        if(house.House.HouseNumber != HouseNumber.None)
        {
            if (GUILayout.Button("Set To Ground Level"))
            {
                house.SetToGoundLevel();
            }
            GUILayout.Space(20);
            //Roof
            GUILayout.Label("Roof v " + house.House.CurRoof + " / " + house.House.MaxRoofAtm);
            if (GUILayout.Button("Next Roof"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Roof, true);
            }
            if (GUILayout.Button("Previous Roof"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Roof, false);
            }

            //ExtraRoof
            if (house.House.ExtraRoof != null && house.House.MaxExtraRoofAtm > 1)
            {
                GUILayout.Label("ExtraRoof v " + house.House.CurExtraRoof + " / " + house.House.MaxExtraRoofAtm);
                if (GUILayout.Button("Next ExtraRoof"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.ExtraRoof, true);
                }
                if (GUILayout.Button("Previous ExtraRoof"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.ExtraRoof, false);
                }
            }

            //OutSideWalls
            GUILayout.Label("OutSideWalls v " + house.House.CurOutsideWall + " / " + house.House.MaxOutSideWallsAtm);
            if (GUILayout.Button("Next OutSideWalls"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.OutsideWall, true);
            }
            if (GUILayout.Button("Previous OutSideWalls"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.OutsideWall, false);
            }
            //Frames
            if(house.House.Frames != null && house.House.MaxFramesAtm > 1)
            {
                GUILayout.Label("Frames v " + house.House.CurFrames + " / " + house.House.MaxFramesAtm);
                if (GUILayout.Button("Next Frames"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Frames, true);
                }
                if (GUILayout.Button("Previous Frames"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Frames, false);
                }
            }
            //InsideWalls
            GUILayout.Label("InsideWalls v " + house.House.CurInsideWall + " / " + house.House.MaxInsideWallsAtm);
            if (GUILayout.Button("Next InsideWalls"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideWall, true);
            }
            if (GUILayout.Button("Previous InsideWalls"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideWall, false);
            }

            //InsideRoof
            if (house.House.InsideRoof != null && house.House.MaxInsideRoofAtm > 1)
            {
                GUILayout.Label("InsideRoof v " + house.House.CurInsideRoof + " / " + house.House.MaxInsideRoofAtm);
                if (GUILayout.Button("Next InsideRoof"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideRoof, true);
                }
                if (GUILayout.Button("Previous InsideRoof"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideRoof, false);
                }
 
            }

            //InsideExtraWalls
            if (house.House.InsideExtraWalls != null && house.House.MaxInsideExtraWallsAtm > 1)
            {
                GUILayout.Label("InsideExtraWalls v " + house.House.CurInsideExtraWalls + " / " + house.House.MaxInsideExtraWallsAtm);
                if (GUILayout.Button("Next InsideExtraWalls"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideExtraWall, true);
                }
                if (GUILayout.Button("Previous InsideExtraWalls"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.InsideExtraWall, false);
                }

            }

            //F1Floor
            GUILayout.Label("F1Floor v " + house.House.CurF1Floor + " / " + house.House.MaxF1FloorAtm);
            if (GUILayout.Button("Next F1Floor"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.F1Floor, true);
            }
            if (GUILayout.Button("Previous F1Floor"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.F1Floor, false);
            }

            //F2Floor
            if (house.House.F2Floor != null && house.House.MaxF2FloorAtm > 1)
            {
                GUILayout.Label("F2Floor v " + house.House.CurF2Floor + " / " + house.House.MaxF2FloorAtm);
                if (GUILayout.Button("Next F2Floor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.F2Floor, true);
                }
                if (GUILayout.Button("Previous F2Floor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.F2Floor, false);
                } 
            }

            //ExtraFloor
            if (house.House.ExtraFloor != null && house.House.MaxExtraFloorAtm > 1)
            {
                GUILayout.Label("ExtraFloor v " + house.House.CurExtraFloor + " / " + house.House.MaxExtraFloorAtm);
                if (GUILayout.Button("Next ExtraFloor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.ExtraFloor, true);
                }
                if (GUILayout.Button("Previous ExtraFloor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.ExtraFloor, false);
                }  
            }

            //FirePlace
            if(house.House.FirePlace != null && house.House.MaxFirePlaceAtm > 1)
            {
                GUILayout.Label("FirePlace v " + house.House.CurFirePlace + " / " + house.House.MaxFirePlaceAtm);
                if (GUILayout.Button("Next FirePlace"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Fireplace, true);
                }
                if (GUILayout.Button("Previous FirePlace"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Fireplace, false);
                }  
            }

            //Door1
            GUILayout.Label("Door1 v " + house.House.CurDoor1 + " / " + house.House.MaxDoor1Atm);
            if (GUILayout.Button("Next Door1"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door1, true);
            }
            if (GUILayout.Button("Previous Door1"))
            {
                house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door1, false);
            }

            //Door2
            if(house.House.Door2 != null && house.House.MaxDoor2Atm > 1)
            {
                GUILayout.Label("Door2 v " + house.House.CurDoor2 + " / " + house.House.MaxDoor2Atm);
                if (GUILayout.Button("Next Door2"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door2, true);
                }
                if (GUILayout.Button("Previous Door2"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door2, false);
                }  
            }
            //Door3
            if (house.House.Door3 != null && house.House.MaxDoor3Atm > 1)
            {
                GUILayout.Label("Door3 v " + house.House.CurDoor3 + " / " + house.House.MaxDoor3Atm);
                if (GUILayout.Button("Next Door3"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door3, true);
                }
                if (GUILayout.Button("Previous Door3"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door3, false);
                }
            }
            //Door4
            if (house.House.Door4 != null && house.House.MaxDoor4Atm > 1)
            {
                GUILayout.Label("Door4 v " + house.House.CurDoor4 + " / " + house.House.MaxDoor4Atm);
                if (GUILayout.Button("Next Door4"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door4, true);
                }
                if (GUILayout.Button("Previous Door4"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Door4, false);
                }
            }

            //GarageDoor
            if (house.House.GarageDoor != null && house.House.MaxGarageDoorAtm > 1)
            {
                GUILayout.Label("GarageDoor v " + house.House.CurGarageDoor + " / " + house.House.MaxGarageDoorAtm);
                if (GUILayout.Button("Next GarageDoor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.GargageDoor, true);
                }
                if (GUILayout.Button("Previous GarageDoor"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.GargageDoor, false);
                }
            }
            //Garage
            if (house.House.Garage != null && house.House.MaxGarageAtm > 1)
            {
                GUILayout.Label("Garage v " + house.House.CurGarage + " / " + house.House.MaxGarageAtm);
                if (GUILayout.Button("Next Garage"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Garage, true);
                }
                if (GUILayout.Button("Previous Garage"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Garage, false);
                }
            }

            //FrontStairs
            if (house.House.FrontStairs != null && house.House.MaxFrontStairsAtm > 1)
            {
                GUILayout.Label("FrontStairs v " + house.House.CurFrontStairs + " / " + house.House.MaxFrontStairsAtm);
                if (GUILayout.Button("Next FrontStairs"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.FrontStairs, true);
                }
                if (GUILayout.Button("Previous FrontStairs"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.FrontStairs, false);
                }
            }
            //Pillars
            if (house.House.Pillar != null && house.House.MaxPillarsAtm > 1)
            {
                GUILayout.Label("Pillars v " + house.House.CurPillar + " / " + house.House.MaxPillarsAtm);
                if (GUILayout.Button("Next Pillars"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Pillar, true);
                }
                if (GUILayout.Button("Previous Pillars"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Pillar, false);
                }
            }
            //Basement
            if (house.House.Basement != null && house.House.MaxBasementAtm > 1)
            {
                GUILayout.Label("Basement v " + house.House.CurBasement + " / " + house.House.MaxBasementAtm);
                if (GUILayout.Button("Next Basement"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Basement, true);
                }
                if (GUILayout.Button("Previous Basement"))
                {
                    house.HouseChanger((int)house.House.HouseNumber, PartOfHouse.Basement, false);
                }
            }

            #region old code

            /*
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
            */
            #endregion

        }

        GUILayout.EndVertical();
    }
	
}
