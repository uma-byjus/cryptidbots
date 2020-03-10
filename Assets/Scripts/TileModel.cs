using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel {
    public enum TileType {
        WATER,
        MOUNTAIN,
        FOREST,
        SWAMP,
        DESERT
    }

    public enum TileTerritory {
        NONE,
        COUGAR_TERRITORY,
        BEAR_TERRITORY
    }

    public enum TileStructureType {
        NONE,
        STANDING_STONE,
        ABANDONED_SHACK
    }

    public enum TileStructureColor {
        BLUE,
        GREEN,
        WHITE,
        BLACK
    }

}
