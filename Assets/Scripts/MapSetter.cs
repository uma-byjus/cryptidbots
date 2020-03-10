using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetter : MonoBehaviour
{
    public MapSegmentScriptableObject mapSegment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("Set Segment 2")]
    private void SetMapSegmentTwo() {
        for (int i = 0; i < mapSegment.rowOne.Length; i++) {
            if (i == 0) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.SWAMP;
            } else {
                mapSegment.rowOne[i].gridType = TileModel.TileType.FOREST;
            }
            if (i < 3) {
                mapSegment.rowOne[i].gridTerritory = TileModel.TileTerritory.COUGAR_TERRITORY;
            }


            // row 2
            if (i < 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.SWAMP;
            } 
            if (i == 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.FOREST;
            }
            if (i >= 3) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.DESERT;
            }


            // row 3
            if (i == 0) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.SWAMP;
            }
            if (i > 0 && i < 5) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i == 5) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.DESERT;
            }
        }
    }

    [ContextMenu("Set Segment 3")]
    private void SetMapSegmentThree() {
        for (int i = 0; i < mapSegment.rowOne.Length; i++) {
            if (i < 2) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.SWAMP;
            } 
            if (i >= 2 && i < 5) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.FOREST;
            }
            if (i == 5) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.WATER;
            }

            // row 2
            if (i < 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.SWAMP;
                mapSegment.rowTwo[i].gridTerritory = TileModel.TileTerritory.COUGAR_TERRITORY;
            } 
            if (i == 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.FOREST;
            }
            if (i == 3) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i >= 4) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.WATER;
            }


            // row 3    
            if (i < 4) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i >= 4) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.WATER;
            }

             if (i == 0) {
                mapSegment.rowThree[i].gridTerritory = TileModel.TileTerritory.COUGAR_TERRITORY;
            }
        }
    }

    [ContextMenu("Set Segment 4")]
    private void SetMapSegmentFour() {
        for (int i = 0; i < mapSegment.rowOne.Length; i++) {
            if (i < 2) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.DESERT;
            } else {
                mapSegment.rowOne[i].gridType = TileModel.TileType.MOUNTAIN;
            }

            // row 2
            if (i < 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.DESERT;
            }
            if (i == 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i > 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.WATER;
            }
            if (i == 5) {
                mapSegment.rowTwo[i].gridTerritory = TileModel.TileTerritory.COUGAR_TERRITORY;
            }

            // row 3 
            if (i < 3) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.DESERT;
            } else {
                mapSegment.rowThree[i].gridType = TileModel.TileType.FOREST;
            }
            if (i == 5) {
                mapSegment.rowThree[i].gridTerritory = TileModel.TileTerritory.COUGAR_TERRITORY;
            }
        }
    }

    [ContextMenu("Set Segment 5")]
    private void SetMapSegmentFive() {
        for (int i = 0; i < mapSegment.rowOne.Length; i++) {
            if (i < 3) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.SWAMP;
            } else {
                mapSegment.rowOne[i].gridType = TileModel.TileType.MOUNTAIN;
            }

            // row 2
            if (i == 0) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.SWAMP;
            }
            if (i > 0 && i < 3) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.DESERT;
            }
            if (i == 3) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.WATER;
            }
            if (i > 3) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.MOUNTAIN;
            } 
            if (i == 5) {
                mapSegment.rowTwo[i].gridTerritory = TileModel.TileTerritory.BEAR_TERRITORY;
            }

            // row 3 
            if (i < 2) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.DESERT;
            } else {
                mapSegment.rowThree[i].gridType = TileModel.TileType.WATER;
            }
            if (i > 3) {
                mapSegment.rowThree[i].gridTerritory = TileModel.TileTerritory.BEAR_TERRITORY;
            }
        }
    }

    [ContextMenu("Set Segment 6")]
    private void SetMapSegmentSix() {
        for (int i = 0; i < mapSegment.rowOne.Length; i++) {
            if (i < 2) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.DESERT;
            }
            if (i >= 2 && i < 5) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.SWAMP;
            }
            if (i == 5) {
                mapSegment.rowOne[i].gridType = TileModel.TileType.FOREST;
            }
            if (i == 0) {
                mapSegment.rowOne[i].gridTerritory = TileModel.TileTerritory.BEAR_TERRITORY;
            }

            // row 2 
            if (i < 2) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i >= 2 && i < 4) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.SWAMP;
            }
            if (i >= 4) {
                mapSegment.rowTwo[i].gridType = TileModel.TileType.FOREST;
            }
            if (i == 0) {
                 mapSegment.rowTwo[i].gridTerritory = TileModel.TileTerritory.BEAR_TERRITORY;
            }

            // row 3 
            if (i == 0) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.MOUNTAIN;
            }
            if (i > 0 && i < 5) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.WATER;
            }
            if (i == 5) {
                mapSegment.rowThree[i].gridType = TileModel.TileType.FOREST;
            }
        }
    }

}
